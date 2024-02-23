using Godot;
using System;

public partial class CounterItemDetecter : Area3D
{
	private void _on_body_entered(Node3D body)
	{
		if (body.Name == "Player1" || body.Name == "Player2")
		{
			GD.Print("Contact");
			body.Position = this.Position;
		}
	}
}



