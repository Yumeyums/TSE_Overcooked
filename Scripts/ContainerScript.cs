using Godot;
using System;

public partial class ContainerScript : Node3D
{
	[Export]
	public Godot.Collections.Array<Node3D> items = new Godot.Collections.Array<Node3D>();
	string[] recipeData;
	private Godot.Collections.Array<String> recipes = new Godot.Collections.Array<String>();
	//public Godot.Collections.Array<String> ingredients = new Godot.Collections.Array<String>();
	public Godot.Collections.Array<Godot.Collections.Array<String>> ingredients = new Godot.Collections.Array<Godot.Collections.Array<String>>();
	
	public override void _Ready(){
		string fileName = "res://Scripts/recipes.txt";		//Filename Label
		using var fileContent = FileAccess.Open(fileName, FileAccess.ModeFlags.Read);	//Accessing the file
		string fileString = fileContent.GetAsText();
		string[] textLines = fileString.Split("\n");
		for (int i = 0; i < textLines.Length; i++){	
			recipeData = textLines[i].Split(",");
			if ((recipeData[0] != "ID") && (recipeData[0] != "0")){
				recipes.Add(recipeData[1]);
				Godot.Collections.Array<String> ingredientList = new Godot.Collections.Array<String>();
				for (int e = 2; e < recipeData.Length; e++){
					if(recipeData[e] != ""){
						ingredientList.Add(recipeData[e]);
					}
				}
				ingredients.Add(ingredientList);
			}
		}
	}
	
	public int GetPositionInItems(Node node){
		int position = -1;
		for (int i = 0; i < items.Count; i++){
			if (items[i] == node){ position = i;}
		}
		return position;
	}
	
	public Godot.Collections.Array<Node3D> GetItems(){
		return items;
	}
	
	private void _on_area_3d_body_entered(Node3D body)
	{
	}
	
	public void AddToContainer(RigidBody3D carriedItem){
		int recipe = -1;
		for (int i = 0; i < ingredients.Count; i++){
			//GD.Print(recipes[i]);
			for (int e = 0; e < ingredients[i].Count; e++){
				if(carriedItem.Name == ingredients[i][e]){
					GD.Print("in container: ", recipes[i]);
					recipe = i;
				}
			}	
		}
		//if (recipe > -1){
			carriedItem.Call("DropInto",this);
			carriedItem.GlobalPosition = this.GlobalPosition + new Vector3(0f, 0.5f, 0f);
			carriedItem.GetNode("Interactable").QueueFree();
			items.Add(carriedItem);
		//}
	}
	
	public void getDish(string dish, Node3D iOC, Node counter)
	{
		if (checkRecipe(dish))
		{
			counter.Call("takeDish", iOC);
		}
	}
	
	private bool checkRecipe(string dish)
	{
		int itemsChecked = 0;
		foreach (var v in ingredients)
		{
			for (int i = 0; i < v.Count; i++)
			{
				if(v[i] == items[i].Name)
				{
					itemsChecked++;
				}
			}
			
			if(itemsChecked == v.Count)
			{
				return true;
			}
		}
		return false;
		
	}
}


