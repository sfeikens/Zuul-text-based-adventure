class Structure
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Structure(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public string GetStructureName()
    {
        return this.Name;
    }
}