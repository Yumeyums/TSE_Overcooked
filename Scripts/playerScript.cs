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
	public Node3D carriedItem = null;
	[Export]
	public Godot.Collections.Array<Node3D> BodiesInRange = new Godot.Collections.Array<Node3D>();
	public Node3D targetNode;
	
	public void _on_area_3d_body_entered(Node3D body)
	{
		if ((body.GetNode("Interactable") != null)&&(body != carriedItem)){
			BodiesInRange.Add(body);
			if(targetNode != BodiesInRange[0]){
				changeTarget(BodiesInRange[0], targetNode);
			}
		}
	}

	public void _on_area_3d_body_exited(Node3D body)
	{
		removeTargettedItem(body);
	}
	
	public void removeTargettedItem(Node3D body){
		if(BodiesInRange.IndexOf(body)!= -1){
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
		if (playerNumber==1){
			if (Input.IsActionPressed("move_right"))
			{
				direction.X += 1.0f; //Sets the X axis direction to positive
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
				InteractWith();
			}
			if (!Input.IsActionPressed("move_forward") 	&&
				!Input.IsActionPressed("move_back")		&&
				!Input.IsActionPressed("move_left")		&&
				!Input.IsActionPressed("move_right"))
			{	//If no movement buttons are being pressed
				if((Speed-friction)<=0){	//Speed depletes twice as fast.
					Speed = 0;
				} else {
					Speed -= friction;
				}
			} 
			else { //Otherwise we accellerate
				// First we check that the current speed + the acceleration doesn't exceed the max speed.
				// if it doesn't we increase the speed as normal via acceleration.
				// otherwise; we set the speed to the maximum speed.
				if((Speed+accelleration)<mspeed){ Speed += accelleration; } 
				else { Speed = mspeed; }
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
				InteractWith();
			}
			if (!Input.IsActionPressed("move_forward2") 	&&
				!Input.IsActionPressed("move_back2")		&&
				!Input.IsActionPressed("move_left2")		&&
				!Input.IsActionPressed("move_right2")	)
			{	//If no movement buttons are being pressed
				if((Speed-friction)<=0){	//Speed depletes twice as fast.
					Speed = 0;
				} 
				else {
					Speed -= friction;
				}
			}
			else { //Otherwise we accellerate
				// First we check that the current speed + the acceleration doesn't exceed the max speed.
				// if it doesn't we increase the speed as normal via acceleration.
				// otherwise; we set the speed to the maximum speed.
				if((Speed+accelleration)<mspeed){ Speed += accelleration; } else { Speed = mspeed; }
			}
		}

		if((Speed-friction)<=0){
			Speed = 0;
		} 
		else {
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
	
	public void InteractWith(){
		if(carriedItem == null){ //pick up
			carriedItem = targetNode;
			targetNode.Call("PickUp",this);
			removeTargettedItem(targetNode);
		}
		else { //drop
			carriedItem.Call("Drop");
			if (targetNode != null){
				if (targetNode.GetParent().Name == "Counter"){
					targetNode.GetParent().GetNode("Area3D").Call("DropItem",carriedItem);
				}
			}
			carriedItem = null;
		}
	}
}




