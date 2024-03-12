using Godot;
using System;

public partial class HobScript : Area3D
{
	[Export]
	private bool ItemIn = false;
	
	private void _on_body_entered(RigidBody3D body)
	{
		if (body.GetNode("Interactable") != null && ItemIn == false)
		{
			body.GlobalPosition = this.GlobalPosition;
			body.Sleeping = true;
			ItemIn = true;
			body.Call("Boil");
		}
	}
}



