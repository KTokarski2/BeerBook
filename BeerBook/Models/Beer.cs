namespace BeerBook.Models;

public class Beer
{
    public int IdBeer { get; set; }
    public string Name { get; set; }
    public int Size { get; set; }
    public int IdCategory { get; set; }
    public virtual Category Category { get; set; } = null!;
    public virtual IEnumerable<UserBeer> UserBeers { get; set; } = null!;
    public virtual IEnumerable<SellerBeer> SellerBeers { get; set; } = null!;
}