using Godot;
using System;

public partial class playerScript : CharacterBody3D
{
	[Export]
	public float Speed { get; set; } = 5;
	[Export]
	public int playerNumber;     // distinguishes between players
	private Vector3 _targetVelocity = Vector3.Zero;
	public float mspeed = 20.0f;			//Maximum speed the player can reach.
	public float accelleration = 1f;	//The amount of speed the player gains every second by moving.
	public float friction = 0.6f; 		//The amount of speed the player loses every second.
	[Export]
	public Node3D carriedItem = null;
	[Export]
	public Godot.Collections.Array<Node3D> BodiesInRange = new Godot.Collections.Array<Node3D>();
	public Node3D targetNode;
	[Export]
  public float DashSpeed { get; set; } = 2f; // dash speed
	private bool isDashing = false;
	private double dashTimer = 0;
	private double dashDuration = 0.3; 
	private float dash = 1;
	
	public int GetPlayerNumber(){
		return playerNumber;
	}
	
	public override void _Ready()
	{
		ResourceLoader.LoadThreadedRequest("res://Scenes/pasta.tscn");
	}
	
	public void _on_area_3d_body_entered(Node3D body)
	{
		if ((body.GetNode("Interactable") != null)&&(body != carriedItem)){
			if (carriedItem != null){
				if ((Node3D)body.Call("getContainer") != carriedItem){
					BodiesInRange.Add(body);
				}
			}
			else{
					BodiesInRange.Add(body);
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
		if(oldTarget != null) {oldTarget.GetNode("Interactable").Call("target",false,this);}
		if(newTarget != null) { 
			newTarget.GetNode("Interactable").Call("target",true,this);
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
			if (Input.IsActionJustPressed("interact"))
			{
				InteractWith();
			}
			if (Input.IsActionJustPressed("alt_interact"))
			{
				Chop();
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
				InteractWith();
			}
			if (Input.IsActionJustPressed("alt_interact"))
			{
				Chop();
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
					if((Speed+accelleration)<mspeed){ Speed += accelleration; } 
					else { 
						if(!isDashing){Speed = mspeed; }
						}
				}
		}
		if (Input.IsActionJustPressed("dash"))
				{
				if (!isDashing)
				{
					isDashing = true;
					dashTimer = dashDuration;
					dash = DashSpeed;
				}
			}
		
		if (isDashing)
		{
			dashTimer -= delta;
			if (dashTimer <= 0)
			{
				isDashing = false;
				_targetVelocity = Vector3.Zero;
				dash = 1;
			}
			//else{
			//	_targetVelocity = forwardDirection.Normalized() * DashSpeed;
			//}
		}
		else{

		if((Speed-friction)<=0){
			Speed = 0;
		} else {
			Speed -= friction;
		}
		}

		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
			direction = direction.Normalized();
			Basis = Basis.LookingAt(direction);
		}

		_targetVelocity.X = direction.X * Speed * dash;
		_targetVelocity.Z = direction.Z * Speed * dash;
		Velocity = _targetVelocity;
		MoveAndSlide();
		
		double smallestDist = 100;
		Node3D smallest = targetNode;
		for (int i = 0; i<BodiesInRange.Count;i++){
			double dist = (double) BodiesInRange[i].GetNode("Interactable").Call("getDistance",this);
			if (dist < smallestDist){
				smallest = BodiesInRange[i];
				smallestDist = dist;
			}
		}	

		if(targetNode != smallest){
			if (smallest != carriedItem)
			{
				changeTarget(smallest, targetNode);
			}
		}	
	}

	public void setCarryItem (Node3D item){
		carriedItem = item;
	}
	
	public void InteractWith(){
		GD.Print("Target node: ", targetNode);
		GD.Print("caried node: ", carriedItem);
		if(carriedItem == null){ //pick up
			GD.Print("Target name: ", targetNode.GetParent().Name);
			if (targetNode.GetParent().GetNode("counter") != null){
				Node3D item = (Node3D) targetNode.GetParent().Call("PickUpFromCounter",this);
				if(item != null){
					carriedItem = item;
					removeTargettedItem(carriedItem);
					carriedItem.GetNode("Interactable").Call("ChangeColour",false,this);
				}
			}
			else{
				//carriedItem = targetNode;
				targetNode.Call("PickUp",this);
				removeTargettedItem(targetNode);
			}
			removeTargettedItem(carriedItem);
		}
		else{
			GD.Print("woop: ", carriedItem.GetParent().Name);
			carriedItem.Call("Drop");
			if (targetNode != null){
				GD.Print("Target name: ", targetNode.GetParent().Name);
				if (targetNode.GetParent().GetNode("counter") != null){
					targetNode.GetParent().Call("DropItem",carriedItem,this);
					carriedItem.GetNode("Interactable").Call("ChangeColour",true,this);
					BodiesInRange.Add(carriedItem);
				}
				else if(targetNode.GetParent().Name == "Container"){
					targetNode.GetParent().Call("AddToContainer",carriedItem);
				}
			}
			else{
				BodiesInRange.Add(carriedItem);
			}
			carriedItem = null;
		}
	}
	
	public void Chop()
	{
		if (targetNode.GetParent().GetParent().Name == "choppingBoard")
		{
			targetNode.GetParent().GetNode("Area3D").Call("CounterChop");
		}
	}
/*
	public void giveItem()
	{
		PackedScene pastacopies = GD.Load<PackedScene>("res://Scenes/pasta.tscn");
		Node3D pasta = pastacopies.Instantiate<Node3D>();
		this.GetParent().GetParent().AddChild(pasta);
		carriedItem = pasta;
		carriedItem.Call("PickUp", this);
	}*/
}




