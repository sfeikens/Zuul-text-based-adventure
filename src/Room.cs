using System.Collections.Generic;
using System.Linq;

class Room
{
	// Private fields
	private string description;
	private Dictionary<string, Room> exits; // stores exits of this room.
	private Inventory chest; // stores items in this room.

	// Property
	public Inventory Chest
	{
		get { return chest; }
	}

	// Create a room described "description". Initially, it has no exits.
	// "description" is something like "in a kitchen" or "in a court yard".
	public Room(string desc)
	{
		description = desc;
		exits = new Dictionary<string, Room>();
		chest = new Inventory(999999); // een Room kan veel items bevatten
	}

	// Define an exit for this room.
	public void AddExit(string direction, Room neighbor)
	{
		exits.Add(direction, neighbor);
	}

	// Add an item to this room.
	public void AddItem(string description, int weight)
	{
    	chest.Put(description, new Item(weight, description));
	}

	// Return the description of the room.
	public string GetShortDescription()
	{
		return description;
	}

	// Return a long description of this room, in the form:
	//     You are in the kitchen.
	//     Exits: north, west
	//     Items: sword, shield
	public string GetLongDescription()
	{
		string str = "You are ";
		str += description;
		str += ".\n";
		str += GetExitString();
		str += "\n";
		str += GetItemString();
		return str;
	}

	// Return the room that is reached if we go from this room in direction
	// "direction". If there is no room in that direction, return null.
	public Room GetExit(string direction)
	{
		if (exits.ContainsKey(direction))
		{
			return exits[direction];
		}
		return null;
	}

	// Return a string describing the room's exits, for example
	// "Exits: north, west".
	private string GetExitString()
	{
		string str = "Exits: ";
		str += String.Join(", ", exits.Keys);

		return str;
	}

	// Return a string describing the room's items, for example
	// "Items: sword, shield".
	private string GetItemString()
	{
		return "Items: " + chest.Show();
	}

	public void RemoveItem(Item item)
	{
		chest.Get(item.Description);
	}

	public Item GetItem(string itemName)
	{
		return chest.Peek(itemName);
	}
}
