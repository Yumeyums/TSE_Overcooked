using Godot;
using System;

public partial class PotScript : Node3D
{
	public Node3D itemIn;
	
	public override void _Ready()
	{
		
	}

	public override void _Process(double delta)
	{
		
	}
	
	public void Boil()
	{
		if (itemIn != null)
		{
			itemIn.GetNode("CanBoil").Call("boiling");
		}
	}
	
	private void _on_body_entered(RigidBody3D body)
	{
		if(body.GetNode("CanBoil") != null)
		{
			itemIn = body;
		}
	}
	
	private void _on_body_exited(RigidBody3D body)
	{
		if(body.GetNode("CanBoil") != null)
		{
			itemIn = null;
		}
	}
}
