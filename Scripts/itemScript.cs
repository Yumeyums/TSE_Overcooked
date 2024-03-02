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
	public Node3D heldBy;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mesh = GetNode<MeshInstance3D>("MeshInstance3D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public double getDistance(Node3D detector){
		double diffX = detector.GlobalPosition[0] - GlobalPosition[0];
		double diffZ = detector.GlobalPosition[2] - GlobalPosition[2];
		double dist = Math.Sqrt((diffX*diffX)+(diffZ*diffZ));
		//GD.Print("distance: ", dist);
		GD.Print("distance: ", dist);
		return dist;
	}

	public void Interact(Node3D player, bool handsEmpty){
		if (handsEmpty == true){
			GD.Print("yoink");
			heldBy = player;
			GlobalPosition = player.GlobalPosition + new Vector3(0f,1.5f,0f);
		}
	}
	
	public void targetted(){
		var material = mesh.GetSurfaceOverrideMaterial(0);
		Color c = new Color(1, 0, 0,0.8f);
		var overrideMaterial = material.Duplicate() as StandardMaterial3D;
		overrideMaterial.AlbedoColor = c;
		mesh.SetSurfaceOverrideMaterial(0, overrideMaterial);
	}
	
	public void untargetted(){
		var material = mesh.GetSurfaceOverrideMaterial(0);
		Color c = new Color(1, 1, 1,0.8f);
		var overrideMaterial = material.Duplicate() as StandardMaterial3D;
		overrideMaterial.AlbedoColor = c;
		mesh.SetSurfaceOverrideMaterial(0, overrideMaterial);
	}
}
