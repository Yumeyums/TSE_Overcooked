using Godot;
using System;

public partial class CounterScript : Area3D
{
	public Node3D itemOnCounter = null;
	
	public void _on_body_entered(RigidBody3D body)
	{
		if (body.GetNode("Interactable") != null && itemOnCounter != null)
		{
			itemOnCounter = body;
			body.Sleeping = true;
			body.GlobalPosition = this.GlobalPosition;
			//GD.Print("Contact", this.GlobalPosition);
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
		if (itemOnCounter != null){
			Node3D temp = itemOnCounter;
			itemOnCounter.Call("PickUp", player);
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
	
	public void CounterChop()
	{
		itemOnCounter.Call("Chopping");
	}
	
	public void SubmitFood()
	{
		
	}
}



