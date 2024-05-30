public class Street
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string StreetName { get; set; } = "";
  public string PostCode { get; set; } = "";

  // Reference fields
  public Guid SuburbId { get; set; }
  public required Suburb Suburb { get; set; }
  public List<House> Houses { get; set; } = [];
}