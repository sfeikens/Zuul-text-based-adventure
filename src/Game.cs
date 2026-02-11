using System;
using System.Collections.Generic;
class Game
{
	// Private fields
	private Parser parser;
	private Player player;
	private Room winRoom;
	private Dictionary<string, Room> roomsByName;

	private bool wantToQuit;

	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player();
		roomsByName = new Dictionary<string, Room>();
		CreateRooms();
	}

	// Initialise the Rooms (and the Items and Enemies)
	private void CreateRooms()
	{
		// Create the rooms
		Room outside = new Room("outside", "outside the main entrance of the university", "ground");
		Room theatre = new Room("theatre", "in a lecture theatre", "ground");
		Room pub = new Room("pub", "in the campus pub", "ground");
		Room lab = new Room("lab", "in a computing lab", "ground");
		Room office = new Room("office", "in the computing admin office", "ground");
		Room library = new Room("library", "in the campus library", "ground");
		Room gym = new Room("gym", "in the campus gym", "ground");
		Room gymupper = new Room("gymupper", "in the campus gym shower room", "first");
		Room pubbasement = new Room("pubbasement", "in the pub basement", "basement");
		Room pubcellars = new Room("pubcellars", "in the pub cellars","basement");

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);
		outside.AddExit("north", gym);

		theatre.AddExit("west", outside);
		theatre.AddExit("south", library);

		pub.AddExit("east", outside);
		pub.AddExit("down", pubbasement);
		
		pubbasement.AddExit("up", pub);
		pubbasement.AddExit("east", pubcellars);

		pubcellars.AddExit("west", pubbasement);


		lab.AddExit("north", outside);
		lab.AddExit("east", office);

		office.AddExit("west", lab);
		office.AddExit("north", library);

		library.AddExit("south", office);
		library.AddExit("north", theatre);

		gym.AddExit("south", outside);
		gym.AddExit("up", gymupper);

		gymupper.AddExit("down", gym);
		gymupper.AddLock();

		winRoom = gymupper;
		player.WinRoomName = winRoom.GetRoomName();

		// Add rooms to dictionary
		roomsByName.Add("outside", outside);
		roomsByName.Add("theatre", theatre);
		roomsByName.Add("pub", pub);
		roomsByName.Add("lab", lab);
		roomsByName.Add("office", office);
		roomsByName.Add("library", library);
		roomsByName.Add("gym", gym);
		roomsByName.Add("gymupper", gymupper);
		roomsByName.Add("pubbasement", pubbasement);
		roomsByName.Add("pubcellars", pubcellars);

		// Create your Items here
		Item Sword = new Item(10, "sword", false, 10);
		Item Suspicious_Apple = new Item(5, "suspicious_Apple", false);
		Item Key = new Item(1, "key", true);
		Item Medkit = new Item(3, "medkit", true);
		Item Heavy_Shield = new Item(25, "heavy_shield", false);
		Item Map = new Item(1, "map", false);
		Item Ladder = new Item(15, "ladder", false);
		Item Iron_Ingot = new Item(2, "iron_ingot", false);
		Item Bandage = new Item(1, "bandage", false);

		// And add them to the Rooms
		outside.AddItem(Sword);
		outside.AddItem(Medkit);
		outside.AddItem(Map);
		lab.AddItem(Suspicious_Apple);

		// Randomly place the key
		Room keyRoom = roomsByName[GetRandomRoomName()];
		keyRoom.AddItem(Key);
		player.KeyRoomName = keyRoom.GetRoomName();

		// Randomly place map
		//Room mapRoom = roomsByName[GetRandomRoomName()];
		//mapRoom.AddItem(Map);

		// Randomly place ladder in a room that is on the ground floor
		Room ladderRoom = roomsByName[GetRandomRoomName()];
		while (ladderRoom.GetFloor() != "ground")
		{
			ladderRoom = roomsByName[GetRandomRoomName()];
		}
		ladderRoom.AddItem(Ladder);
		player.LadderRoomName = ladderRoom.GetRoomName();

		// Create enemies
		Enemy trainer = new Enemy(30, "wild gym trainer");
		// Add enemies to rooms
		gym.AddEnemy(trainer);

		// And give enemies Items
		gym.enemy.EquipItem(Sword);

		// Create structures
		Structure Forge = new Structure("forge", "A sturdy forge for enhancing weapons.", new Dictionary<string, string[]>
		{
			{"sword", new string[] {"iron_ingot"}},
			{"enhanced_sword", new string[] {"sword", "iron_ingot"}},
			{"heavy_shield", new string[] {"iron_ingot"}}
		});

		Structure CraftingTable = new Structure("craftingtable","Used for crafting items", new Dictionary<string, string[]>
		{
			{"medkit", new string[] {"suspicious_apple", "bandage"}}
		});

		// Add structures to rooms
		lab.AddStructure(Forge);
		outside.AddStructure(CraftingTable);

		// Start game outside
		player.CurrentRoom=outside;
	}

	private string GetRandomRoomName()
	{
		var available = Room.Rooms.Where(r => r != winRoom.GetRoomName()).ToList();
		return available[new Random().Next(available.Count)];
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{
			if (!player.IsAlive())
			{
				Console.WriteLine("You have died!");
				break;
			}
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine("#################################################################");
		Console.WriteLine("###########################  OBJECTIVE  #########################"); // for now...
		Console.WriteLine("#################################################################");
		Console.WriteLine();
		Console.WriteLine("Your mission is to find the secret winning room and win the game!");
		Console.WriteLine("In the future, the winning room and objective may be different each time you play.");
		Console.WriteLine("But for now, I will leave you this hint: you feel incredibly stinky, maybe you should take a shower? ;)");
		Console.WriteLine("Good luck!");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		wantToQuit = false;

		if(command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}
		// Check which command it is and execute it.

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
			case "look":
				printLook();
				break;
			case "take":
				Take(command);
				break;
			case "drop":
				Drop(command);
				break;
			case "inventory":
				PrintInventory();
				break;
			case "status":
				PrintStatus();
				break;
			case "use":
				PlayerUse(command);
				break;
			case "craft":
				PlayerCraft(command);
				break;
		}
		EnemyAttacksPlayer();
		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################
	
	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if (player.CurrentRoom.HasEnemy)
		{
			Console.WriteLine("There is an enemy in this room, stopping you from leaving");
			return;
		}
		if(!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;
		bool hasLadder = player.PeekBackpack("ladder") != null;
		if ((direction == "up" || direction == "down") && !hasLadder)
		{
			Console.WriteLine("You can't go that way without a ladder!");
			return;
		}

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+direction+"!");
			return;
		}

		if (nextRoom.IsLocked)
		{
			Console.WriteLine("This room is locked, you will need to find a key.");
			return;
		}
		player.Damage(5);
		Console.WriteLine($"You feel a bit tired. (-5 Health){Environment.NewLine}Current Health: {player.Health}/{player.MaxHealth}");
	
		player.CurrentRoom = nextRoom;
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
		if (player.CurrentRoom == winRoom)
		{
			Console.WriteLine("Congratulations! You found the secret winning room and have won the game!");
			wantToQuit = true;
		}
	}
	private void printLook()
	{
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	private void Take(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Take what?");
			return;
		}

		string itemName = command.SecondWord;
		player.TakeFromChest(itemName);
	}

	private void Drop(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Drop what?");
			return;
		}

		string itemName = command.SecondWord;
		player.DropToChest(itemName);
	}

	private void PrintInventory()
	{
		Console.WriteLine("You are carrying:");
		Console.WriteLine(player.ShowBackpack());
	}

	private void PrintStatus()
	{
		Console.WriteLine($"Health: {player.Health}/{player.MaxHealth}");
		Console.WriteLine("Backpack: " + player.ShowBackpack());
	}

	private void PlayerUse(Command command)
	{
		player.Use(command);
	}

	private void PlayerCraft(Command command)
	{
		if (player.CurrentRoom.GetStructureName("craftingtable") == null)
		{
			Console.WriteLine("Your current room does not contain a crafting table");
			return;
		}
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Craft what?");
			return;
		}
		string craftable = command.SecondWord;
		Structure craftingTable = player.CurrentRoom.GetStructure("craftingtable");
		var recipes = craftingTable.GetStructureRecipes();
		if (recipes.ContainsKey(craftable))
		{
			string[] ingredients = recipes[craftable];
			// TODO: Check if player has ingredients and craft item
		}
		else
		{
			Console.WriteLine($"Cannot craft {craftable} at this structure.");
		}
	}

	private void EnemyAttacksPlayer()
	{
		if (player.CurrentRoom.HasEnemy)
		{
			player.CurrentRoom.enemy.Attacks(player);
		}
	}
}