namespace BeerBook.Models;

public class SellerBeer
{
    public int IdSeller { get; set; }
    public virtual Seller Seller { get; set; } = null!;
    public int IdBeer { get; set; }
    public virtual Beer Beer { get; set; } = null!;
    public double Price { get; set; }
}