public interface IStreetService
{
  Task <IEnumerable<Street>> GetAllStreets();
  Task <IEnumerable<Street>> GetStreetsBySuburbId(Guid? suburbId);
  Task <Street?> GetStreetById(Guid id);
  Task CreateStreet(Street street);
  Task UpdateStreet(Guid id, Street street);
  Task DeleteStreet(Guid id);
}