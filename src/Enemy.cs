class Enemy
{
    public int MaxHealth;
    public int CurrentHealth;
    private string Description;
    public int AttackDamage { get; set; }
    private PrintInColor print;
    public bool IsAlive
    {
        get { return CurrentHealth > 0; }
    }
    public Room currentroom;
    public Item EquippedItem;
    public Enemy(int maxHealth, string description, Room room = null, int? damage = null)
    {
        print = new PrintInColor();
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
        print.Color($"The {Description} attacks you for {damage} damage!", "red");
    }
    public string EnemyDescription()
    {
        return Description;
    }

    public void EquipItem(Item item)
    {
        EquippedItem = item;
    }

    public bool IsEnemyAlive()
    {
        return IsAlive;
    }
}