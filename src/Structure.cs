class Structure
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    private static int nextId = 1;

    public Structure(string name, string description)
    {
        Id = nextId++;
        Name = name;
        Description = description;
    }
    public string GetStructureName()
    {
        return this.Name;
    }
}