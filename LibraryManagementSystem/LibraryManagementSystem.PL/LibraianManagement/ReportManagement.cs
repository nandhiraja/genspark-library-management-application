using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.PL
{
    
    public partial class  Program
    {
        
        public void _handleReportManagement()
        {
            while(true)
            {
                Console.WriteLine("\n================================== Reports ===============================\n");
                Console.WriteLine("1.Books Currently Borrowed");
                Console.WriteLine("2.Overdue Books");
                Console.WriteLine("3.Members with Pending Fines");
                Console.WriteLine("4.Most Borrowed Books");
                Console.WriteLine("5.Available Books");
                Console.WriteLine("6.Member Borrow History");
                Console.WriteLine("7.Exit");

                string choice = Console.ReadLine()??"";
                switch(choice)
                {
                    case "1":
                        reportService.ShowBorrowedBooks();
                        break;
                    case "2":
                        reportService.ShowOverdueBooks();
                        break;
                    case "3":
                        reportService.ShowMembersWithPendingFines();
                        break;
                    case "4":
                        reportService.ShowMostBorrowedBooks();
                        break;
                    case "5":
                        reportService.ShowAvailableBooks();
                        break;
                    case "6":
                        Console.Write("Enter Member Id : ");
                        if(int.TryParse(Console.ReadLine(), out int memberId))
                        {
                            reportService.ShowMemberBorrowHistory(memberId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Member Id");
                        }
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Enter valid option");
                        break;
                }
            }
        }

    }
}
