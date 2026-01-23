using System.Collections.Generic;

public class Inventory
{
    // fields
    private int maxWeight;
    private int currentWeight;
    private Dictionary<string, Item> items;
    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        this.currentWeight = 0;
        this.items = new Dictionary<string, Item>();
    }

    // methods
    public int TotalWeight()
    {
        int total = 0;
        // TODO implementeer:
        // Loop door alle items
        // Tel alle gewichten op
        foreach (Item item in items.Values)
        {
            total += item.Weight;
        }
        return total;
    }
    
    public int FreeWeight()
    {
        // TODO implementeer:
        // Vergelijk MaxWeight en TotalWeight()
        return maxWeight - TotalWeight();
    }
    public bool Put(string itemName, Item item)
    {
        // Check het gewicht van het Item
        // Is er genoeg ruimte in de Inventory?
        // Past het Item?
        // Zet Item in de Dictionary
        // Return true/false voor succes/mislukt
        if (currentWeight + item.Weight <= maxWeight)
        {
            items[itemName] = item;
            currentWeight += item.Weight;
            return true;
        }
        return false;
    }

    public Item Get(string itemName)
    {
        // Zoek Item in de Dictionary
        // Verwijder Item uit Dictionary (als gevonden)
        // Return Item of null
        if (items.ContainsKey(itemName))
        {
            Item item = items[itemName];
            items.Remove(itemName);
            currentWeight -= item.Weight;
            return item;
        }
        return null;
    }

    public Item Peek(string itemName)
    {
        // Zoek Item in de Dictionary zonder te verwijderen
        // Return Item of null
        if (items.ContainsKey(itemName))
        {
            return items[itemName];
        }
        return null;
    }

    public string Show()
    {
        if (items.Count == 0)
        {
            return "no items";
        }
        return string.Join(", ", items.Keys);
    }
}