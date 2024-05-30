public class HouseDTO
{
  public Guid Id { get; set; } = Guid.Empty;
  public string OwnerName { get; set; }
  public string HouseNumber { get; set; }
  public int OccupantCount { get; set; }
  public string HouseArea { get; set; }
  public Guid StreetId { get; set; }
}

public class HouseDetailDTO : HouseDTO
{
  public StreetDTO Street { get; set; }
}