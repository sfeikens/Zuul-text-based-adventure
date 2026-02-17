#nullable enable

class Structure
{
    private string Name { get; set; }
    private string Description { get; set; }
    private Dictionary<string, string[]>? Recipes { get; set; }

    public Structure(string name, string description, Dictionary<string, string[]>? recipes = null)
    {
        Name = name;
        Description = description;
        Recipes = recipes;
    }
    public string GetStructureName()
    {
        return this.Name;
    }
    public string GetStructureDescription()
    {
        return this.Description;
    }
    public Dictionary<string, string[]>? GetStructureRecipes()
    {
        return this.Recipes;
    }
}