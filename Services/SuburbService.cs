
using Microsoft.EntityFrameworkCore;

public class SuburbService : ISuburbService
{
  private readonly RelationDBContext _dbContext;
  public SuburbService(RelationDBContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IEnumerable<Suburb>> GetAllSuburbs()
  {
    return await _dbContext.Suburb.ToListAsync();
  }

  public async Task<Suburb?> GetSuburbById(Guid id)
  {
    return await _dbContext.Suburb.Where(c => c.Id == id).FirstAsync();
  }

  public async Task<IEnumerable<Suburb>> GetSuburbsByCityId(Guid? cityId)
  {
    return await _dbContext.Suburb.Where(c => c.CityId == cityId).ToListAsync();
  }

  public async Task CreateSuburb(Suburb suburb)
  {
    await _dbContext.Suburb.AddAsync(suburb);
    await _dbContext.SaveChangesAsync();
  }

  public async Task UpdateSuburb(Guid id, Suburb suburb)
  {
    // Throw exception if suburb not found
    var suburbToUpdate = await GetSuburbById(id) ?? throw new ArgumentException("Suburb not found");

    // Update suburb
    suburbToUpdate.SuburbName = suburb.SuburbName;
    suburbToUpdate.PostCode = suburb.PostCode;

    await _dbContext.SaveChangesAsync();
  }

  public async Task DeleteSuburb(Guid id)
  {
    // Throw exception if suburb not found
    var suburbToUpdate = await GetSuburbById(id) ?? throw new ArgumentException("Suburb not found");

    // Delete suburb
    _dbContext.Suburb.Remove(suburbToUpdate);
    await _dbContext.SaveChangesAsync();
  }
}