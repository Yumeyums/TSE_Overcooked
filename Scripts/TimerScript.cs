using Godot;

public partial class TimerScript : Node
{
	private int seconds = 0;
	private int defaultSeconds = 60;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ResetTimer();
		GetNode<Timer>("TimerNodeName").Start();
	}

	// Called when the timer signals a timeout.
private void OnTimerTimeout()
{
	GetNode<Label>("TimeRemaining").Text = seconds.ToString();
	if (seconds > 0)
	{
		seconds--;
	}
}

	// Function to reset the timer.
	private void ResetTimer()
	{
		seconds = defaultSeconds;

	}
}
