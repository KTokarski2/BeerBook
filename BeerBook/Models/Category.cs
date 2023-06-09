namespace BeerBook.Models;

public class Category
{
    public int IdCategory { get; set; }
    public string Name { get; set; }
    public virtual IEnumerable<Beer> Beers { get; set; } = null!;
}