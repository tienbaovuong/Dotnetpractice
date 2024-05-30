public interface ICityService
{
  Task <IEnumerable<CityDTO>> GetAllCities();
  Task <CityDetailDTO?> GetCityById(Guid id);
  Task CreateCity(CityDTO city);
  Task UpdateCity(Guid id, CityDTO city);
  Task DeleteCity(Guid id);
}