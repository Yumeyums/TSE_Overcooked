using Godot;
using System;

public partial class InteractableScript : Node3D
{
		[Export]
	public MeshInstance3D mesh;
	
	public override void _Ready()
	{
		mesh = GetParent().GetNode<MeshInstance3D>("MeshInstance3D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public double getDistance(Node3D detector){
		double diffX = detector.GlobalPosition[0] - GlobalPosition[0];
		double diffZ = detector.GlobalPosition[2] - GlobalPosition[2];
		double dist = Math.Sqrt((diffX*diffX)+(diffZ*diffZ));
		return dist;
	}

	public void targeted(){
		//GD.Print("targeted");
		var material = mesh.GetSurfaceOverrideMaterial(0);
		Color c = new Color(1, 0, 0,0.8f);
		var overrideMaterial = material.Duplicate() as StandardMaterial3D;
		overrideMaterial.AlbedoColor = c;
		mesh.SetSurfaceOverrideMaterial(0, overrideMaterial);
	}

	public void untargeted(){
		//GD.Print("untargeted");
		var material = mesh.GetSurfaceOverrideMaterial(0);
		Color c = new Color(1, 1, 1,0.8f);
		var overrideMaterial = material.Duplicate() as StandardMaterial3D;
		overrideMaterial.AlbedoColor = c;
		mesh.SetSurfaceOverrideMaterial(0, overrideMaterial);
	}
}
