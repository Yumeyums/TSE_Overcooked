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
	
	public void targeted(){
		GD.Print("targeted");
		var material = mesh.GetSurfaceOverrideMaterial(0);
		Color c = new Color(1, 0, 0,0.8f);
		var overrideMaterial = material.Duplicate() as StandardMaterial3D;
		overrideMaterial.AlbedoColor = c;
		mesh.SetSurfaceOverrideMaterial(0, overrideMaterial);
	}

	public void untargeted(){
		GD.Print("untargeted");
		var material = mesh.GetSurfaceOverrideMaterial(0);
		Color c = new Color(1, 1, 1,0.8f);
		var overrideMaterial = material.Duplicate() as StandardMaterial3D;
		overrideMaterial.AlbedoColor = c;
		mesh.SetSurfaceOverrideMaterial(0, overrideMaterial);
	}
}
