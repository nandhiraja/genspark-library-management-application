using LibraryManagementSystem.BLL.Interfaces;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;
using LibraryManagementSystem.DAL.Repositories;

namespace LibraryManagementSystem.BLL.Services
{
    public class MemberShipService:IMemberShipService
    {
        private MemberShipRepository _memberShipRepository;
        private LibraryDbContext _context;
        
        public MemberShipService()
        {
            _context = new LibraryDbContext();
            _memberShipRepository =  new MemberShipRepository(_context);
        }

        public bool AddMemberShip(MembershipType membershipType)
        {
            try{
                _memberShipRepository.AddMemberShipType(membershipType);
                return true;
            }
            catch(Exception ex)
            {   
                Console.WriteLine("Unable to add new memebership type: ",ex);
                return false;
            }
        }

      
        public List<MembershipType>? GetAllMembershipType()
        {
            return _memberShipRepository.GetAllMemberShipType();
        }

        public MembershipType? GetMemberShipById(int memberShipTypeId)
        {
            return _memberShipRepository.GetMembershipType(memberShipTypeId);
        }

        public bool UpdateMemberShip(MembershipType membershipType)
        {
            return _memberShipRepository.UpdateMemberShipType(membershipType);
        }
          public bool DeleteMemberShip(MembershipType membershipType)
        {
            return _memberShipRepository.DeleteMemberShipType(membershipType);
        }
    }
}