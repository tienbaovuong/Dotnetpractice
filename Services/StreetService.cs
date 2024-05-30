
using Microsoft.EntityFrameworkCore;

public class StreetService : IStreetService
{
  private readonly RelationDBContext _dbContext;
  public StreetService(RelationDBContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IEnumerable<Street>> GetAllStreets()
  {
    return await _dbContext.Street.ToListAsync();
  }

  public async Task<Street?> GetStreetById(Guid id)
  {
    return await _dbContext.Street.Where(c => c.Id == id).FirstAsync();
  }

  public async Task<IEnumerable<Street>> GetStreetsBySuburbId(Guid? suburbId)
  {
    return await _dbContext.Street.Where(c => c.SuburbId == suburbId).ToListAsync();
  }

  public async Task CreateStreet(Street street)
  {
    await _dbContext.Street.AddAsync(street);
    await _dbContext.SaveChangesAsync();
  }

  public async Task UpdateStreet(Guid id, Street street)
  {
    // Throw exception if street not found
    var streetToUpdate = await GetStreetById(id) ?? throw new ArgumentException("Suburb not found");

    // Update street
    streetToUpdate.StreetName = street.StreetName;
    streetToUpdate.PostCode = street.PostCode;

    await _dbContext.SaveChangesAsync();
  }

  public async Task DeleteStreet(Guid id)
  {
    // Throw exception if street not found
    var streetToUpdate = await GetStreetById(id) ?? throw new ArgumentException("Suburb not found");

    // Delete street
    _dbContext.Street.Remove(streetToUpdate);
    await _dbContext.SaveChangesAsync();
  }
}