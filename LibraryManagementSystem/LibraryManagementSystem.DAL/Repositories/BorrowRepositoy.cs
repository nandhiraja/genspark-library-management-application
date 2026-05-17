
using System.Transactions;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;
using LibraryManagementSystem.DAL.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Repositories
{
    public class BorrowRepository : IBorrowTransactionRepository
    {
        readonly LibraryDbContext _context;
        public BorrowRepository(LibraryDbContext libraryDbContext)
        {
            _context = libraryDbContext;
        }
        public void Add(BorrowTransaction transaction)
        {
            _context.BorrowTransactions.Add(transaction);
            _context.SaveChanges();

        }

        public int GetActiveBorrowingCount(int memberId)
        {
            List<BorrowTransaction> transactions = _context.BorrowTransactions
                                                                        .Where(t=> t.MemberId ==memberId 
                                                                        && (t.Status== Enums.BorrowStatus.Borrowed || t.Status == Enums.BorrowStatus.Overdue))
                                                                        .ToList();
            return transactions.Count;
        }

        public List<BorrowTransaction> GetActiveBorrowsByMember(int memberId)
        {
            List<BorrowTransaction> transactions = _context.BorrowTransactions.Where(t=> t.MemberId == memberId).ToList(); 
            return transactions;  
        }

        public BorrowTransaction? GetActiveBorrowTransaction(int memberId, int bookCopyId)
        {
            return _context.BorrowTransactions.FirstOrDefault(t=> t.MemberId ==memberId 
                                                                                && t.BookCopyId==bookCopyId
                                                                                && (t.Status== Enums.BorrowStatus.Borrowed 
                                                                                || t.Status == Enums.BorrowStatus.Overdue)
                                                                                );
                                                
                                                                        
        }

        public List<BorrowTransaction> GetBorrowHistoryByMember(int memberId)
        {
             List<BorrowTransaction> transactions = _context.BorrowTransactions.Where(t=> t.MemberId ==memberId ).ToList();
             return transactions;
        }

        public BorrowTransaction? GetById(int borrowId)
        {
             BorrowTransaction? borrowTransactions = _context.BorrowTransactions.Find(borrowId );
             return borrowTransactions;      
         }

        public bool HasActiveBorrowingForBook(int memberId, int bookTitleId)
        {
            bool isHaveActiveBorrow = _context.BorrowTransactions.Any( t=>t.MemberId == memberId && t.BookCopy.BookTitleId==bookTitleId 
                                                                                        && (t.Status == Enums.BorrowStatus.Borrowed || t.Status == Enums.BorrowStatus.Overdue));
             return isHaveActiveBorrow; 
        }

        public void Update(BorrowTransaction transaction)
        {
             BorrowTransaction? borrowTransactions = _context.BorrowTransactions.FirstOrDefault( t=>t.BorrowId == transaction.BorrowId );
                if(borrowTransactions!=null){
                borrowTransactions.Status = transaction.Status;
                borrowTransactions.DueDate = transaction.DueDate;

                borrowTransactions.ReturnDate = transaction.ReturnDate;
                
                borrowTransactions.BorrowDate = transaction.BorrowDate;
                }
                _context.SaveChanges();

            }
    }

}