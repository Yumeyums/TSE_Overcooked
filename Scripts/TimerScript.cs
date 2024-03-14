using Godot;
using System;

public partial class TimerScript : Node
{
	private double seconds = 0;
	private int secondsInt;
	private int defaultSeconds = 60;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ResetTimer();
		//GetNode<Timer>("TimerNodeName").Start();
	}

	// Called when the timer signals a timeout.
public override void _Process(double delta)
{
	GetNode<Label>("TimeRemaining").Text = secondsInt.ToString();
	if (seconds > 0)
	{
		seconds -= delta;
		secondsInt = (int)seconds;
	}
}

	// Function to reset the timer.
	private void ResetTimer()
	{
		seconds = defaultSeconds;
	}
	
	private void _on_timer_timeout()
	{
		GD.Print("End");
	}
}
