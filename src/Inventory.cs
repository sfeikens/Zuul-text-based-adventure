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
}