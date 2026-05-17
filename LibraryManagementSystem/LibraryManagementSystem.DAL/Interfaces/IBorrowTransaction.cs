using LibraryManagementSystem.Core.Models;


namespace LibraryManagementSystem.DAL.Interface
{
    public interface IBorrowTransactionRepository
    {
        void Add(BorrowTransaction transaction);
        void Update(BorrowTransaction transaction);
        BorrowTransaction? GetById(int borrowId);
        List<BorrowTransaction> GetActiveBorrowsByMember(int memberId);
        int GetActiveBorrowingCount(int memberId);
        bool HasActiveBorrowingForBook( int memberId,int bookTitleId);

        BorrowTransaction? GetActiveBorrowTransaction( int memberId, int bookCopyId);

        // List<BorrowTransaction> GetOverdueBorrowings();

        List<BorrowTransaction> GetBorrowHistoryByMember(int memberId);
    }
}