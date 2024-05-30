public class House
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string OwnerName { get; set; } = "";
  public string HouseNumber { get; set; } = "";
  public int OccupantCount { get; set; }
  public string HouseArea { get; set; } = "";

  // Reference fields
  public Guid StreetId { get; set; }
  public Street Street { get; set; }
}