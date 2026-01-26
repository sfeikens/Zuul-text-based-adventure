class Enemy
{
    public int MaxHealth;
    public int CurrentHealth;
    public string Description;
    public bool IsAlive
    {
        get { return CurrentHealth > 0; }
    }
    public Room currentroom;
    public Enemy(int maxHealth, string description, Room room)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        Description = description;
        currentroom = room;
    }
    public void Damage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
    }
    public void Attacks(Player player)
    {
        int damage = 10; // vaste schade voor nu
        player.Damage(damage);
        Console.WriteLine($"The {Description} attacks you for {damage} damage!");
    }
    public string EnemyDescription()
    {
        return Description;
    }
    
}