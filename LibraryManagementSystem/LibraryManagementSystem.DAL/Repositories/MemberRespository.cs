using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;
using LibraryManagementSystem.DAL.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Repositories
{
    public class MemeberRepository : IMemberRepository
    {   
        LibraryDbContext _context;
        public MemeberRepository(LibraryDbContext libraryDbContext)
        {
            _context = libraryDbContext;
        }

        public void AddMember(Member member)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
        }

        public bool ExistsByEmail(string email)
        {
             return _context.Members.Any(m=>m.Email==email);
        }

        public List<Member> GetAllMembers()
        {
            return _context.Members.Include(m => m.MembershipType).ToList();
        }

        public Member? GetByEmail(string email)
        {
            return _context.Members.Include(m => m.MembershipType).FirstOrDefault(m=>m.Email==email);
        }

        public Member? GetById(int memberId)
        {
             return _context.Members.Include(m => m.MembershipType).FirstOrDefault(m => m.MemberId == memberId);
        }

        public bool UpdateMember(Member member)
        {
            Member? oldMember =  _context.Members.Find(member.MemberId);
            if (oldMember == null)
            {
                throw new Exception("Member Not Found");
            }
            oldMember.MembershipTypeId = member.MembershipTypeId;
            oldMember.MemberRole = member.MemberRole;
            oldMember.Phone = member.Phone;
            oldMember.Name = member.Name;
            oldMember.IsActive = member.IsActive;

            _context.SaveChanges();
            return true;
        }
    }
}