using System.Security.Cryptography.X509Certificates;
using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.BLL.Interfaces
{
    public interface IMemberShipService
    {
        public bool AddMemberShip(MembershipType membershipType);

        public MembershipType? GetMemberShipById(int memberShipTypeId);
        public List<MembershipType>? GetAllMembershipType();

        public bool UpdateMemberShip(MembershipType membershipType);
        public bool DeleteMemberShip(MembershipType membershipType);

    }
}