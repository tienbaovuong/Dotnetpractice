
using Microsoft.EntityFrameworkCore;

public class SuburbService : ISuburbService
{
  private readonly RelationDBContext _dbContext;
  public SuburbService(RelationDBContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IEnumerable<SuburbDTO>> GetAllSuburbs()
  {
    return await _dbContext.Suburb
      .Select(c => new SuburbDTO
      {
        Id = c.Id,
        SuburbName = c.SuburbName,
        PostCode = c.PostCode,
        CityId = c.CityId
      })
      .ToListAsync();
  }

  public async Task<SuburbDetailDTO?> GetSuburbById(Guid id)
  {
    return await _dbContext.Suburb
      .Where(c => c.Id == id)
      .Select(c => new SuburbDetailDTO
      {
        Id = c.Id,
        SuburbName = c.SuburbName,
        PostCode = c.PostCode,
        CityId = c.CityId,
        City = new CityDTO
        {
          Id = c.City.Id,
          CityName = c.City.CityName,
          PostCode = c.City.PostCode
        },
        Streets = c.Streets.Select(s => new StreetDTO
        {
          Id = s.Id,
          StreetName = s.StreetName,
          PostCode = s.PostCode
        }).ToList()
      })
      .FirstOrDefaultAsync();
  }

  public async Task<IEnumerable<SuburbDTO>> GetSuburbsByCityId(Guid? cityId)
  {
    return await _dbContext.Suburb
      .Where(c => c.CityId == cityId)
      .Select(c => new SuburbDTO
      {
        Id = c.Id,
        SuburbName = c.SuburbName,
        PostCode = c.PostCode,
        CityId = c.CityId
      })
      .ToListAsync();
  }

  public async Task CreateSuburb(SuburbDTO suburb)
  {
    var newSuburb = new Suburb
    {
      Id = suburb.Id,
      SuburbName = suburb.SuburbName,
      PostCode = suburb.PostCode,
      CityId = suburb.CityId
    };
    await _dbContext.Suburb.AddAsync(newSuburb);
    await _dbContext.SaveChangesAsync();
  }

  public async Task UpdateSuburb(Guid id, SuburbDTO suburb)
  {
    // Throw exception if suburb not found
    var suburbToUpdate = await _dbContext.Suburb.Where(c => c.Id == id).FirstOrDefaultAsync() 
      ?? throw new ArgumentException("Suburb not found");

    // Update suburb
    suburbToUpdate.SuburbName = suburb.SuburbName;
    suburbToUpdate.PostCode = suburb.PostCode;
    suburbToUpdate.CityId = suburb.CityId;

    await _dbContext.SaveChangesAsync();
  }

  public async Task DeleteSuburb(Guid id)
  {
    // Throw exception if suburb not found
    var suburbToDelete = await _dbContext.Suburb.Where(c => c.Id == id).FirstOrDefaultAsync() 
      ?? throw new ArgumentException("Suburb not found");

    // Delete suburb
    _dbContext.Suburb.Remove(suburbToDelete);
    await _dbContext.SaveChangesAsync();
  }
}