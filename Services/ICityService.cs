public interface ICityService
{
  Task <IEnumerable<City>> GetAllCities();
  Task <City?> GetCityById(Guid id);
  Task CreateCity(City city);
  Task UpdateCity(Guid id, City city);
  Task DeleteCity(Guid id);
}