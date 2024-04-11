using Godot;
using System;

public partial class ContainerItemsScripts : Node3D
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
	
	private void _on_body_entered(RigidBody3D body)
	{
		//if(body.GetNode("Ingredient") != null)
		//{
			//AddToPlate(body);
			//GD.Print(body.GetParent().Name, " on plate");
		//}
	}

	
	public void AddToPlate(Node3D carriedItem){
		//GD.Print(ingredients);
		int recipe = -1;
		for (int i = 0; i < ingredients.Count; i++){
			//GD.Print(recipes[i]);
			for (int e = 0; e < ingredients[i].Count; e++){
				if(carriedItem.Name == ingredients[i][e]){
					GD.Print(recipes[i]);
					recipe = i;
				}
			}	
		}
		//if (recipe > -1){
			carriedItem.Call("DropInto",this);
			carriedItem.GlobalPosition = this.GlobalPosition + new Vector3(0f, 0.5f, 0f);
			items.Add(carriedItem);
		//}
	}
}
