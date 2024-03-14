using Godot;
using System;

public partial class HobScript : Area3D
{
	[Export]
	//private bool ItemIn = false;
	public Node3D itemOnCounter = null;
	
	private void _on_body_entered(RigidBody3D body)
	{
		//if (body.GetNode("Interactable") != null && ItemIn == false)
		if (body.GetNode("Interactable") != null && itemOnCounter != null)
		{
			body.Sleeping = true;
			body.GlobalPosition = this.GlobalPosition;
			//ItemIn = true;
			itemOnCounter = body;
			body.Call("Boil");
		}
	}
	
	public void RemoveFromCounter(){
		itemOnCounter = null;
	}

	public void DropItem(Node3D carriedItem){
		if (itemOnCounter == null){
			carriedItem.Call("DropOnCounter",this);
			carriedItem.GlobalPosition = this.GlobalPosition + new Vector3(0f, 1f, 0f);
			itemOnCounter = carriedItem;
		}
	}
}



