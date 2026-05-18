using System.Security.Cryptography.X509Certificates;
using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.BLL.Interfaces
{
    public interface IFineService
    {
        public bool AddNewFine(Fine fine);

        public decimal GetTotalFineAmountById(int memberId);
        public List<Fine> GetAllUnPaidFinesListById(int memberId);
        public List<Fine> GetAllUnPaidFines();
        public List<Fine> GetAllFines();

        public bool UpdateFine(Fine fine);

    }
}