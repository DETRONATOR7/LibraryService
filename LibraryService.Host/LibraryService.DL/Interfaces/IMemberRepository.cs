using LibraryService.Models.Dto;

namespace LibraryService.DL.Interfaces
{
    public interface IMemberRepository
    {
        void Add(Member member);
        List<Member> GetAll();
        Member? GetById(Guid? id);
    }
}
