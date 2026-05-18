using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Core.Enums;

namespace LibraryManagementSystem.PL
{
    
    public partial class  Program
    {

        private void _handleBorrowBooks(Member member)
        {
            Console.WriteLine("========================== Borrow Books ====================\n");
            Console.WriteLine("Choose option: \n1.See all books \n2.Search book");
            switch (Console.ReadLine() ?? "")
            {
                case "1":
                    _showAllBookTitles();
                    break;
                case "2":
                    _handleSearchBooks();
                    break;
                default:
                    Console.WriteLine("Enter valid input...");
                    break;
            }
            Console.Write("\nEnter Book Title Id to borrow :  ");
            if(int.TryParse(Console.ReadLine(),out int bookTitleId)){
                BookTitle? bookTitle = bookService.GetBookTitleByID(bookTitleId);
                if(bookTitle != null){
                    try{
                        borrowService.BorrowBook(member,bookTitle);
                        Console.WriteLine("Book borrowed successfully!");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Unable to borrow: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Book not found");
                }
            }
        }

        private void _handleReturnBook(Member member)
        {
            Console.WriteLine("========================== Return Book ====================\n");

            // show active borrows for this member
            var activeBorrows = borrowService.GetActiveBorrows(member.MemberId);

            if(activeBorrows == null || activeBorrows.Count == 0)
            {
                Console.WriteLine("You have no active borrows\n");
                return;
            }

            Console.WriteLine("Your Active Borrows :\n");
            foreach(var borrow in activeBorrows)
            {
                Console.WriteLine($"BorrowId: {borrow.BorrowId} | Book: {borrow.BookCopy?.BookTitle?.Title} | Due: {borrow.DueDate.ToShortDateString()} | Status: {borrow.Status}");
            }

            Console.Write("\nEnter BorrowId to return : ");
            if(!int.TryParse(Console.ReadLine(), out int borrowId))
            {
                Console.WriteLine("Invalid BorrowId");
                return;
            }

            BorrowTransaction? selectedBorrow = activeBorrows.FirstOrDefault(b => b.BorrowId == borrowId);
            if(selectedBorrow == null)
            {
                Console.WriteLine("Borrow record not found");
                return;
            }

            Console.WriteLine("Is the book in good condition?");
            Console.WriteLine("1.Good (Available)\n2.Damaged\n3.Lost");
            string conditionChoice = Console.ReadLine()??"";

            BookStatus bookStatus = BookStatus.Available;
            switch(conditionChoice)
            {
                case "1":
                    bookStatus = BookStatus.Available;
                    break;
                case "2":
                    bookStatus = BookStatus.Damaged;
                    break;
                case "3":
                    bookStatus = BookStatus.Lost;
                    break;
                default:
                    Console.WriteLine("Invalid option, defaulting to Available");
                    break;
            }

            try
            {
                Fine fine = bookReturnService.ReturnBook(member, selectedBorrow, bookStatus);
                Console.WriteLine("\nBook returned successfully!");

                if(fine.Amount > 0)
                {
                    Console.WriteLine($"Late return fine : Rs  {fine.Amount}");
                    Console.WriteLine("Please pay the fine to continue borrowing");
                }
                else
                {
                    Console.WriteLine("No fine applied returned on time");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unable to return book: {ex.Message}");
            }
        }

        private void _ShowUserBorrowHistory(Member member)
        {
            Console.WriteLine("========================== My Borrow History ====================\n");
            reportService.ShowMemberBorrowHistory(member.MemberId);
        }

        private void _ShowUserFineHistory(Member member)
        {
            Console.WriteLine("========================== My Fines ====================\n");

            List<Fine> unpaidFines = fineService.GetAllUnPaidFinesListById(member.MemberId);

            if(unpaidFines == null || unpaidFines.Count == 0)
            {
                Console.WriteLine("No pending fines\n");
            }
            else
            {
                Console.WriteLine("Pending Fines :\n");
                foreach(var fine in unpaidFines)
                {
                    Console.WriteLine($"FineId: {fine.FineId} | BorrowId: {fine.BorrowId} | Amount: Rs.{fine.Amount} | Paid: {fine.IsPaid}");
                }

                decimal total = unpaidFines.Sum(f => f.Amount);
                Console.WriteLine($"\nTotal Pending : Rs.{total}");
                Console.WriteLine("\nPay a fine? yes/no");
                string answer = Console.ReadLine()??"";

                if(answer.ToLower() == "yes")
                {
                    Console.Write("Enter FineId to pay : ");
                    if(int.TryParse(Console.ReadLine(), out int fineId))
                    {
                        Fine? fine = fineService.GetFine(fineId);
                        if(fine == null)
                        {
                            Console.WriteLine("Fine not found");
                            return;
                        }
                        fine.IsPaid = true;
                        fine.PaidDate = DateTime.Now;

                        if(fineService.UpdateFine(fine))
                        {
                            Console.WriteLine($"Fine Rs.{fine.Amount} paid successfully");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid FineId");
                    }
                }
            }
        }

        private void _handleSearchBooks(){
            while(true){
            Console.Write("\nSearch (Type: Title/ Author / Category or [to exit type 'exit'] ) : ");
            string searchInput = Console.ReadLine()??"";

            searchInput = searchInput.ToLower();
            if(searchInput == "exit"){return;}
            List<BookTitle>? bookTitles = bookService.searchBooks(searchInput);
                if (bookTitles == null || bookTitles.Count == 0)
                {
                    Console.WriteLine("No books found");
                }
                else
                {
                    foreach(var books in bookTitles)
                    {
                        Console.WriteLine($"Id: {books.BookTitleId} | Title: {books.Title} | Author: {books.Author} | Category: {books.Category?.Name}");
                    }
                }
            
            }
        }
    }
}