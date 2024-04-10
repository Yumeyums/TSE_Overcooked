using Godot;
using System;
public partial class itemScript : RigidBody3D
{
	[Export]
	public MeshInstance3D mesh;
	public Node3D heldBy;
	
	public override void _PhysicsProcess(double delta)
	{
		if (heldBy != null){
			if (heldBy.Name == "Plate"){
				this.Sleeping = true;
				GlobalPosition = heldBy.GlobalPosition + new Vector3 (0,1,0);
			}
			else if (heldBy.Name != "Counter")  {
				this.Sleeping = true;
				GlobalPosition = heldBy.GlobalPosition - heldBy.GlobalTransform.Basis.Z;
			}
		}
	}
	
	public Node3D getContainer(){
		if(heldBy != null){
			return heldBy;
		}
		else{
			return this;
		}
	}

	public void PickUp(Node3D carrier){
		if (heldBy != null){
			if (heldBy.Name == "Counter"){
				heldBy.Call("RemoveFromCounter");
			}
		}
		heldBy = carrier;
		this.Sleeping = true;
	}

	public void DropInto(Node3D into){
		heldBy = into;
	}

	public void Drop(){
		heldBy = null;
	}
}
