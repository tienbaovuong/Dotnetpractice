public class StreetDTO
{
  public Guid Id { get; set; } = Guid.Empty;
  public string StreetName { get; set; }
  public string PostCode { get; set; }
  public Guid SuburbId { get; set; }
}

public class StreetDetailDTO : StreetDTO
{
  public List<HouseDTO> Houses { get; set; } = [];
  public SuburbDTO Suburb { get; set; }
}