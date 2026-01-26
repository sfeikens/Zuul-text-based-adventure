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
		Room outside = new Room("outside", "outside the main entrance of the university");
		Room theatre = new Room("theatre", "in a lecture theatre");
		Room pub = new Room("pub", "in the campus pub");
		Room lab = new Room("lab", "in a computing lab");
		Room office = new Room("office", "in the computing admin office");
		Room library = new Room("library", "in the campus library");
		Room gym = new Room("gym", "in the campus gym");
		Room gymupper = new Room("gymupper", "in the campus gym shower room");

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);
		outside.AddExit("north", gym);

		theatre.AddExit("west", outside);
		theatre.AddExit("south", library);

		pub.AddExit("east", outside);

		lab.AddExit("north", outside);
		lab.AddExit("east", office);

		office.AddExit("west", lab);
		office.AddExit("north", library);

		library.AddExit("south", office);
		library.AddExit("north", theatre);

		gym.AddExit("south", outside);
		gym.AddExit("up", gymupper);
		gym.AddEnemy(30, "wild gym trainer", 10);

		gymupper.AddExit("down", gym);
		gymupper.AddLock();

		winRoom = gymupper;

		// Add rooms to dictionary
		roomsByName.Add("outside", outside);
		roomsByName.Add("theatre", theatre);
		roomsByName.Add("pub", pub);
		roomsByName.Add("lab", lab);
		roomsByName.Add("office", office);
		roomsByName.Add("library", library);
		roomsByName.Add("gym", gym);
		roomsByName.Add("gymupper", gymupper);

		// Create your Items here
		Item Sword = new Item(10, "sword", false, 10);
		Item Suspicious_Apple = new Item(5, "suspicious_Apple", false);
		Item Key = new Item(1, "key", true);
		Item Medkit = new Item(3, "medkit", true);
		Item Heavy_Shield = new Item(25, "heavy_shield", false);
		// And add them to the Rooms
		outside.AddItem(Sword);
		outside.AddItem(Medkit);
		lab.AddItem(Suspicious_Apple);
		// Randomly place the key
		string keyRoomName = GetRandomRoomName();
		Room keyRoom = roomsByName[keyRoomName];
		keyRoom.AddItem(Key);
		// Give enemies Items
		gym.enemy.EquipItem(Sword);
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
		 EnemyAttacksPlayer();

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
				PlayerUseItem(command);
				break;
		}
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

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+direction+"!");
			return;
		}

		if (nextRoom.islocked)
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

	private void PlayerUseItem(Command command)
	{
		player.Use(command);
	}

	private void EnemyAttacksPlayer()
	{
		if (player.CurrentRoom.HasEnemy)
		{
			player.CurrentRoom.enemy.Attacks(player);
		}
	}
}