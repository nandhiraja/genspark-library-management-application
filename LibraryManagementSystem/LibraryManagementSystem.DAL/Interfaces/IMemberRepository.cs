using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.DAL.Interface
{
    public interface IMemberRepository
    {
    void AddMember(Member member);
    bool UpdateMember(Member member);
    Member? GetById(int memberId);
    Member? GetByEmail(string email);
    List<Member> GetAllMembers();
    bool ExistsByEmail(string email);
    }
}