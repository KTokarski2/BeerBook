namespace BeerBook.Models;

public class Seller
{
    public int IdSeller { get; set; }
    public string Name { get; set; }
    public virtual IEnumerable<SellerBeer> SellerBeers { get; set; } = null!;
    public virtual IEnumerable<Address> Addresses { get; set; } = null!;
}