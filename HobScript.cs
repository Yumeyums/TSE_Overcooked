using Godot;
using System;

public partial class HobScript : CounterScript
{
	public void _on_body_entered(RigidBody3D body)
	{
		if (body.GetNode("Interactable") != null && itemOnCounter != null)
		{
			body.Call("Boil");
		}
	}
}
