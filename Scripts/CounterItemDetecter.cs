using Godot;
using System;

public partial class CounterItemDetecter : Area3D
{
	[Export]
	private bool ItemIn = false;
	
	private void _on_body_entered(RigidBody3D body)
	{
		if (body.GetNode("Interactable") != null && ItemIn == false)
		{
			body.Position = this.GlobalPosition;
			body.Sleeping = true;
			ItemIn = true;
			
			GD.Print("Contact");
		}
	}
}



