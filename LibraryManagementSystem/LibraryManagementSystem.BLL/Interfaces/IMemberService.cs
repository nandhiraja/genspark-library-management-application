using System.Security.Cryptography.X509Certificates;
using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.BLL.Interfaces
{
    public interface IMemberService
    {
        public bool AddMember(Member member);

        public Member? GetMemberById(int memberId);
        public Member? GetMemberByEmail(string email);
        public Member? GetMemberByPhone(string phone);
        public List<Member>? GetAllMembers();
        public List<Member>? GetAllMembersByMemberShip(MembershipType membershipType);
        public List<Member>? GetAllActiveMembers();

        public bool UpdateMember(Member member);
        public bool DeleteMember(Member member);

    }
}