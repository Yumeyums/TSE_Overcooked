using Godot;
using System;

public partial class AllCounterScript : Node3D
{
	[Export]
	public Node3D itemOnCounter = null;
	[Export]
	public string itemFilePath = "res://Scenes/pasta.tscn";
	
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
		GD.Print(this.GetParent().Name);
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
			else if (this.GetParent().Name == "choppingBoard"){ // chopping counter
				GD.Print("ahhg");
				if (carriedItem.GetNode("CanChop") != null){
					allowed = true;
					GD.Print("on chop");
				}
			}
		return allowed;
	}
	
	public Node3D PickUpFromCounter(Node3D player){
		if(this.GetParent().Name == "itemDispenser"){
			PackedScene itemcopies = GD.Load<PackedScene>(itemFilePath);
			Node3D item = itemcopies.Instantiate<Node3D>();
			item.GlobalPosition = player.GlobalPosition - player.GlobalTransform.Basis.Z;;
			this.GetParent().GetParent().AddChild(item);
			player.Call("setCarryItem",item.GetNode("RigidBody3D"));
			item.GetNode("RigidBody3D").Call("PickUp", player);
		}
		else{
			if (itemOnCounter != null){
				GD.Print(itemOnCounter.Name);
				Node3D temp = itemOnCounter;
				itemOnCounter.Call("PickUp", player);
				return temp;
			}
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
