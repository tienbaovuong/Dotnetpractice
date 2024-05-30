public interface IHouseService
{
  Task <IEnumerable<House>> GetAllHouses();
  Task <IEnumerable<House>> GetHousesByStreetId(Guid? streetId);
  Task <House?> GetHouseById(Guid id);
  Task CreateHouse(House House);
  Task UpdateHouse(Guid id, House House);
  Task DeleteHouse(Guid id);
}