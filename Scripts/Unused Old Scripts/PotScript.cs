using Godot;
using System;

public partial class PotScript : Node3D
{
	public Node3D itemOnHob;
	
	public void Boil()
	{
		if (itemOnHob != null)
		{
			itemOnHob.GetNode("CanBoil").Call("boiling");
		}
	}
	
	private void _on_body_entered(Node3D body)
	{
		if(body.GetNode("CanBoil") != null)
		{
			itemOnHob = body;
		}
	}
	
	private void _on_body_exited(Node3D body)
	{
		if(body.GetNode("CanBoil") != null)
		{
			itemOnHob = null;
		}
	}
}
