using Godot;
using System;
public partial class itemScript : RigidBody3D
{
	[Export]
	public MeshInstance3D mesh;
	public Node3D heldBy;
	
	public override void _PhysicsProcess(double delta)
	{
		if (heldBy != null && heldBy.GetParent().Name != "Counter"){
			this.Sleeping = true;
			GlobalPosition = heldBy.GlobalPosition - heldBy.GlobalTransform.Basis.Z;
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
	
	public void DropInto(Node3D into){
		heldBy = into;
	}

	public void Drop(){
		heldBy = null;
	}

	public void DropOnCounter(Node3D counter){
		heldBy = counter;
	}
}
