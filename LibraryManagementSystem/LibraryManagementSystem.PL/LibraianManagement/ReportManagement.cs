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
                Console.WriteLine("\t1.Books Currently Borrowed");
                Console.WriteLine("\t2.Overdue Books");
                Console.WriteLine("\t3.Members with Pending Fines");
                Console.WriteLine("\t4.Most Borrowed Books");
                Console.WriteLine("\t5.Available Books");
                Console.WriteLine("\t6.List All Members");
                Console.WriteLine("\t7.Member Borrow History");
                Console.WriteLine("\t8.Exit");

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
                        reportService.ShowAllMembers();
                        break;
                    case "7":
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
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Enter valid option");
                        break;
                }
            }
        }

    }
}
