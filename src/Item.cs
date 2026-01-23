
public class Item
{
    //fields
    public int Weight { get; }
    public string Description { get; }

    public bool IsExpendable { get; }

    //constructor
    public Item(int weight, string description, bool isexpendable)
    {
        Weight = weight;
        Description = description;
        IsExpendable = isexpendable;
    }
}