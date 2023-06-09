using BeerBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BeerBook.BeerBookRepository;

public class BeerBookDbContext : DbContext
{
    public BeerBookDbContext()
    {
        
    }

    public BeerBookDbContext(DbContextOptions<BeerBookDbContext> options) : base(options)
    {
        
    }
    
    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Beer> Beers { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Seller> Sellers { get; set; }
    public virtual DbSet<SellerBeer> SellerBeers { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserBeer> UserBeers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => new { e.IdAddress }).HasName("Address_pk");
            entity.ToTable("Address", "beerbook");
            entity.Property(e => e.Street).HasMaxLength(100);
            entity.Property(e => e.StreetNumber);
            entity.Property(e => e.FlatNumber);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.PostalCode).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(100);

            entity
                .HasOne(a => a.Seller)
                .WithMany(s => s.Addresses)
                .HasForeignKey(a => a.IdSeller)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Address_Seller");
        });

        modelBuilder.Entity<Beer>(entity =>
        {
            entity.HasKey(e => new { e.IdBeer }).HasName("Beer_pk");
            entity.ToTable("Beer", "beerbook");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Size);

            entity
                .HasOne(b => b.Category)
                .WithMany(c => c.Beers)
                .HasForeignKey(b => b.IdCategory)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("Beer_Category");
        });
        
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => new { e.IdCategory }).HasName("Category_pk");
            entity.ToTable("Category", "beerbook");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(e => new { e.IdSeller }).HasName("Seller_pk");
            entity.ToTable("Seller", "beerbook");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<SellerBeer>(entity =>
        {
            entity.HasKey(e => new { e.IdSeller, e.IdBeer }).HasName("SellerBeer_pk");
            entity.ToTable("SellerBerr", "beerbook");
            entity.Property(e => e.Price).HasPrecision(2);

            entity
                .HasOne(s => s.Seller)
                .WithMany(s => s.SellerBeers)
                .HasForeignKey(s => s.IdSeller)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("SellerBeer_Seller");

            entity
                .HasOne(s => s.Beer)
                .WithMany(b => b.SellerBeers)
                .HasForeignKey(s => s.IdBeer)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("SellerBeer_Beer");
        });
    }
}