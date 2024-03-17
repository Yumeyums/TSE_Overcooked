using Godot;
using System;

public partial class chopping_board : Area3D
{
	[Export]
	public Node3D itemOnBoard = null;
	
	public void _on_body_entered(RigidBody3D body)
	{
		if (body.GetNode("Interactable") != null && itemOnBoard != null)
		{
			body.Sleeping = true;
			body.GlobalPosition = this.GlobalPosition;
			GD.Print("Contact", this.GlobalPosition);
			itemOnBoard = body;
		}
	}

	public void RemoveFromCounter(){
		itemOnBoard = null;
	}

	public void DropItem(Node3D carriedItem){
		if (itemOnBoard == null){
			carriedItem.Call("DropOnCounter",this);
			carriedItem.GlobalPosition = this.GlobalPosition + new Vector3(0f, 0.5f, 0f);
			itemOnBoard = carriedItem;
		}
	}
	
	public void _on_body_exited(RigidBody3D body)
	{
		GD.Print("it's off");
		if (body.GetNode("Interactable") != null && itemOnBoard != null)
		{
			 itemOnBoard = null;
		}
	}
	
	public void Chopping()
	{
		if (itemOnBoard != null)
		{
			itemOnBoard.GetNode("CanChop").Call("chopping");
			GD.Print("Chopping");
		}
	}
}
