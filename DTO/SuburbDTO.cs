public class SuburbDTO
{
  public Guid Id { get; set; } = Guid.Empty;
  public string SuburbName { get; set; }
  public string PostCode { get; set; }
  public Guid CityId { get; set; }
}

public class SuburbDetailDTO : SuburbDTO
{
  public List<StreetDTO> Streets { get; set; } = [];
  public CityDTO City { get; set; }
}