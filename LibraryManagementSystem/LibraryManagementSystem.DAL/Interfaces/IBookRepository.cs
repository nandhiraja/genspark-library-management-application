using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.DAL.Interface
{
   public interface IBookRepository
    {
        void AddBook(BookTitle bookTitle);
        void AddBookCopy(BookCopy bookCopy);
        void UpdateCopy(BookCopy bookCopy);
        BookTitle? GetBookById(int bookTitleId);
        BookCopy? GetBookCopyById(int bookCopyId);
        List<BookTitle> GetAllBooks();
        List<BookTitle> SearchBooks(string keyword);
        List<BookTitle> GetBooksByCategory(int categoryId);
        BookCopy? GetAvailableCopy(int bookTitleId);
    }
}