public class Book
{
    public string Name { get; set; }
    public int Year { get; set; }

    public Book(string Name, int Year)
    {
        this.Name = Name;
        this.Year = Year;
    }

    public override string ToString()
    {
        return $"{Name} ({Year})";
    }
}
