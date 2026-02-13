using LibraryService.BL.Interfaces;
using LibraryService.DL.Interfaces;
using LibraryService.Models.Dto;

namespace LibraryService.BL.Services
{
    internal class MemberCrudService : IMemberCrudService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberCrudService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public void Add(Member member)
        {
            if (member == null) return;

            if (member.Id == Guid.Empty)
            {
                member.Id = Guid.NewGuid();
            }

            _memberRepository.Add(member);
        }

        public List<Member> GetAll()
        {
            return _memberRepository.GetAll();
        }

        public Member? GetById(Guid id)
        {
            return _memberRepository.GetById(id);
        }
    }
}
