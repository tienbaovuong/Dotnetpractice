public interface ISuburbService
{
  Task <IEnumerable<SuburbDTO>> GetAllSuburbs();
  Task <IEnumerable<SuburbDTO>> GetSuburbsByCityId(Guid? cityId);
  Task <SuburbDetailDTO?> GetSuburbById(Guid id);
  Task CreateSuburb(SuburbDTO suburb);
  Task UpdateSuburb(Guid id, SuburbDTO suburb);
  Task DeleteSuburb(Guid id);
}