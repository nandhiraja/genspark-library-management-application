namespace LibraryManagementSystem.Core.Models
{
    
    public class Fine
    {       
           public int FineId { get; set; }
           public int BorrowId { get; set; }
           public BorrowTransaction BorrowTransaction { get; set; } = null!;
           public decimal Amount { get; set; }
           public bool IsPaid { get; set; }
           public DateTime? PaidDate { get; set; }

           public Fine(){}
    }
}