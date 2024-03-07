using Godot;
using System;

public partial class plateScript : Node3D
{
	public Godot.Collections.Array<Node3D> items = new Godot.Collections.Array<Node3D>();
	
	private void _on_body_entered(RigidBody3D body)
	{
		if(body.GetNode("Ingredient") != null)
		{
			items.Add(body);
			GD.Print(body.GetParent().Name, " on plate");
		}
	}
	
	private void _on_body_exited(RigidBody3D body)
	{
		if(body.GetNode("Ingredient") != null)
		{
			items.Add(body);
			GD.Print(body.GetParent().Name, " off plate");
		}
	}
}
