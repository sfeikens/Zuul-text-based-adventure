using System.Collections.Generic;
using System.Linq;

class Room
{
	// Private fields
	private string description;
	private Dictionary<string, Room> exits; // stores exits of this room.
	private List<Item> items; // stores items in this room.

	// Create a room described "description". Initially, it has no exits.
	// "description" is something like "in a kitchen" or "in a court yard".
	public Room(string desc)
	{
		description = desc;
		exits = new Dictionary<string, Room>();
		items = new List<Item>();
	}

	// Define an exit for this room.
	public void AddExit(string direction, Room neighbor)
	{
		exits.Add(direction, neighbor);
	}

	// Add an item to this room.
	public void AddItem(string description, int weight)
	{
    	items.Add(new Item(weight, description));
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
		if (items.Count == 0)
		{
			return "Items: none";
		}
		string str = "Items: ";
		str += string.Join(", ", items.Select(i => i.Description));
		return str;
	}
}
