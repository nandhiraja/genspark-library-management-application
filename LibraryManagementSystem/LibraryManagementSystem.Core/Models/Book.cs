
using  LibraryManagementSystem.Core.Enums;
namespace LibraryManagementSystem.Core.Models
{
    public class BookCopy
    {
        public int BookCopyId { get; set; }

        public string CopyCode { get; set; } = null!;

        public BookStatus Status { get; set; }

        public int BookTitleId { get; set; }

        public BookTitle BookTitle { get; set; } =null!;

        public ICollection<BorrowTransaction>? BorrowTransactions { get; set; }

    }
}