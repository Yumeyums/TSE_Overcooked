using Godot;
using System;

public partial class CanChop : Node
{
	[Export]
	private int chopsNeeded = 10;
	private int chopsDone = 0;
	private bool Chopped = false;
	public void Chop()
	{
		if(chopsDone < chopsNeeded)
		{
			chopsDone++;
		}
		else
		{
			Chopped = false;
		}
	}
}
