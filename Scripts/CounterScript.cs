using Godot;
using System;

public partial class CounterScript : Area3D
{
	[Export]
	public Node3D itemOnCounter = null;
	
	public void _on_body_entered(RigidBody3D body)
	{
		if (body.GetNode("Interactable") != null && itemOnCounter != null)
		{
			body.Sleeping = true;
			body.GlobalPosition = this.GlobalPosition;
			GD.Print("Contact", this.GlobalPosition);
			itemOnCounter = body;
		}
	}

	public void RemoveFromCounter(){
		itemOnCounter = null;
	}

	public void DropItem(Node3D carriedItem){
		if (itemOnCounter == null){
			carriedItem.Call("DropOnCounter",this);
			carriedItem.GlobalPosition = this.GlobalPosition + new Vector3(0f, 0.5f, 0f);
			itemOnCounter = carriedItem;
		}
	}
	
	public void _on_body_exited(RigidBody3D body)
	{
		
		GD.Print("it's off");
		if (body.GetNode("Interactable") != null && itemOnCounter != null)
		{
			 itemOnCounter = null;
		}
	}
}



