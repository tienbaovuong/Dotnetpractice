
using Microsoft.EntityFrameworkCore;

public class StreetService : IStreetService
{
  private readonly RelationDBContext _dbContext;
  public StreetService(RelationDBContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IEnumerable<StreetDTO>> GetAllStreets()
  {
    return await _dbContext.Street
      .Select(c => new StreetDTO
      {
        Id = c.Id,
        StreetName = c.StreetName,
        PostCode = c.PostCode,
        SuburbId = c.SuburbId
      })
      .ToListAsync();
  }

  public async Task<StreetDetailDTO?> GetStreetById(Guid id)
  {
    return await _dbContext.Street
      .Where(c => c.Id == id)
      .Select(c => new StreetDetailDTO
      {
        Id = c.Id,
        StreetName = c.StreetName,
        PostCode = c.PostCode,
        SuburbId = c.SuburbId,
        Suburb = new SuburbDTO
        {
          Id = c.Suburb.Id,
          SuburbName = c.Suburb.SuburbName,
          PostCode = c.Suburb.PostCode
        },
        Houses = c.Houses.Select(s => new HouseDTO
        {
          Id = s.Id,
          OwnerName = s.OwnerName,
          HouseNumber = s.HouseNumber,
          OccupantCount = s.OccupantCount,
          HouseArea = s.HouseArea
        }).ToList()
      })
      .FirstOrDefaultAsync();
  }

  public async Task<IEnumerable<StreetDTO>> GetStreetsBySuburbId(Guid? suburbId)
  {
    return await _dbContext.Street
      .Where(c => c.SuburbId == suburbId)
      .Select(c => new StreetDTO
      {
        Id = c.Id,
        StreetName = c.StreetName,
        PostCode = c.PostCode,
        SuburbId = c.SuburbId
      })
      .ToListAsync();
  }

  public async Task CreateStreet(StreetDTO street)
  {
    var newStreet = new Street
    {
      Id = street.Id,
      StreetName = street.StreetName,
      PostCode = street.PostCode,
      SuburbId = street.SuburbId
    };
    await _dbContext.Street.AddAsync(newStreet);
    await _dbContext.SaveChangesAsync();
  }

  public async Task UpdateStreet(Guid id, StreetDTO street)
  {
    // Throw exception if street not found
    var streetToUpdate = await _dbContext.Street.Where(c => c.Id == id).FirstAsync() 
      ?? throw new ArgumentException("Suburb not found");

    // Update street
    streetToUpdate.StreetName = street.StreetName;
    streetToUpdate.PostCode = street.PostCode;
    streetToUpdate.SuburbId = street.SuburbId;

    await _dbContext.SaveChangesAsync();
  }

  public async Task DeleteStreet(Guid id)
  {
    // Throw exception if street not found
    var streetToUpdate = await _dbContext.Street.Where(c => c.Id == id).FirstOrDefaultAsync() 
      ?? throw new ArgumentException("Suburb not found");
    
    // Delete street
    _dbContext.Street.Remove(streetToUpdate);
    await _dbContext.SaveChangesAsync();
  }
}