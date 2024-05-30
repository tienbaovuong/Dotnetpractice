public interface IStreetService
{
  Task <IEnumerable<StreetDTO>> GetAllStreets();
  Task <IEnumerable<StreetDTO>> GetStreetsBySuburbId(Guid? suburbId);
  Task <StreetDetailDTO?> GetStreetById(Guid id);
  Task CreateStreet(StreetDTO street);
  Task UpdateStreet(Guid id, StreetDTO street);
  Task DeleteStreet(Guid id);
}