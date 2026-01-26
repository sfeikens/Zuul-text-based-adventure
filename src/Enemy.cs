class Enemy
{
    public int MaxHealth;
    public int CurrentHealth;
    public string Description;
    public int AttackDamage { get; set; }
    public bool IsAlive
    {
        get { return CurrentHealth > 0; }
    }
    public Room currentroom;
    public Item EquippedItem;
    public Enemy(int maxHealth, string description, Room room, int? damage = null)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        Description = description;
        currentroom = room;
        AttackDamage = damage ?? 0;
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
        int damage = this.AttackDamage;
        if (EquippedItem != null && EquippedItem.IsWeapon && EquippedItem.WeaponDamage > 0)
        {
            damage = EquippedItem.WeaponDamage;
        }
        else
        {
            return;
        }
        player.Damage(damage);
        Console.WriteLine($"The {Description} attacks you for {damage} damage!");
    }
    public string EnemyDescription()
    {
        return Description;
    }

    public void EquipItem(Item item)
    {
        EquippedItem = item;
    }
}