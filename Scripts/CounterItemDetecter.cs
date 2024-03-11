using Godot;
using System;

public partial class CounterItemDetecter : Area3D
{
	[Export]
	private bool ItemIn = false;
	public Node3D itemOnCounter = null;
	
	private void _on_body_entered(RigidBody3D body)
	{
		if (body.GetNode("Interactable") != null && itemOnCounter != null)
		{
			body.Position = this.GlobalPosition - new Vector3(0f, 2.5f, 0f);
			body.Sleeping = true;
			itemOnCounter = body;
		}
	}
	
	public void RemoveFromCounter(){
		itemOnCounter = null;
	}
	
	public void DropItem(Node3D carriedItem){
		if (itemOnCounter == null){
			carriedItem.Call("DropOnCounter",this);
			carriedItem.Position = this.GlobalPosition - new Vector3(0f, 2.5f, 0f);
			itemOnCounter = carriedItem;
		}
	}
}



