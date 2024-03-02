using Godot;
using System;

public partial class counterScript : StaticBody3D
{
	[Export]
	public MeshInstance3D mesh;
	
		public override void _Ready()
	{
		mesh = GetNode<MeshInstance3D>("MeshInstance3D");
	}
	
	private void _on_area_3d_body_entered(Node3D body)
{
	GD.Print("Contact");
		body.GlobalPosition = GlobalPosition + new Vector3(0f,1f,0f);;
}

	public void targetted(){
		GD.Print("targetted");
		var material = mesh.GetSurfaceOverrideMaterial(0);
		Color c = new Color(1, 0, 0,0.8f);
		var overrideMaterial = material.Duplicate() as StandardMaterial3D;
		overrideMaterial.AlbedoColor = c;
		mesh.SetSurfaceOverrideMaterial(0, overrideMaterial);
	}
	
	public void untargetted(){
		GD.Print("untargetted");
		var material = mesh.GetSurfaceOverrideMaterial(0);
		Color c = new Color(1, 1, 1,0.8f);
		var overrideMaterial = material.Duplicate() as StandardMaterial3D;
		overrideMaterial.AlbedoColor = c;
		mesh.SetSurfaceOverrideMaterial(0, overrideMaterial);
	}
}

