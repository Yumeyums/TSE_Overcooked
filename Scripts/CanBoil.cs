using Godot;
using System;

public partial class CanBoil : Node3D
{
	public bool boiled = false;
	public bool spoilt = false;
	
	public int boilTime = 10;
	public int burnTime = 10;
	private int cookTime = 0;
	private int realTime;
	
	public override void _Process(double delta)
	{
		realTime = (int)delta;
	}
	
	
	public void boiling()
	{
		cookTime += realTime;
		
		if(cookTime == boilTime)
		{
			boiled = true;
		}
		if(cookTime >= (boilTime + burnTime))
		{
			spoilt = true;
		}
	}
}
