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
	public string Use(string itemName)
	{
		// TODO implementeer CORRECTLY
		Item item = backpack.Get(itemName);

		if (item != null && item.IsExpendable)
		{
			backpack.Remove(itemName);
		}

		if (item != null)
		{
			return $"You used the {itemName}.";
		}
		return $"You don't have the {itemName}.";
	}
}