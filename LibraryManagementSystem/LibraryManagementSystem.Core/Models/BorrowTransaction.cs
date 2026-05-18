using LibraryManagementSystem.Core.Enums;
namespace LibraryManagementSystem.Core.Models
{
public class BorrowTransaction
{
    public int BorrowId { get; set; }

    public int MemberId { get; set; }

    public Member? Member { get; set; }

    public int BookCopyId { get; set; }

    public BookCopy BookCopy { get; set; } =null!;

    public DateTime BorrowDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public BorrowStatus Status { get; set; }

    public Fine? Fine { get; set; }
}
}