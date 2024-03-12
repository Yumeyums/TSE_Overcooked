using Godot;
using System;

public partial class CounterScript : Area3D
{
	[Export]
	private bool ItemIn = false;
	
	public void _on_body_entered(RigidBody3D body)
	{
		if (body.GetNode("Interactable") != null && ItemIn == false)
		{
			body.Sleeping = true;
			body.GlobalPosition = this.GlobalPosition;
			ItemIn = true;
			
			GD.Print("Contact", this.GlobalPosition);
		}
	}
	
	public void _on_body_exited(RigidBody3D body)
	{
		
		GD.Print("it's off");
		if (body.GetNode("Interactable") != null && ItemIn == false)
		{
			ItemIn = false;
		}
	}
}



