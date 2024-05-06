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
			if (heldBy.Name == "Container"){
				this.Sleeping = true;
				//GlobalPosition = heldBy.GlobalPosition + new Vector3 (0,1,0);
				Node3D body = (Node3D) heldBy.GetNode("RigidBody3D");
				int position = (int) heldBy.Call("GetPositionInItems",this) + 1;
				GlobalPosition = body.GlobalPosition + new Vector3 (0,1*position,0);
			}
			else if (heldBy.GetNode("counter") == null){
			//else if (heldBy.Name != "Counter")  {
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
			return null;
		}
	}
	
	public void PickUp(Node3D carrier){
		GD.Print("Pick Up: ",this.GetParent().Name);
		
		if (heldBy != null){
			GD.Print("From: ",heldBy.Name);
			if (heldBy.GetNode("counter") != null){
			//if (heldBy.Name == "Counter"){
				GD.Print("Here");
				heldBy.Call("RemoveFromCounter");
				heldBy = carrier;
				this.Sleeping = true;
				carrier.Call("setCarryItem",this);
			} 
			else if(heldBy.Name == "Container") {
				GD.Print("45 pick up methos, held by container, item script");
				carrier.Call("setCarryItem",heldBy);
				heldBy.GetNode("RigidBody3D").Call("PickUp", carrier);
				//heldBy.Call("PickUp", carrier);
			} 
		}
		else {
			
			heldBy = carrier;
			carrier.Call("setCarryItem",this);
			this.Sleeping = true;
		}
		//GD.Print(this.GetParent().Name, " is held by ", carrier.Name);
	}
		

	public void DropInto(Node3D into){
		heldBy = into;
	}

	public void Drop(){
		heldBy = null;
	}
	
}
