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

/*
	public double getDistance(Node3D detector){
		double diffX = detector.GlobalPosition[0] - GlobalPosition[0];
		double diffZ = detector.GlobalPosition[2] - GlobalPosition[2];
		double dist = Math.Sqrt((diffX*diffX)+(diffZ*diffZ));
		//GD.Print("distance: ", dist);
		GD.Print("distance: ", dist);
		return dist;
	}
	*/
	
	public override void _PhysicsProcess(double delta)
	{
		if (heldBy != null){
			this.Sleeping = true;
			GlobalPosition = heldBy.GlobalPosition + heldBy.GlobalTransform.Basis.Z*-1;
		}
	}

	public void PickUp(Node3D player){
		if (heldBy != null){
			if (heldBy.GetParent().Name == "Counter"){
				heldBy.Call("RemoveFromCounter");
			}
		}
		heldBy = player;
		this.Sleeping = true;
	}
		
	public void Drop(){
		heldBy = null;
	}
	
	public void DropOnCounter(Node3D counter){
		heldBy = counter;
	}
}
