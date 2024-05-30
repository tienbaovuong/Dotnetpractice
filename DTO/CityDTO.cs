public class CityDTO
{
  public Guid Id { get; set; } = Guid.Empty;
  public string CityName { get; set; }
  public string PostCode { get; set; }
}

public class CityDetailDTO : CityDTO
{
  public List<SuburbDTO> Suburbs { get; set; } = [];
}