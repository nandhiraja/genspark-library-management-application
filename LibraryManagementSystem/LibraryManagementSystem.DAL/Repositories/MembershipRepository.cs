using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;

namespace LibraryManagementSystem.DAL.Repositories
{
    public class MemberShipRepository{

        readonly LibraryDbContext _context;
        public MemberShipRepository(LibraryDbContext libraryDbContext)
        {
            _context =libraryDbContext;
        }

        public void AddMemberShipType(MembershipType membershipType)
        {
            _context.MembershipTypes.Add(membershipType);
            _context.SaveChanges();
        }

        public List<MembershipType> GetAllMemberShipType()
        {
            return _context.MembershipTypes.ToList();
        }
        public MembershipType? GetMembershipType(int memberShipTypeId)
        {
            return _context.MembershipTypes.Find(memberShipTypeId);
        }

        public bool UpdateMemberShipType (MembershipType memberShip)
        {
            MembershipType? oldMembership =  _context.MembershipTypes.Find(memberShip.MembershipTypeId);
            if (oldMembership == null)
            {
                return false;
            }
            oldMembership.Name = memberShip.Name;
            oldMembership.MaxDays= memberShip.MaxDays;
            oldMembership.MaxBooks = memberShip.MaxBooks;
            _context.SaveChanges();
            return true;

        }

        public bool DeleteMemberShipType (MembershipType memberShip)
        {
             _context.MembershipTypes.Remove(memberShip);
             _context.SaveChanges();
              
            return true;
        }
    }
}