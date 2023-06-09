namespace BeerBook.Models;

public class User
{
    public int IdUser { get; set; }
    public string Nick { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public virtual IEnumerable<UserBeer> UserBeers { get; set; } = null!;
}