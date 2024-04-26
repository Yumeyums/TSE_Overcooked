using Godot;
using System;

public partial class UIScript : Node{
	
	System.Collections.Generic.List<string[]> OrderList;
	int StartingTime;
	double RemainingTime;
	///*
	public int[] checkpoints; 	//Static Order Checkpoints
	public int[] cTimers;		//Current Timers
	public int[] pTimers;		//Time Passed Timers
	public int[] tStart;		//Timer starting point
	public int[] orderID;		//Order IDs for Slot script.
	public int[] slotID;		//Label Slots for UI.
	public bool[] start;		//Start flag for timers
	public bool OrderReady = false;
	//*/
	public System.Collections.Generic.List<string[]> randomRecipe(){
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
		
		return RandomRecipes;
	}
	
	public void UpdateTimer(double delta) {
		
		if(RemainingTime > 0) {
			
			string TimerString;
			RemainingTime -= delta;
			int Minutes = (int)RemainingTime / 60;
			int Seconds = (int)RemainingTime - (Minutes * 60);
			if(Seconds > 10) {
				TimerString = Minutes.ToString() + ":" + Seconds.ToString();
			} else {
				TimerString = Minutes.ToString() + ":0" + Seconds.ToString();
			}
			
			GetNode<Label>("TimeRemaining").Text = TimerString;
			
		} else {
			GD.Print("Insert Timeout");
		}
		
	}
	///*
	public void SetupOrder() {
		
		int interval = StartingTime / (OrderList.Count-1); 	//Minus one to avoid one recipe 
															//appearing as soon as the timer ends.
		
		checkpoints = new int[OrderList.Count];
		cTimers = new int[OrderList.Count];
		pTimers = new int[OrderList.Count];
		start = new bool[OrderList.Count];
		tStart = new int[OrderList.Count];
		orderID = new int[OrderList.Count];
		slotID = new int[4];
		slotID[0] = -1;
		slotID[1] = -1;
		slotID[2] = -1;
		slotID[3] = -1;
		
		int i = 0;
		foreach(var u in OrderList) {
			orderID[i] = i;
			checkpoints[i] = i * interval;
			cTimers[i] = 30;
			GD.Print(checkpoints[i]);
			
			i += 1;
		}
		
		GetNode<Label>("Order1").Text = "";
		GetNode<Label>("Order2").Text = "";
		GetNode<Label>("Order3").Text = "";
		GetNode<Label>("Order4").Text = "";
		
		OrderReady = true;
		
	}//*/
	///*
	public void UpdateOrder(double delta) {
		int i = 0;
		string CurrentOrder = "";
		
		foreach(var u in OrderList) {
			
			if((checkpoints[i] > RemainingTime) & (pTimers[i]>=0))	{
				int j = 0;
				if(start[i] != true){ 
						while (j < 4){
							
							if (slotID[j] == -1) {
								slotID[j] = orderID[i]; 
								start[i] = true;
								tStart[i] = (int)RemainingTime;
								j = 4;
							}
							
							j += 1;
						}
						
				} else { if (start[i] == true) { 
						int timePassed = (int)RemainingTime - tStart[i];
						pTimers[i] = cTimers[i] + timePassed;
						
						
						
					}
				}
				CurrentOrder = 	u[1] + " - " + pTimers[i] + "s\n -" +
										u[2] + "\n -" +
										u[3] + "\n -" +
										u[4] + "\n -" +
										u[5] + "\n";
			}
			
			// 1st Order Slot
			if (slotID[0] == orderID[i]) {
				if (pTimers[i]>=0) {
					GetNode<Label>("Order1").Text = CurrentOrder;
				} else {
					
					slotID[0] = slotID[1];
					slotID[1] = slotID[2];
					slotID[2] = slotID[3];
					slotID[3] = -1;
				}
			}
			// 2nd Order Slot
			if (slotID[1] == orderID[i]) {
				if (pTimers[i]>=0) {
					GetNode<Label>("Order2").Text = CurrentOrder;
				} else {
					
					slotID[1] = slotID[2];
					slotID[2] = slotID[3];
					slotID[3] = -1;
				}
			}
			// 3rd Order Slot
			if (slotID[2] == orderID[i]) {
				if (pTimers[i]>=0) {
					GetNode<Label>("Order3").Text = CurrentOrder;
				} else {
					
					slotID[2] = slotID[3];
					slotID[3] = -1;
				}
			}
			// 4th Order Slot
			if (slotID[3] == orderID[i]) {
				if (pTimers[i]>=0) {
					GetNode<Label>("Order4").Text = CurrentOrder;
				} else {
					
					slotID[3] = -1;
				}
			}
			
			if (slotID[0] == -1) {GetNode<Label>("Order1").Text = "";}
			if (slotID[1] == -1) {GetNode<Label>("Order2").Text = "";}
			if (slotID[2] == -1) {GetNode<Label>("Order3").Text = "";}
			if (slotID[3] == -1) {GetNode<Label>("Order4").Text = "";}
			
			i += 1;
		}
		
		
		
		
	}
	//*/
	//Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		
		StartingTime = 90;
		RemainingTime = StartingTime;
		
		OrderList = randomRecipe();
		
		
		
		SetupOrder();
		
	}

	//Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		UpdateTimer(delta);
		//GD.Print(RemainingTime);
		//GD.Print((int)RemainingTime);
		
		UpdateOrder(delta);
		
		
	}
}
