using Godot;
using System;

public partial class playerScript : CharacterBody3D
{
	[Export]
	public int Speed { get; set; } = 14;
	[Export]
	public int playerNumber;     // distinguishes between players

	private Vector3 _targetVelocity = Vector3.Zero;

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
				//if (Area3D.get_overlapping_bodies() != null)
				//{
					GD.Print("interact 1");   // start of code to interact with objects using Area3D, not done yet
				//}
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
