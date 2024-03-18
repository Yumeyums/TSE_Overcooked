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
			carriedItem.Call("DropInto",this);
			carriedItem.GlobalPosition = this.GlobalPosition + new Vector3(0f, 0.5f, 0f);
			itemOnCounter = carriedItem;
		}
	}
	
	public Node3D PickUpFromCounter(Node3D player){
		GD.Print("kujwbrfku");
		if (itemOnCounter != null){
			GD.Print("ahh");
			itemOnCounter.Call("PickUp", player);
			Node3D temp = itemOnCounter;
			itemOnCounter = null;
			return temp;
		}
		return null;
	}
	
	public void _on_body_exited(RigidBody3D body)
	{
		if (body.GetNode("Interactable") != null && itemOnCounter != null)
		{
			 itemOnCounter = null;
		}
	}
}



