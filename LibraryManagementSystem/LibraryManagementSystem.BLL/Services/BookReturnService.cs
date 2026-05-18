using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;
using LibraryManagementSystem.DAL.Repositories;
using LibraryManagementSystem.Core.Enums;


namespace LibraryManagementSystem.BLL.Services
{
    public class BookReturnService
    {   
        private BookRepository _bookRepository;
        private BorrowRepository _borrowRepository;
        private FineRepository _fineRepository;
        private LibraryDbContext libraryDbContext;

        public BookReturnService()
        {
            libraryDbContext = new LibraryDbContext();
            _bookRepository = new BookRepository(libraryDbContext);
            _borrowRepository = new BorrowRepository(libraryDbContext);
            _fineRepository = new FineRepository(libraryDbContext);
        }

        public Fine ReturnBook(Member member, BorrowTransaction borrowTransaction, BookStatus bookStatus)
        {
            borrowTransaction.ReturnDate = DateTime.Now;
            int fineAmount = 0;

            if (borrowTransaction.ReturnDate.HasValue && borrowTransaction.ReturnDate > borrowTransaction.DueDate)
            {
                TimeSpan delay = borrowTransaction.ReturnDate.Value.Subtract(borrowTransaction.DueDate);
                int daysLate = delay.Days;
                fineAmount = daysLate * 10;
            }

            Fine fine = new Fine
            {
                BorrowId = borrowTransaction.BorrowId,
                Amount = fineAmount,
                IsPaid = fineAmount == 0,
            };

            borrowTransaction.Status = BorrowStatus.Returned;
            _borrowRepository.Update(borrowTransaction);

            // update book copy status back to available 
            BookCopy? borrowedCopy = _bookRepository.GetBookCopyById(borrowTransaction.BookCopyId);
            if(borrowedCopy != null)
            {
                borrowedCopy.Status = bookStatus;
            }

            _fineRepository.Add(fine);

            return fine;
        }

    }
}