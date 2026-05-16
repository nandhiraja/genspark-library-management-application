using System.Security.Cryptography.X509Certificates;
using LibraryManagementSystem.Enums;

namespace LibraryManagementSystem.Core.Models
{
    
    public class Member
    {
         public int MemberId { get; set; }
         public string Name { get; set; } = null!;
         public string Email { get; set; }= null!;
         public string Phone { get; set; }= null!;
         public string Password { get; set; }= null!;
         public bool IsActive { get; set; }
         public Role  MemberRole {get;set;}
         public int MembershipTypeId { get; set; }
         public MembershipType MembershipType { get; set; } =null!;
         public ICollection<BorrowTransaction> BorrowTransactions { get; set; } = new List<BorrowTransaction>();

         public Member(){}

    }
}