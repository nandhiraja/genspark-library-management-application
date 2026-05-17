using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;
using LibraryManagementSystem.DAL.Interface;

namespace LibraryManagementSystem.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {

        LibraryDbContext _context;
        public BookRepository(LibraryDbContext libraryDbContext)
        {
            _context = libraryDbContext;
        }
        public void AddBook(BookTitle bookTitle)
        {
           
            _context.BookTitles.Add(bookTitle);
            _context.SaveChanges();

        }

        public void AddBookCopy(BookCopy bookCopy)
        {
            
            _context.BookCopies.Add(bookCopy);
            _context.SaveChanges();
     
        }

        public List<BookTitle> GetAllBooks()
        {
            
            List<BookTitle> books = _context.BookTitles.ToList();   
            return books;
      
        }

        public BookCopy? GetAvailableCopy(int bookTitleId)
        {
             
            return _context.BookCopies.FirstOrDefault(b => b.BookTitleId== bookTitleId && b.Status == Enums.BookStatus.Available);
   
        }

        public BookTitle? GetBookById(int bookTitleId)
        {
            
            return  _context.BookTitles.Find(bookTitleId);
            
            
        }

        public BookCopy? GetBookCopyById(int bookId)
        {
            
            return  _context.BookCopies.Find(bookId);
            
        }

        public List<BookTitle> GetBooksByCategory(int categoryId)
        {
           
            return  _context.BookTitles.Where(b=> b.CategoryId==categoryId).ToList();
            
        }

        public List<BookTitle> SearchBooks(string keyword)
        {
            
            return  _context.BookTitles.Where(b=> b.Title.Contains(keyword)||
                                                    b.Author.Contains(keyword)||
                                                    b.Category.Name.Contains(keyword)).ToList();
            
            
        }

        public void UpdateCopy(BookCopy bookCopy)
        {
            
               BookCopy? book =  _context.BookCopies.FirstOrDefault(b=>b.CopyCode == bookCopy.CopyCode);
               if(book == null)
                {
                    throw new Exception("BookCopy Not Found");
                }
                book.Status = bookCopy.Status;
                book.BookTitle = bookCopy.BookTitle;
                book.BookTitleId = bookCopy.BookTitleId;

            
           
        }
    }
}
    
