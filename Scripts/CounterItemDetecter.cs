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

			body.Position = this.GlobalPosition - new Vector3(0f, 2.5f, 0f);
			body.Sleeping = true;
			ItemIn = true;
			
			GD.Print("Contact");
			
		}
	}
}



