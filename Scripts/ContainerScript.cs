using Godot;
using System;

public partial class ContainerScript : Node3D
{
	[Export]
	private Godot.Collections.Array<Node3D> items = new Godot.Collections.Array<Node3D>();
	private string[] recipeData;
	private Godot.Collections.Array<String> recipes = new Godot.Collections.Array<String>();
	private Godot.Collections.Array<Godot.Collections.Array<String>> ingredients = new Godot.Collections.Array<Godot.Collections.Array<String>>();
	
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
			}
		}
		GD.Print(ingredients);
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
	
	private void _on_area_3d_body_entered(RigidBody3D body)
	{
		if ((body.GetNode("Interactable") != null))
		{
			Node3D C = (Node3D) body.Call("getContainer");
			if((C.Name == "Player1")||(C.Name == "Player2")){
					C.Call("setCarryItemNull");
				}
			AddToContainer(body);
		}
	}
	
	public void AddToContainer(RigidBody3D carriedItem){
		if (carriedItem.GetNode("Ingredient")!= null){
			carriedItem.Call("DropInto",this);
			carriedItem.GlobalPosition = this.GlobalPosition + new Vector3(0f, 0.5f, 0f);
			carriedItem.GetNode("Interactable").QueueFree();
			items.Add(carriedItem);
			Node3D c = (Node3D)carriedItem.Call("getContainer");
			if ((c.Name == "Player1")||(c.Name == "Player2")){
				c.Call("setCarryItem",null);
			}
		}
	}
	
	public void getDish(string dish, Node3D iOC, Node counter)
	{
		if (checkRecipe(dish))
		{
			counter.Call("takeFood", iOC);
		}
	}
	
	private bool checkRecipe(string dish)
	{
		int itemsChecked = 0;
		foreach (var v in ingredients)
		{
			for (int i = 0; i < v.Count; i++)
			{
				if(v[i] == items[i].GetParent().Name)
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
	
	private void Delete(){
		foreach(Node item in items){
			item.QueueFree();
		}
		GD.Print("Delete");
		this.QueueFree();
	}
}


