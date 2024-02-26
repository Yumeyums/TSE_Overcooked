using Godot;
using System;

public partial class playerScript : CharacterBody3D
{
	[Export]
	public float Speed { get; set; } = 14;
	[Export]
	public int playerNumber;     // distinguishes between players
	private Vector3 _targetVelocity = Vector3.Zero;
	public float mspeed = 20.0f;			//Maximum speed the player can reach.
	public float accelleration = 1.25f;	//The amount of speed the player gains every second by moving.
	public float friction = 0.60f; 		//The amount of speed the player loses every second.
	[Export]
	public Node3D targetObject = null;
	[Export]
	public Node3D carriedItem = null;
	[Export]
	public Godot.Collections.Array<Node3D> BodiesInRange = new Godot.Collections.Array<Node3D>();
	public Node3D targetNode;
	
public void _on_area_3d_body_entered(Node3D body)
{
	if (body.GetNode("Interactable") != null){
			GD.Print("entered ", body.GetParent().Name);
			BodiesInRange.Add(body);
		}
		/*
		double shortestDist = 100.0;

		/*double shortestDist = 100.0;
		for (int i = 0; i <BodiesInRange.Count;i++){
			double dist = Convert.ToDouble(BodiesInRange[i].Call("getDistance",this));
			//double dist = BodiesInRange[i].Call("getDistance",this);
			if(dist < shortestDist){
				shortestDist = dist;
				targetNode = BodiesInRange[i];
				GD.Print(BodiesInRange.Count);
			}
			*/
		//}

		//GD.Print(body.Call("getDistance",this));
		targetNode = BodiesInRange[0];
}


public void _on_area_3d_body_exited(Node3D body)
{
		if(BodiesInRange.IndexOf(body)!= -1){
			GD.Print("exited ", body.GetParent().Name);
			BodiesInRange.Remove(body);
		}
		targetNode = BodiesInRange[0];
}
	
	public bool isTargetNode(Node3D node){
		if (node == BodiesInRange[0]){
			return true;
		}
		return false;
		/*
		bool contained = false;
		for (int i = 0; i <BodiesInRange.Count;i++){
			if (BodiesInRange[i] == node){
				contained = true;
			}
		}
		return contained;
		*/
	}
	


	public override void _PhysicsProcess(double delta)
	{
		var direction = Vector3.Zero;
		if (playerNumber==1){
			if (Input.IsActionPressed("move_right"))
			{
				direction.X += 1.0f; //Sets the X axis direction to positive
				//Formula for speed calculation:
			}
			if (Input.IsActionPressed("move_left"))
			{
				direction.X -= 1.0f; //Sets the X axis direction to negative
			}
			if (Input.IsActionPressed("move_back"))
			{
				direction.Z += 1.0f; //Sets the Z axis direction to positive
			}
			if (Input.IsActionPressed("move_forward"))
			{
				direction.Z -= 1.0f;  //Sets the Z axis direction to negative
			}
			if (Input.IsActionPressed("interact"))
			{
				InteractWith(BodiesInRange[0]);
			}
						if (!Input.IsActionPressed("move_forward") 	&&
				!Input.IsActionPressed("move_back")		&&
				!Input.IsActionPressed("move_left")		&&
				!Input.IsActionPressed("move_right")	)
				{	//If no movement buttons are being pressed
					if((Speed-friction)<=0){	//Speed depletes twice as fast.
						Speed = 0;
					} else {
						Speed -= friction;
					}
				} else { //Otherwise we accellerate
					// First we check that the current speed + the acceleration doesn't exceed the max speed.
					// if it doesn't we increase the speed as normal via acceleration.
					// otherwise; we set the speed to the maximum speed.
					if((Speed+accelleration)<mspeed){ Speed += accelleration; } else { Speed = mspeed; }
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
			if (!Input.IsActionPressed("move_forward2") 	&&
				!Input.IsActionPressed("move_back2")		&&
				!Input.IsActionPressed("move_left2")		&&
				!Input.IsActionPressed("move_right2")	)
				{	//If no movement buttons are being pressed
					if((Speed-friction)<=0){	//Speed depletes twice as fast.
						Speed = 0;
					} else {
						Speed -= friction;
					}
				} else { //Otherwise we accellerate
					// First we check that the current speed + the acceleration doesn't exceed the max speed.
					// if it doesn't we increase the speed as normal via acceleration.
					// otherwise; we set the speed to the maximum speed.
					if((Speed+accelleration)<mspeed){ Speed += accelleration; } else { Speed = mspeed; }
				}
		}

		if((Speed-friction)<=0){
			Speed = 0;
		} else {
			Speed -= friction;
		}

		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
			direction = direction.Normalized();
			Basis = Basis.LookingAt(direction);
		}

		_targetVelocity.X = direction.X * Speed;
		_targetVelocity.Z = direction.Z * Speed;
		Velocity = _targetVelocity;
		MoveAndSlide();
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




