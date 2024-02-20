using Godot;
using System;

public partial class itemScript : RigidBody3D
{
	[Export]
	public bool canCook;
	[Export]
	public bool canBoil;
	[Export]
	public bool canChop;
	[Export]
	public bool canClean;
	[Export]
	public bool canSubmit;
	[Export]
	public bool canPlate;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void Interact(Node3D player){
		GD.Print("hello");
	}
}
