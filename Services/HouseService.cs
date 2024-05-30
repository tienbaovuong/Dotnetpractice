
using Microsoft.EntityFrameworkCore;

public class HouseService : IHouseService
{
  private readonly RelationDBContext _dbContext;
  public HouseService(RelationDBContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IEnumerable<House>> GetAllHouses()
  {
    return await _dbContext.House.ToListAsync();
  }

  public async Task<House?> GetHouseById(Guid id)
  {
    return await _dbContext.House.Where(c => c.Id == id).FirstAsync();
  }

  public async Task<IEnumerable<House>> GetHousesByStreetId(Guid? streetId)
  {
    return await _dbContext.House.Where(c => c.StreetId == streetId).ToListAsync();
  }
  public async Task CreateHouse(House House)
  {
    await _dbContext.House.AddAsync(House);
    await _dbContext.SaveChangesAsync();
  }

  public async Task UpdateHouse(Guid id, House House)
  {
    // Throw exception if House not found
    var HouseToUpdate = await GetHouseById(id) ?? throw new ArgumentException("House not found");

    // Update House
    HouseToUpdate.OwnerName = House.OwnerName;
    HouseToUpdate.HouseNumber = House.HouseNumber;
    HouseToUpdate.OccupantCount = House.OccupantCount;
    HouseToUpdate.HouseArea = House.HouseArea;

    await _dbContext.SaveChangesAsync();
  }

  public async Task DeleteHouse(Guid id)
  {
    // Throw exception if House not found
    var HouseToUpdate = await GetHouseById(id) ?? throw new ArgumentException("House not found");

    // Delete House
    _dbContext.House.Remove(HouseToUpdate);
    await _dbContext.SaveChangesAsync();
  }
}