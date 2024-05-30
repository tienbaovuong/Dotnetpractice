
using Microsoft.EntityFrameworkCore;

public class HouseService : IHouseService
{
  private readonly RelationDBContext _dbContext;
  public HouseService(RelationDBContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IEnumerable<HouseDTO>> GetAllHouses()
  {
    return await _dbContext.House
      .Select(c => new HouseDTO
      {
        Id = c.Id,
        OwnerName = c.OwnerName,
        HouseNumber = c.HouseNumber,
        OccupantCount = c.OccupantCount,
        HouseArea = c.HouseArea,
        StreetId = c.StreetId
      })
      .ToListAsync();
  }

  public async Task<HouseDetailDTO?> GetHouseById(Guid id)
  {
    return await _dbContext.House
      .Where(c => c.Id == id)
      .Select(c => new HouseDetailDTO
      {
        Id = c.Id,
        OwnerName = c.OwnerName,
        HouseNumber = c.HouseNumber,
        OccupantCount = c.OccupantCount,
        HouseArea = c.HouseArea,
        StreetId = c.StreetId,
        Street = new StreetDTO
        {
          Id = c.Street.Id,
          StreetName = c.Street.StreetName,
          PostCode = c.Street.PostCode
        }
      }
      )
      .FirstOrDefaultAsync();
  }

  public async Task<IEnumerable<HouseDTO>> GetHousesByStreetId(Guid? streetId)
  {
    return await _dbContext.House
      .Where(c => c.StreetId == streetId)
      .Select(c => new HouseDTO
      {
        Id = c.Id,
        OwnerName = c.OwnerName,
        HouseNumber = c.HouseNumber,
        OccupantCount = c.OccupantCount,
        HouseArea = c.HouseArea,
        StreetId = c.StreetId
      })
      .ToListAsync();
  }

  public async Task CreateHouse(HouseDTO House)
  {
    var newHouse = new House
    {
      OwnerName = House.OwnerName,
      HouseNumber = House.HouseNumber,
      OccupantCount = House.OccupantCount,
      HouseArea = House.HouseArea,
      StreetId = House.StreetId
    };
    await _dbContext.House.AddAsync(newHouse);
    await _dbContext.SaveChangesAsync();
  }

  public async Task UpdateHouse(Guid id, HouseDTO House)
  {
    // Throw exception if House not found
    var HouseToUpdate = await _dbContext.House.Where(c => c.Id == id).FirstOrDefaultAsync() 
      ?? throw new ArgumentException("House not found");

    // Update House
    HouseToUpdate.OwnerName = House.OwnerName;
    HouseToUpdate.HouseNumber = House.HouseNumber;
    HouseToUpdate.OccupantCount = House.OccupantCount;
    HouseToUpdate.HouseArea = House.HouseArea;
    HouseToUpdate.StreetId = House.StreetId;

    await _dbContext.SaveChangesAsync();
  }

  public async Task DeleteHouse(Guid id)
  {
    // Throw exception if House not found
    var HouseToUpdate = await _dbContext.House.Where(c => c.Id == id).FirstOrDefaultAsync() 
      ?? throw new ArgumentException("House not found");

    // Delete House
    _dbContext.House.Remove(HouseToUpdate);
    await _dbContext.SaveChangesAsync();
  }
}