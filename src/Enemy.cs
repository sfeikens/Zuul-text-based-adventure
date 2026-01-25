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
    public string EnemyDescription()
    {
        return Description;
    }
    
}