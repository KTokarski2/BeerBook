namespace BeerBook.Models;

public class UserBeer
{
    public int IdUser { get; set; }
    public virtual User User { get; set; } = null!;
    public int IdBeer { get; set; }
    public virtual Beer Beer { get; set; } = null!;
    public int Rating { get; set; }
    
}