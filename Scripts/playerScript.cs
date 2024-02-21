using Godot;
using System;

public partial class playerScript : CharacterBody3D
{
	[Export]
	public float Speed { get; set; } = 0;
	[Export]
	public int playerNumber;     // distinguishes between players
	public float mspeed = 20.0f;			//Maximum speed the player can reach.
	public float accelleration = 1.25f;	//The amount of speed the player gains every second by moving.
	public float friction = 0.60f; 		//The amount of speed the player loses every second.
	
	
	private Vector3 _targetVelocity = Vector3.Zero;

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
				direction.Z -= 1.0f; //Sets the Z axis direction to negative
				
			}
			if (Input.IsActionPressed("interact"))
			{
				//if (Area3D.get_overlapping_bodies() != null)
				//{
					GD.Print("interact 1");   // start of code to interact with objects using Area3D, not done yet
				//}
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
				//if (Area3D.get_overlapping_bodies() != null)
				//{
					GD.Print("interact 2");
				//}
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
}
