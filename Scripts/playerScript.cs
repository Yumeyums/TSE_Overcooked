using Godot;
using System;

public partial class playerScript : CharacterBody3D
{
	[Export]
	public int Speed { get; set; } = 14;
	[Export]
	public int playerNumber;     // distinguishes between players
	private Vector3 _targetVelocity = Vector3.Zero;
	[Export]
	public Node3D targetObject = null;
	[Export]
	public Node3D carriedItem = null;
	[Export]
	public Godot.Collections.Array<Node3D> BodiesInRange = new Godot.Collections.Array<Node3D>();
	public Node3D targetNode;

	private void _on_area_3d_body_entered(Node3D body)
	{
		if (body.GetNode("Interactable") != null){
			GD.Print("entered ", body.GetParent().Name);
			BodiesInRange.Add(body);
		}
		/*
		double shortestDist = 100.0;
		for (int i = 0; i <BodiesInRange.Count;i++){
			double dist = Convert.ToDouble(BodiesInRange[i].Call("getDistance",this));

			if(dist < shortestDist){
				shortestDist = dist;
				targetNode = BodiesInRange[i];
				GD.Print(BodiesInRange.Count);
			}
		}
		*/
		targetNode = BodiesInRange[0];
	}
	
	private void _on_area_3d_body_exited(Node3D body)
	{
		if(BodiesInRange.IndexOf(body)!= -1){
			GD.Print("exited ", body.GetParent().Name);
			BodiesInRange.Remove(body);
		}
		targetNode = BodiesInRange[0];
	}

	public override void _PhysicsProcess(double delta)
	{
		
		var direction = Vector3.Zero;
		if (playerNumber==1){
			if (Input.IsActionPressed("move_right"))
			{
				direction.X += 1.0f;
			}
			if (Input.IsActionPressed("move_left"))
			{
				direction.X -= 1.0f;
			}
			if (Input.IsActionPressed("move_back"))
			{
				direction.Z += 1.0f;
			}
			if (Input.IsActionPressed("move_forward"))
			{
				direction.Z -= 1.0f;
			}
			if (Input.IsActionPressed("interact"))
			{
				InteractWith(BodiesInRange[0]);
			}
		}
		else if (playerNumber==2){
			if (Input.IsActionPressed("move_right2"))
			{
				direction.X += 1.0f;
			}
			if (Input.IsActionPressed("move_left2"))
			{
				direction.X -= 1.0f;
			}
			if (Input.IsActionPressed("move_back2"))
			{
				direction.Z += 1.0f;
			}
			if (Input.IsActionPressed("move_forward2"))
			{
				direction.Z -= 1.0f;
			}
			if (Input.IsActionPressed("interact2"))
			{
				InteractWith(BodiesInRange[0]);
			}
		}

		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized(); // gives veactor at diatnce of 1
			Basis = Basis.LookingAt(direction); // player looks at normalized direction
		}

		_targetVelocity.X = direction.X * Speed;
		_targetVelocity.Z = direction.Z * Speed;
		Velocity = _targetVelocity; // velocity is a property of CharcacterBody3D
		MoveAndSlide(); // uses velocity 
	}
	
	public Node3D getTargetNode(){
		return targetNode;
	}
	
	public void InteractWith(Node3D body){
		if (carriedItem == null){
			carriedItem = body;
			GD.Print("pickUp");
			carriedItem.Call("Interact",this);
		}
		/*else{
			carriedItem = null;
			GD.Print("Drop");
		}
		*/
		
	}
}








