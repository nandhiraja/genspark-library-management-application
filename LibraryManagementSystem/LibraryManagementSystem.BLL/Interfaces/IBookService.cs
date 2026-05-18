using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.BLL.Interfaces
{
    public interface IBookService
    {
        public bool AddBookTitle(BookTitle bookTitle);
        public bool AddBookCopy(BookCopy bookCopy);

        public BookTitle? GetBookTitleByID(int bookTitleId);
        public BookCopy? GetBookCopyByID(int bookCopyId);

        public  List<BookTitle>? GetAllBookTitle();
        public List<BookCopy> GetAllBookCopy();
        public List<BookCopy> GetAllAvailableBookCopy();

        public List<BookTitle>? searchBooks(string keyword);

        public bool UpdateBookTitle(BookTitle bookTitle);
        public bool UpdateBookCopy(BookCopy bookCopy);

        public bool DeleteBookTitle(BookTitle bookTitle);
        public bool DeleteBookCopy(BookCopy bookCopy);
    }
}