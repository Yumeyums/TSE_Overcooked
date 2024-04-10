using Godot;
using System;

public partial class AllCounterScript : Node3D
{
	[Export]
	public Node3D itemOnCounter = null;
	
	public void _on_body_entered(RigidBody3D body)
	{
		if (body.GetNode("Interactable") != null && itemOnCounter != null)
		{
			if(GetAllowed(body) == true){
				body.Sleeping = true;
				body.GlobalPosition = this.GlobalPosition;
				itemOnCounter = body;
			}
		}
	}

	public void RemoveFromCounter(){
		itemOnCounter = null;
	}

	public void DropItem(Node3D carriedItem){
		if (itemOnCounter == null){
			if(GetAllowed(carriedItem) == true){
				GD.Print("allowed");
				carriedItem.Call("DropInto",this);
				carriedItem.GlobalPosition = this.GlobalPosition + new Vector3(0f, 0.5f, 0f);
				itemOnCounter = carriedItem;
				itemOnCounter.GetNode("Interactable").Call("ChangeColour",true);
			}
		}
	}
	
	public Node3D GetItemOnCounter(){
		return itemOnCounter;
	}
	
	public bool GetAllowed(Node3D carriedItem){
		bool allowed = false;
			if(this.GetParent().Name == "MainScene"){ // normal counter
				allowed = true;
				GD.Print("on normal counter");
			}
			else if (this.GetParent().Name == "Hob"){ // hob counter
				if (carriedItem.GetParent().GetNode("CanOnHob") != null){
					allowed = true;
					GD.Print("on hob");
				}
			}
			else if (this.GetParent().Name == "ChoppingBoard"){ // chopping counter
				if (carriedItem.GetNode("CanChop") != null){
					allowed = true;
					GD.Print("on chop");
				}
			}
		return allowed;
	}
	
	public Node3D PickUpFromCounter(Node3D player){
		if (itemOnCounter != null){
			GD.Print(itemOnCounter.Name);
			Node3D temp = itemOnCounter;
			itemOnCounter.Call("PickUp", player);
			return temp;
		}
		return null;
	}
	
	public void _on_body_exited(RigidBody3D body)
	{
		if (body.GetNode("Interactable") != null && itemOnCounter != null)
		{
			 itemOnCounter = null;
		}
	}
}
