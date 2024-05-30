using Microsoft.EntityFrameworkCore;

[Index(nameof(CityName), AllDescending = true)]
public class City
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string CityName { get; set; } = "";
  public string PostCode { get; set; } = "";

  // Reference fields
  public List<Suburb> Suburbs { get; set; } = [];
}
