namespace BeerBook.Models;

public class Address
{
    public int IdAddress { get; set; }
    public string Street { get; set; }
    public int StreetNumber { get; set; }
    public int FlatNumber { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public int IdSeller { get; set; }
    public virtual Seller Seller { get; set; } = null!;

}