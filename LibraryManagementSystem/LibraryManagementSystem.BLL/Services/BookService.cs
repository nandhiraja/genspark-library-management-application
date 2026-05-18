using LibraryManagementSystem.BLL.Interfaces;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;
using LibraryManagementSystem.DAL.Interface;
using LibraryManagementSystem.DAL.Repositories;

namespace LibraryManagementSystem.BLL.Services
{
    public class BookService: IBookService
    {
        private BookRepository _bookRepository;
        private LibraryDbContext _context;

        public BookService()
        {
            _context = new LibraryDbContext();
            _bookRepository = new BookRepository(_context);
        }

        public bool AddBookTitle(BookTitle bookTitle)
        {   
            try{
                _bookRepository.AddBook(bookTitle);
                return true;
            }
            catch
            {
                throw new Exception("Unable to add new book");
            }
        }

        public bool AddBookCopy(BookCopy bookCopy)
        {
            try {
                 _bookRepository.AddBookCopy(bookCopy);
                 return true;
            }
            catch
            {
                throw new Exception("Unable to add new book copy ");
            }
        }

        public List<BookTitle> ViewBooks()
        {
           return _bookRepository.GetAllBooks();
        }

        public BookTitle? GetBookTitleByID(int bookTitleId)
        {
            try{
                return _bookRepository.GetBookTitleById(bookTitleId);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unable to get the book title {bookTitleId}\n ",ex);
                return null;
            }
        }

        public BookCopy? GetBookCopyByID(int bookCopyId)
        {
             try{
                return _bookRepository.GetBookCopyById(bookCopyId);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unable to get the book Copy {bookCopyId}\n ",ex);
                return null;
            }
        }

        public List<BookTitle>? GetAllBookTitle()
        {
            return _bookRepository.GetAllBooks();
        }

        public List<BookCopy> GetAllBookCopy()
        {
            return _bookRepository.GetAllBookCopies();
        }

        public List<BookCopy> GetAllAvailableBookCopy()
        {
            return _bookRepository.GetAllBookCopies().Where(bc=>bc.Status == Core.Enums.BookStatus.Available).ToList();
        }

        public List<BookTitle>? searchBooks(string keyword)
        {
            return _bookRepository.SearchBooks(keyword);
        }

        public bool UpdateBookCopy(BookCopy bookCopy)
        {   
            try{
                _bookRepository.UpdateCopy(bookCopy);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unable to update the book Copy {bookCopy.BookCopyId}\n ",ex);
                return false;
            }
        }

        public bool UpdateBookTitle(BookTitle bookTitle)
        {
            try{
                _bookRepository.UpdateBook(bookTitle);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unable to update the book title {bookTitle.Title}\n ",ex);
                return false;
            }
        }

        public bool DeleteBookTitle(BookTitle bookTitle)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBookCopy(BookCopy bookCopy)
        {
            throw new NotImplementedException();
        }
    }
}
