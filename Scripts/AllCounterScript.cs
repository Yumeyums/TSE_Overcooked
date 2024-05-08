using Godot;
using System;

public partial class AllCounterScript : Node3D
{
	[Export]
	public Node3D itemOnCounter = null;
	[Export]
	public string itemFilePath = "res://Scenes/pasta.tscn";

private void _on_area_3d_body_entered(RigidBody3D body)
{
		if (body.GetNode("Interactable") != null && itemOnCounter == null)
		{
			if(GetAllowed(body) == true){
				Node3D C = (Node3D) body.Call("getContainer");
				if (C != null){
					if((C.Name == "Player1")||(C.Name == "Player2")){
						C.Call("setCarryItemNull");
					}
				}
				body.Sleeping = true;
				DropItem(body,C);
			}
		}
	}

	public void RemoveFromCounter(){
		itemOnCounter = null;
	}

	public void DropItem(Node3D carriedItem, Node3D player){
		if (itemOnCounter == null){
			if(GetAllowed(carriedItem) == true){
				carriedItem.Call("DropInto",this);
				carriedItem.GlobalPosition = this.GlobalPosition + new Vector3(0f, 0.5f, 0f);
				itemOnCounter = carriedItem;
				itemOnCounter.GetNode("Interactable").Call("ChangeColour",true, player);
			}
		}
	}
	
	public Node3D GetItemOnCounter(){
		return itemOnCounter;
	}

	public bool GetAllowed(Node3D carriedItem){
		bool allowed = false;
			if (this.GetNode("hob") != null){ // hob counter
				if (carriedItem.GetNode("CanOnHob") != null){
					allowed = true;
					GD.Print("on hob");
				}
			}
			else if (this.GetNode("chop") != null){ // chopping counter
				if (carriedItem.GetNode("CanChop") != null){
					allowed = true;
					GD.Print("on chop");
				}
			}
			else if(this.GetNode("finish") != null){ // normal counter
				allowed = true;
				getOrder(carriedItem);
				GD.Print("on finishedCounter counter");
				}
			else{ // normal counter
				if(this.GetNode("itemDispenser") == null){
					allowed = true;
					GD.Print("on normal counter");
				}
			}
		return allowed;
	}
	
	public Node3D PickUpFromCounter(Node3D player){
		if(this.GetNode("StaticBody3D").GetNode("itemDispenser") != null){
			PackedScene itemcopies = GD.Load<PackedScene>(itemFilePath);
			Node3D item = itemcopies.Instantiate<Node3D>();
			item.GlobalPosition = player.GlobalPosition - player.GlobalTransform.Basis.Z;;
			this.GetParent().GetParent().AddChild(item);
			player.Call("setCarryItem",item.GetNode("RigidBody3D"));
			item.GetNode("RigidBody3D").Call("PickUp", player);
		}
		else{
			if (itemOnCounter != null){
				Node3D temp = itemOnCounter;
				itemOnCounter.Call("PickUp", player);
				return temp;
			}
		}
		return null;
	}
	
	public void _on_body_exited(RigidBody3D body)
	{
		if (body == itemOnCounter)
		{
			itemOnCounter = null;
		}
	}
	
	private void getOrder(Node3D iOC)
	{
		GD.Print("object name: ", iOC.Name);
		if (iOC.GetParent().GetNode("plate") != null)
		{
			GetParent().GetParent().GetParent().GetNode("Control").Call("getOrders", iOC, this);
		}
	}
	
	public void takeFood(Node3D iOC)
	{
		iOC.GetParent().Call("Delete");
	}
}
