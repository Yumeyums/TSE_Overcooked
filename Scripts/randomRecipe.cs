using Godot;
using System;



public partial class randomRecipe : Node{
	
	//Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		//Needs a data structure to hold each recipe, I.E. list
		var Recipes = new System.Collections.Generic.List<string[]>();
		
		//Recipe file is a csv format txt file with ID, Name, Ingredient 1-4 in each line
		string fileName = "res://Scripts/recipes.txt";		//Filename Label
		using var fileContent = FileAccess.Open(fileName, FileAccess.ModeFlags.Read);	//Accessing the file
		string fileString = fileContent.GetAsText();		//Passing the file contect as text in a string.
		
		string[] textLines = fileString.Split("\n");	//Splitting the file into lines.
		string[] RecipeData;	//Array that contains the data of each recipe in their respective line.
		int nRecipes = 0;
		
		foreach (string data in textLines){		//Iterates through all the strings
			
			RecipeData = data.Split(",");	//Splits the data at the "," since in a CSV it divides data per line
			
			//GD.Print(RecipeData[0]);		//Used for debugging
			
			//Check that the current recipe isn't the field names (ID) or number 0
			// Which contains the URL to the excel spreadsheet the csv is derived from. 
			if ((RecipeData[0] != "ID") && (RecipeData[0] != "0")){
				Recipes.Add(RecipeData);
				nRecipes = nRecipes + 1;
			}
		}
		
		//Used only for Debugging
		//foreach (var u in Recipes){
		//	GD.Print(u);
		//}
		
		int maxRecipes = 8;
		
		var RandomRecipes = new System.Collections.Generic.List<string[]>();
		
		var randValue = 0;
		
		for(int i=0; i<maxRecipes; i++){
			randValue = (int)(GD.Randi() % nRecipes - 1);
			//GD.Print(randValue);		//used for debugging
			RandomRecipes.Add(Recipes[randValue]);
		}
		
		
		foreach (var u in RandomRecipes){
			GD.Print(u[1]);
		}
		
	}

	//Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
