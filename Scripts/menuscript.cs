using Godot;
using System;



public partial class menuscript : Control
{
	
	private string PathName = "res://Scenes/MainScene.tscn";
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void ChangeScene()
	{
		
		var rootNode = GetTree().Root;
		
		foreach(var item in GetTree().Root.GetChildren()){
			GetTree().Root.RemoveChild(item);
			item.QueueFree();
		}
		
		var scene = GD.Load<PackedScene>(PathName);
		Node currentNode = scene.Instantiate();
		rootNode.AddChild(currentNode);
		QueueFree();
	}
	
	
	private void _on_play_pressed()
	{
		ChangeScene();
	}
	
	private void _on_quit_pressed()
	{
		GetTree().Quit(0);
	}
	
}




