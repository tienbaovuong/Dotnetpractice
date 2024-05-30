public interface ISuburbService
{
  Task <IEnumerable<Suburb>> GetAllSuburbs();
  Task <IEnumerable<Suburb>> GetSuburbsByCityId(Guid? cityId);
  Task <Suburb?> GetSuburbById(Guid id);
  Task CreateSuburb(Suburb suburb);
  Task UpdateSuburb(Guid id, Suburb suburb);
  Task DeleteSuburb(Guid id);
}