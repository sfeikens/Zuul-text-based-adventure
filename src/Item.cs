
using System.Globalization;

public class Item
{
    //fields
    public int Weight { get; }
    public string Description { get; }

    public bool IsExpendable { get; }
    public bool IsWeapon { get; }

    public int WeaponDamage { get; }

    //constructor
    public Item(int weight, string description, bool isexpendable, int? damage = null)
    {
        Weight = weight;
        Description = description;
        IsExpendable = isexpendable;
        if (damage.HasValue)
        {
            IsWeapon = true;
            WeaponDamage = damage.Value;
        }
        else
        {
            IsWeapon = false;
            WeaponDamage = 0;
        }
    }
}