
using Microsoft.EntityFrameworkCore;

public class CityService : ICityService
{
  private readonly RelationDBContext _dbContext;
  public CityService(RelationDBContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IEnumerable<City>> GetAllCities()
  {
    return await _dbContext.City.ToListAsync();
  }

  public async Task<City?> GetCityById(Guid id)
  {
    return await _dbContext.City.Where(c => c.Id == id).FirstAsync();
  }

  public async Task CreateCity(City city)
  {
    await _dbContext.City.AddAsync(city);
    await _dbContext.SaveChangesAsync();
  }

  public async Task UpdateCity(Guid id, City city)
  {
    // Throw exception if city not found
    var cityToUpdate = await GetCityById(id) ?? throw new ArgumentException("City not found");

    // Update city
    cityToUpdate.CityName = city.CityName;
    cityToUpdate.PostCode = city.PostCode;

    await _dbContext.SaveChangesAsync();
  }

  public async Task DeleteCity(Guid id)
  {
    // Throw exception if city not found
    var cityToDelete = await GetCityById(id) ?? throw new ArgumentException("City not found");

    // Delete city
    _dbContext.City.Remove(cityToDelete);
    await _dbContext.SaveChangesAsync();
  }
}