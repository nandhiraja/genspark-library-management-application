using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;
using LibraryManagementSystem.DAL.Repositories;
using LibraryManagementSystem.Core.Enums;

namespace LibraryManagementSystem.BLL.Services
{
    public class ReportService
    {
        private readonly FineRepository _fineRepository;
        private readonly MemeberRepository _memberRepository;
        private readonly BorrowRepository _borrowRepository;
        private readonly BookRepository _bookRepository;

        private readonly LibraryDbContext _context;

        public ReportService()
        {
            _context = new LibraryDbContext();
            _fineRepository = new FineRepository(_context);

            _bookRepository = new BookRepository(_context);
            _borrowRepository = new BorrowRepository(_context);
            _memberRepository = new MemeberRepository(_context);
        }

        public void ShowBorrowedBooks()
        {
            List<BorrowTransaction> borrowedBooks =
                _borrowRepository
                .GetAllBorrowTransactions()
                .Where(t =>
                    t.Status == BorrowStatus.Borrowed ||
                    t.Status == BorrowStatus.Overdue)
                .ToList();

            Console.WriteLine("\n==================== CURRENTLY BORROWED BOOKS ====================\n");

            foreach (var transaction in borrowedBooks)
            {
                Console.WriteLine($"Member : {transaction.Member?.Name}");

                Console.WriteLine($"Book   : {transaction.BookCopy?.BookTitle?.Title}");

                Console.WriteLine( $"Copy   : {transaction.BookCopy?.CopyCode}");

                Console.WriteLine( $"Due    : {transaction.DueDate.ToShortDateString()}");

                Console.WriteLine("--------------------------------------------------\n");
            }
        }

       
        public void ShowOverdueBooks()
        {
            List<BorrowTransaction> overdueBooks =
                _borrowRepository
                .GetAllBorrowTransactions()
                .Where(t =>t.ReturnDate == null &&t.DueDate < DateTime.Now)
                .ToList();

            Console.WriteLine("\n==================== OVERDUE BOOKS ====================\n");

            foreach (var transaction in overdueBooks)
            {
                int delayedDays =(DateTime.Now - transaction.DueDate).Days;

                Console.WriteLine( $"Member : {transaction.Member?.Name} || Delayed Days : {delayedDays}");
                Console.WriteLine( $"Book   : {transaction.BookCopy?.BookTitle?.Title}");

                Console.WriteLine("--------------------------------------------------\n");
            }
        }

     

        public void ShowMembersWithPendingFines()
        {
            List<Fine> unpaidFines =
                _fineRepository
                .GetAllFines()
                .Where(f => !f.IsPaid)
                .ToList();

            Console.WriteLine("\n==================== MEMBERS WITH PENDING FINES ====================\n");

            foreach (var fine in unpaidFines)
            {
                Console.WriteLine($"Member : {fine.BorrowTransaction?.Member?.Name} | Amount : ₹{fine.Amount}");

                Console.WriteLine("--------------------------------------------------\n");
            }
        }

        public void ShowMostBorrowedBooks()
        {
            var mostBorrowed =
                _borrowRepository
                .GetAllBorrowTransactions()
                .GroupBy(t => t.BookCopy.BookTitle.Title)
                .Select(g => new
                {
                    Title = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToList();

            Console.WriteLine("\n==================== MOST BORROWED BOOKS ====================\n");

            foreach (var item in mostBorrowed)
            {
                Console.WriteLine(
                    $"{item.Title} --> Borrowed {item.Count} times");
            }
        }

    

        public void ShowAvailableBooks()
        {
            List<BookCopy> availableBooks =
                _bookRepository
                .GetAllBookCopies()
                .Where(b => b.Status == BookStatus.Available)
                .ToList();

            Console.WriteLine("\n==================== AVAILABLE BOOKS ====================\n");

            foreach (var book in availableBooks)
            {
                Console.WriteLine( $"Title : {book.BookTitle.Title}");
                Console.WriteLine($"Copy Code : {book.CopyCode}");

                Console.WriteLine("--------------------------------------------------\n");
            }
        }

      
        public void ShowMemberBorrowHistory(int memberId)
        {
            List<BorrowTransaction> history =
                _borrowRepository
                .GetBorrowHistoryByMember(memberId);

            Console.WriteLine("\n==================== MEMBER BORROW HISTORY ====================\n");

            foreach (var transaction in history)
            {
                Console.WriteLine( $"Book : {transaction.BookCopy.BookTitle.Title}");

                Console.Write($"Borrowed : {transaction.BorrowDate.ToShortDateString()} || ");
                Console.Write( $"Returned : {transaction.ReturnDate} || Status : {transaction.Status}");

                Console.WriteLine("--------------------------------------------------\n");
            }
        }
    }
}