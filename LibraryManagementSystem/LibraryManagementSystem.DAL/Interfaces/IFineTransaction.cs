using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.DAL.Interface
{
    public interface IFineRepository
    {
        void Add(Fine fine);
        void Update(Fine fine);
        Fine? GetById(int fineId);
        List<Fine> GetPendingFines(int memberId);
        List<Fine> GetFineHistory(int memberId);
        decimal GetTotalUnpaidFine(int memberId);
    }
}