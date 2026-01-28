using System.Collections.Generic;
using System.Linq;

class Room
{
	// Private fields
	private string name;
	private string description;
	private string floor;
	private Dictionary<string, Room> exits; // stores exits of this room.
	public static List<string> Rooms = new List<string>();
	private Inventory chest; // stores items in this room.
	private Enemy _enemy;
	private Structure structure;
	public List<Structure> structures = new List<Structure>();

	public bool islocked;

	public Inventory Chest
	{
		get { return chest; }
	}

	public Enemy enemy => _enemy;

	// Create a room described "description". Initially, it has no exits.
	// "description" is something like "in a kitchen" or "in a court yard".
	public Room(string name, string desc, string floor)
	{
		this.name = name;
		description = desc;
		this.floor = floor;
		Rooms.Add(name);
		exits = new Dictionary<string, Room>();
		chest = new Inventory(999999); // een Room kan veel items bevatten
		islocked = false;
	}

	// Define an exit for this room.
	public void AddExit(string direction, Room neighbor)
	{
		exits.Add(direction, neighbor);
	}

	// Add an item to this room.
	public void AddItem(Item item)
	{
    	chest.Put(item.Description, item);
	}

	// Return the description of the room.
	public string GetShortDescription()
	{
		return description;
	}

	public void AddLock()
	{
		islocked = true;
	}

	public void RemoveLock()
	{
		islocked = false;
	}

	public void AddEnemy(int health, string description, int damage)
	{
		_enemy = new Enemy(health, description, this, damage);
	}

	public void RemoveEnemy()
	{
		_enemy = null;
	}

	public bool HasEnemy => _enemy != null;

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

	public string[] GetExitStringArray()
	{
		return exits.Keys.ToArray();
	}

	public string GetFloor()
	{
		return this.floor;
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

	public string GetRoomName()
	{
		return this.name;
	}

	public string[] GetRoomNames()
	{
		return Rooms.ToArray();
	}
	public void AddStructure(Structure structure)
	{
		this.structure = structure;
		structures.Add(structure);
	}
	public string[] GetStructureNames()
	{
		return structures.Select(s => s.GetStructureName()).ToArray();
	}
	public string GetStructureName(string structureName)
	{
		Structure foundStructure = structures.FirstOrDefault(s => s.GetStructureName() == structureName);
		if (foundStructure != null)
		{
			return foundStructure.GetStructureName();
		}
		return null;
	}
}