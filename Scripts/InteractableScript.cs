using Godot;
using System;

public partial class InteractableScript : Node3D
{
		[Export]
	public MeshInstance3D mesh;
	public MeshInstance3D model;
	
	public override void _Ready()
	{
		mesh = GetParent().GetNode<MeshInstance3D>("MeshInstance3D");
		model = GetParent().GetNode<MeshInstance3D>("Model");
	}
	
	public double getDistance(Node3D detector){
		double diffX = detector.GlobalPosition[0] - GlobalPosition[0];
		double diffZ = detector.GlobalPosition[2] - GlobalPosition[2];
		double dist = Math.Sqrt((diffX*diffX)+(diffZ*diffZ));
		return dist;
	}
	
	public void target(bool target, Node3D player){
		this.Call("ChangeColour",target,player);
		if (this.GetParent().GetParent().Name == "Counter"){
			Node3D heldItem = (Node3D) GetParent().GetParent().Call("GetItemOnCounter");
			if (heldItem !=  null ){
				heldItem.GetNode("Interactable").Call("ChangeColour",target,player);
			}
		}
		else{
			Node3D container = (Node3D) this.GetParent().Call("getContainer");
			if (container !=  null){
				if (!(container.Name == "Player1")||(container.Name == "Player2")){
					container.GetNode("StaticBody3D").GetNode("Interactable").Call("ChangeColour",target,player);
				}
			}
		}
	}

	
	public void ChangeColour(bool target, Node3D player){
		//GD.Print("change: ", this.GetParent().GetParent().Name, ", ", target);
		Color c = new Color(1, 1, 1,0.8f);
		if (target == true) {
			if ((int) player.Call("GetPlayerNumber") == 1){ c = new Color(1f, 0.2f, 0.2f,0.5f);}
			else{c = new Color(0.2f, 0.2f, 1f,0.5f);}
		}
		Material material = null;
		if(model == null){material = mesh.GetSurfaceOverrideMaterial(0);}
		else{material = model.GetSurfaceOverrideMaterial(0);}
		//GD.Print("material: ",material);
		var overrideMaterial = material.Duplicate() as StandardMaterial3D;
		overrideMaterial.AlbedoColor = c;
		mesh.SetSurfaceOverrideMaterial(0, overrideMaterial);
	
	}
}
