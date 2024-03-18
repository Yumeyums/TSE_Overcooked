using Godot;
using System;

public partial class item_dispenser : StaticBody3D
{
	public void PickUp(Node3D player)
	{
		GD.Print("PickUp called");
		player.Call("giveItem");
	}
}
