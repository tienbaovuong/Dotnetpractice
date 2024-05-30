
using Microsoft.EntityFrameworkCore;

public class CityService : ICityService
{
  private readonly RelationDBContext _dbContext;
  public CityService(RelationDBContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IEnumerable<CityDTO>> GetAllCities()
  {
    return await _dbContext.City
      .Select(c => new CityDTO
      {
        Id = c.Id,
        CityName = c.CityName,
        PostCode = c.PostCode
      })
      .ToListAsync();
  }

  public async Task<CityDetailDTO?> GetCityById(Guid id)
  {
    try
    {
    return await _dbContext.City
      .Where(c => c.Id == id)
      .Select(c => new CityDetailDTO
      {
        Id = c.Id,
        CityName = c.CityName,
        PostCode = c.PostCode,
        Suburbs = c.Suburbs.Select(s => new SuburbDTO
        {
          Id = s.Id,
          SuburbName = s.SuburbName,
          PostCode = s.PostCode
        }).ToList()
      })
      .FirstOrDefaultAsync();
    }
    catch (InvalidOperationException)
    {
      return null;
    }
  }

  public async Task CreateCity(CityDTO city)
  {
    var newCity = new City
    {
      Id = city.Id,
      CityName = city.CityName,
      PostCode = city.PostCode
    };
    await _dbContext.City.AddAsync(newCity);
    await _dbContext.SaveChangesAsync();
  }

  public async Task UpdateCity(Guid id, CityDTO city)
  {
    // Throw exception if city not found
    var cityToUpdate = await _dbContext.City.Where(c => c.Id == id).FirstOrDefaultAsync() 
      ?? throw new ArgumentException("City not found");

    // Update city
    cityToUpdate.CityName = city.CityName;
    cityToUpdate.PostCode = city.PostCode;

    await _dbContext.SaveChangesAsync();
  }

  public async Task DeleteCity(Guid id)
  {
    // Throw exception if city not found
    var cityToDelete = await _dbContext.City.Where(c => c.Id == id).FirstOrDefaultAsync() 
      ?? throw new ArgumentException("City not found");

    // Delete city
    _dbContext.City.Remove(cityToDelete);
    await _dbContext.SaveChangesAsync();
  }
}