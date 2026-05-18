using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;
using LibraryManagementSystem.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Core.Enums;

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
            return _context.BookTitles.Include(bt => bt.Category).ToList();
        }

         public List<BookCopy> GetAllBookCopies()
        {
            return _context.BookCopies.Include(bc => bc.BookTitle).ToList();
        }

        public BookCopy? GetAvailableCopy(int bookTitleId)
        {
             
            return _context.BookCopies.FirstOrDefault(b => b.BookTitleId== bookTitleId && b.Status == BookStatus.Available);
   
        }

        public BookTitle? GetBookTitleById(int bookTitleId)
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
            BookCopy? book = _context.BookCopies.FirstOrDefault(b => b.CopyCode == bookCopy.CopyCode);
            if(book == null)
            {
                throw new Exception("BookCopy Not Found");
            }
            book.Status = bookCopy.Status;
            book.BookTitleId = bookCopy.BookTitleId;
            _context.SaveChanges();
        }

        public void UpdateBook(BookTitle bookTitle)
        {
            BookTitle? existing = _context.BookTitles.Find(bookTitle.BookTitleId);
            if(existing == null)
            {
                throw new Exception("BookTitle Not Found");
            }
            existing.Title = bookTitle.Title;
            existing.Author = bookTitle.Author;
            existing.CategoryId = bookTitle.CategoryId;
            existing.PublishedYear = bookTitle.PublishedYear;
            _context.SaveChanges();
        }
    }
}
