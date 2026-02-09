class Player
{
	// auto property
	public Room CurrentRoom { get; set; }
	public string KeyRoomName { get; set; }
	public string WinRoomName { get; set; }
	public string LadderRoomName { get; set; }
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

	public string PeekBackpack(string itemName)
	{
		Item item = backpack.Peek(itemName);
		if (item == null)
		{
			return null;
		}
		return item.Description;
	}
	
	// methods
	public void Use(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Use what?");
			return;
		}

		if (!command.HasThirdWord())
		{
			Console.WriteLine("You might need to specify a target for that item.");
		}
		switch (command.SecondWord)
		{
			case "item":
			{
				string itemName = command.ThirdWord;
				Item item = backpack.Peek(itemName);
				if (item == null)
				{
					Console.WriteLine("You don't have that item.");
					return;
				}

				switch (itemName)
				{
					case "medkit":
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
						if (!LockedRoom.IsLocked)
						{
							Console.WriteLine("That room is not locked.");
							return;
						}
						LockedRoom.RemoveLock();
						Console.WriteLine("The room is now unlocked.");
						break;

					case "suspicious_Apple":
						Console.WriteLine("You eat the Suspicious Apple. It was delicious!");
						Console.WriteLine("However, you suddenly feel very sleepy...");
						Damage(10);
						Console.WriteLine("You lost 10 Health.");
						break;

					case "sword":
						if (CurrentRoom.HasEnemy)
						{
							int SwordDamage = item.WeaponDamage;
							CurrentRoom.enemy.Damage(SwordDamage);
							Console.WriteLine($"You damaged the {CurrentRoom.enemy.EnemyDescription()} for {SwordDamage} hitpoints! He has {CurrentRoom.enemy.CurrentHealth} health left.");
							if (CurrentRoom.enemy.CurrentHealth == 0)
							{
								Console.WriteLine($"You have defeated the {CurrentRoom.enemy.EnemyDescription()}!");
								CurrentRoom.RemoveEnemy();
							}
						}
						else
						{
							Console.WriteLine("There is no enemy to target");
						}
						break;

					case "map":
						Console.WriteLine("You look at the map. It shows the location of the key room and the winning room.");
						Console.WriteLine("Key Room: " + KeyRoomName);
						Console.WriteLine("Winning Room: " + WinRoomName);
						Console.WriteLine("Ladder Room: " + LadderRoomName);
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
				break;
			}

			case "structure":
				string structureName = CurrentRoom.GetStructureName(command.ThirdWord);
				if (structureName == null)
				{
					Console.WriteLine("There is no such structure here.");
					return;
				}
				switch (structureName)
				{
					case "forge":
						if (command.FourthWord == null)
						{
							Console.WriteLine("Use the forge on what?");
							return;
						}
						string forgeItemName = command.FourthWord;
						Item forgeItem = backpack.Peek(forgeItemName);
						if (forgeItem == null)
						{
							Console.WriteLine("You don't have that item.");
							return;
						}
						if (!forgeItem.IsWeapon)
						{
							Console.WriteLine("You can only use weapons in the forge.");
							return;
						}
						if (forgeItemName.Contains("Enhanced"))
						{
							Console.WriteLine("This weapon is already enhanced.");
							return;
						}
						int enhancedDamage = forgeItem.WeaponDamage + 10;
						Item enhancedWeapon = new Item(forgeItem.Weight, forgeItem.Description + "(Enhanced)", false, enhancedDamage);
						backpack.Get(forgeItemName);
						backpack.Put(enhancedWeapon.Description, enhancedWeapon);
						Console.WriteLine($"You have enhanced your {forgeItemName} to {enhancedWeapon.Description} with {enhancedDamage} damage!");
						break;

					default:
						Console.WriteLine("You can't use that structure.");
						break;
				}
				break;

			default:
				Console.WriteLine("You can't use that.");
				break;
		}
	}
}