public interface IHouseService
{
  Task <IEnumerable<HouseDTO>> GetAllHouses();
  Task <IEnumerable<HouseDTO>> GetHousesByStreetId(Guid? streetId);
  Task <HouseDetailDTO?> GetHouseById(Guid id);
  Task CreateHouse(HouseDTO House);
  Task UpdateHouse(Guid id, HouseDTO House);
  Task DeleteHouse(Guid id);
}