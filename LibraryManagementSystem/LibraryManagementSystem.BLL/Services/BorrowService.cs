using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;
using LibraryManagementSystem.DAL.Repositories;
using LibraryManagementSystem.Core.Enums;

namespace LibraryManagementSystem.BLL.Services
{
    public class BorrowService
    {
        private readonly LibraryDbContext _context;

        private readonly BorrowRepository _borrowRepository;
        private readonly BookRepository _bookRepository;
        private readonly FineRepository _fineRepository;

        private readonly decimal _maxAllowedFineAmount = 500;

        public BorrowService()
        {
            _context = new LibraryDbContext();

            _borrowRepository = new BorrowRepository(_context);
            _bookRepository = new BookRepository(_context);
            _fineRepository = new FineRepository(_context);
        }

        public bool BorrowBook(Member member, BookTitle bookTitle)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                if (!member.IsActive)
                {
                    throw new Exception("Member account is inactive");
                }

                decimal unpaidFine = GetUnPaidFineTotalAmount(member);

                if (unpaidFine > _maxAllowedFineAmount)
                {
                    throw new Exception("Fine limit exceeded");
                }

                int activeBorrowCount =_borrowRepository.GetActiveBorrowingCount(member.MemberId);

                if (activeBorrowCount >= member.MembershipType.MaxBooks)
                {
                    throw new Exception("Borrow limit reached");
                }

                bool alreadyBorrowed = _borrowRepository.HasActiveBorrowingForBook( member.MemberId,bookTitle.BookTitleId);

                if (alreadyBorrowed)
                {
                    throw new Exception("Member already borrowed this book");
                }

                BookCopy? availableCopy = _bookRepository.GetAvailableCopy(bookTitle.BookTitleId);

                if (availableCopy == null)
                {
                    throw new Exception("No available copy found");
                }

                BorrowTransaction borrowTransaction =new BorrowTransaction()
                                                    {
                                                        MemberId = member.MemberId,
                                                        BookCopyId = availableCopy.BookCopyId,
                                                        BorrowDate = DateTime.UtcNow,
                                                        DueDate = DateTime.UtcNow.AddDays(member.MembershipType.MaxDays),
                                                        Status = BorrowStatus.Borrowed
                                                    };

                _borrowRepository.Add(borrowTransaction);
                //Update Book Copy Status
                availableCopy.Status = BookStatus.Borrowed;

                _context.SaveChanges();
                transaction.Commit();

                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();

                throw;
            }
        }

        private decimal GetUnPaidFineTotalAmount(Member member)
        {
            return _fineRepository.GetTotalUnpaidFine(member.MemberId);
        }

        public List<BorrowTransaction> GetActiveBorrows(int memberId)
        {
            return _borrowRepository.GetActiveBorrowsByMember(memberId);
        }

           public BorrowTransaction? GetBorrowsTransactionByID(int borrowId)
        {
            return _borrowRepository.GetById(borrowId);
        }
    }
}