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

	public void Interact(Node3D player, bool handsEmpty){
		if (handsEmpty == true){
			GD.Print("yoink");
			heldBy = player;
			GlobalPosition = player.GlobalPosition + new Vector3(0f,1.5f,0f);
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
