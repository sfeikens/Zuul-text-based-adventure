class Player
{
	// auto property
	public Room CurrentRoom { get; set; }

	// constructor
	public Player()
	{
		CurrentRoom = null;
		MaxHealth = 100;
		Health = MaxHealth;
		backpack = new Inventory(25); // 25kg is best zwaar om de hele dag te dragen
	}

	// health properties
	public int MaxHealth { get; private set; }
	public int Health { get; private set; }

	// inventory
	private Inventory backpack;

	// speler verliest health
	public void Damage(int amount)
	{
		if (amount <= 0)
			return;

		Health -= amount;
		if (Health < 0)
			Health = 0;
	}

	// speler krijgt health
	public void Heal(int amount)
	{
		if (amount <= 0)
			return;

		Health += amount;
		if (Health > MaxHealth)
			Health = MaxHealth;
	}

	// checkt of speler nog leeft
	public bool IsAlive()
	{
		return Health > 0;
	}

	public bool TakeFromChest(string itemName)
	{
		Item item = CurrentRoom.Chest.Peek(itemName);
		if (item == null)
		{
			Console.WriteLine("Item is not in Room");
			return false;
		}
		if (backpack.Put(itemName, item))
		{
			CurrentRoom.Chest.Get(itemName);
			Console.WriteLine($"You took the {itemName}.");
			return true;
		}
		else
		{
			Console.WriteLine("Item doesn't fit in your inventory");
			return false;
		}
	}

	public bool DropToChest(string itemName)
	{
		Item item = backpack.Get(itemName);
		if (item == null)
		{
			Console.WriteLine("You don't have that Item");
			return false;
		}
		CurrentRoom.Chest.Put(itemName, item);
		Console.WriteLine($"You dropped the {itemName}.");
		return true;
	}

	public string ShowBackpack()
	{
		return backpack.Show();
	}

	
	// methods
	public void Use(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Use what?");
			return;
		}

		string itemName = command.SecondWord;
		Item item = backpack.Peek(itemName);
		if (item == null)
		{
			Console.WriteLine("You don't have that item.");
			return;
		}

		switch (itemName)
		{
			case "health_potion":
				Heal(20);
				Console.WriteLine("You feel rejuvenated! (+20 Health)");
				break;
			case "key":
				Room LockedRoom = CurrentRoom.GetExit(command.ThirdWord);
				if (LockedRoom == null)
				{
					Console.WriteLine("There is no room in that direction.");
					return;
				}
				if (!LockedRoom.islocked)
				{
					Console.WriteLine("That room is not locked.");
					return;
				}
				LockedRoom.RemoveLock();
				Console.WriteLine("The room is now unlocked.");
				break;
			default:
				Console.WriteLine("Nothing happened.");
				return;
		}

		if (item.IsExpendable)
		{
			backpack.Get(itemName);
			Console.WriteLine($"You used the {itemName}, it is now gone from your inventory.");
		}
		else
		{
			Console.WriteLine($"You used the {itemName}.");
		}
	}
}