using LibraryService.Models.Dto;

namespace LibraryService.BL.Interfaces
{
    public interface IMemberCrudService
    {
        void Add(Member member);
        List<Member> GetAll();
        Member? GetById(Guid id);
    }
}
