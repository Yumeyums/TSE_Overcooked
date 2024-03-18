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
	[Export]
  public float DashSpeed { get; set; } = 30f; // dash speed
	private bool isDashing = false;
	private double dashTimer = 0;
	private double dashDuration = 0.1; 
	
	public void _on_area_3d_body_entered(Node3D body)
	{
		if (body.GetNode("Interactable") != null){
			//if (carriedItem==null){
				//if(body.GetParent().Name != "counter"){
					BodiesInRange.Add(body);
					if(targetNode != BodiesInRange[0]){
						changeTarget(BodiesInRange[0], targetNode);
					}
				//}
				/*
			}
			else{
				BodiesInRange.Add(body);
				if(targetNode != BodiesInRange[0]){
					changeTarget(BodiesInRange[0], targetNode);
				}
			}
			*/
			GD.Print("entered ", body.GetParent().Name);
		}
	}

	public void _on_area_3d_body_exited(Node3D body)
	{
		if(BodiesInRange.IndexOf(body)!= -1){
			GD.Print("exited ", body.GetParent().Name);
			BodiesInRange.Remove(body);
			if (BodiesInRange.Count == 0){
				changeTarget(null, targetNode);
			}
			else{
				if(targetNode != BodiesInRange[0]){
					changeTarget(BodiesInRange[0], targetNode);
				}
			}
		}
	}

	public void changeTarget(Node3D newTarget, Node3D oldTarget){
		if(oldTarget != null) {oldTarget.GetNode("Interactable").Call("untargeted");}
		if(newTarget != null) { 
			newTarget.GetNode("Interactable").Call("targeted");
			targetNode = newTarget;
			}
		else{
			targetNode = null;
			}
		}

	public override void _PhysicsProcess(double delta)
	{
		var direction = Vector3.Zero;

 var characterRotation = Rotation;


 var forwardDirection = characterRotation.Rotated(Vector3.Up, 0) * -Vector3.Forward;


	if (Input.IsActionJustPressed("dash"))
	{
		if (!isDashing)
		{
			isDashing = true;
			dashTimer = dashDuration;



		_targetVelocity = forwardDirection.Normalized() * DashSpeed;
		}
	}

	
	if (isDashing)
	{
		dashTimer -= delta;
		if (dashTimer <= 0)
		{
			isDashing = false;
	
			_targetVelocity = Vector3.Zero;
		}
	}

		
		
		if (playerNumber==1)
		{
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
			
			if (Input.IsActionJustPressed("interact"))
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
			if (Input.IsActionJustPressed("interact2"))
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

  if (!isDashing)
		{
			if ((Speed - friction) <= 0)
			{
				Speed = 0;
			}
			else
			{
				Speed -= friction;
			}
		}
		else
		{
			dashTimer -= delta;
			if (dashTimer <= 0)
			{
				isDashing = false;
			}
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
	
	public void InteractWith(Node3D body){
		GD.Print("called");
		if (body.GetParent().Name != "counter"){
			if(carriedItem == null){
				body.Call("Interact",this, true); //emptyHands
				carriedItem = body;
			}
			else{
				body.Call("Interact",this, false); //not emptyHands
				carriedItem = null;
			}
		}
		
	}
	}
	


	
	
	






