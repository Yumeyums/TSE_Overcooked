using Godot;
using System;

public partial class CounterItemDetecter : Area3D
{
	[Export]
	public MeshInstance3D mesh;
	
		public override void _Ready()
	{
		mesh = GetNode<MeshInstance3D>("MeshInstance3D");
	}
	
	private void _on_body_entered(Node3D body)
	{
		GD.Print("Contact");
		body.GlobalPosition = GlobalPosition;
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



