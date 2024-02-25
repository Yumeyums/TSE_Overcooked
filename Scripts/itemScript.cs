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
	
	[Export]
	public MeshInstance3D mesh;
	public Node player1, player2;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mesh = GetNode<MeshInstance3D>("MeshInstance3D");
		player1 = GetTree().GetNodesInGroup("Players")[0];
		player2 = GetTree().GetNodesInGroup("Players")[1];
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		targetted();
	}
	
	public double getDistance(Node3D detector){
		double diffX = detector.GlobalPosition[0] - GlobalPosition[0];
		double diffZ = detector.GlobalPosition[2] - GlobalPosition[2];
		double dist = Math.Sqrt((diffX*diffX)+(diffZ*diffZ));
		GD.Print("distance: ", dist);
		return dist;
	}
	
	public void Interact(Node3D player){
		GD.Print("hello");
		
	}
	
	public void targetted(){
		if(player1.Call("isTargetNode",this)){
			GD.Print("player 1 target ", this.Name());
			var material = mesh.GetSurfaceOverrideMaterial(0);
			Color c = new Color(0.8f, 0.2f, 0.8f,0.1f);
			var overrideMaterial = material.Duplicate() as StandardMaterial3D;
			overrideMaterial.AlbedoColor = c;
			mesh.SetSurfaceOverrideMaterial(0, overrideMaterial);
		}
		else if (player2.Call("isTargetNode",this)){
			GD.Print("player 2 target ", this.Name());
		}
	}
}
