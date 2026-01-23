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
		Inventory = new Inventory(50); // max gewicht 50
	}

	// health properties
	public int MaxHealth { get; private set; }
	public int Health { get; private set; }

	// inventory
	public Inventory Inventory { get; private set; }

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

}