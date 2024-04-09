using Godot;
using System;

public partial class item_dispenser : StaticBody3D
{
	[Export]
	public string itemFilePath = "res://Scenes/pasta.tscn";
	
	public void PickUp(Node3D player)
	{
		PackedScene itemcopies = GD.Load<PackedScene>(itemFilePath);
		Node3D item = itemcopies.Instantiate<Node3D>();
		this.GetParent().GetParent().AddChild(item);
		
		item.GetNode("RigidBody3D").Call("PickUp", player);
		
		GD.Print("PickUp called");
		player.Call("giveItem", item);
	}
}
