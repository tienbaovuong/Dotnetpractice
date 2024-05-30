using Microsoft.EntityFrameworkCore;

public partial class RelationDBContext : DbContext
{
  public RelationDBContext(DbContextOptions<RelationDBContext> options) : base(options)
  {
  }
  public virtual DbSet<City> City { get; set; }
  public virtual DbSet<Suburb> Suburb { get; set; }
  public virtual DbSet<Street> Street { get; set; }
  public virtual DbSet<House> House { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // One to Many City, Suburb
    modelBuilder.Entity<City>()
      .HasMany(e => e.Suburbs)
      .WithOne(e => e.City)
      .HasForeignKey(e => e.CityId)
      .HasPrincipalKey(e => e.Id);

    // One to Many Suburb, Street
    modelBuilder.Entity<Suburb>()
      .HasMany(e => e.Streets)
      .WithOne(e => e.Suburb)
      .HasForeignKey(e => e.SuburbId)
      .HasPrincipalKey(e => e.Id);

    // One to Many Street, House
    modelBuilder.Entity<Street>()
      .HasMany(e => e.Houses)
      .WithOne(e => e.Street)
      .HasForeignKey(e => e.StreetId)
      .HasPrincipalKey(e => e.Id);

    // House PK
    modelBuilder.Entity<House>()
      .HasKey(e => e.Id);

    // Call partial method to allow further configuration
    OnModelCreatingPartial(modelBuilder);
  }
  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}