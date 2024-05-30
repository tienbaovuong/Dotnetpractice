public class Suburb
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string SuburbName { get; set; } = "";
  public string PostCode { get; set; } = "";

  // Reference fields
  public Guid CityId { get; set; }
  public required City City { get; set; }
  public List<Street> Streets { get; set; } = [];
}