
using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.PL
{
    
    public partial class  Program
    {
        
        public void _handleBookManagement()
        {
             Console.WriteLine("Books Management options\n");
                Console.WriteLine("1.BookTitleManagement\n2. BookCopiesManagement \n3.Exit");

                 string adminInput = Console.ReadLine()??"";
                 switch (adminInput)
                 {
                     case "1":
                         _handleBookTitleManagement();
                         break;
                     case "2":
                         _handleBookCopyManagement();
                         break;
                     case "3":
                         return;
                     default:
                         Console.WriteLine("Enter the Valid option");
                         break;
    
                }
        }

     
       
       



// ========================================== Management implmentations ================================================





        
        public void _handleBookTitleManagement()
        {
               Console.WriteLine("1.Add New book \n2. Update existing books \n3.Delete Existing Book \n4.Exit");
                string adminInput = Console.ReadLine()??"";
                switch (adminInput)
                 {
                     case "1":
                         _addNewBook();
                         break;
                     case "2":
                         _updateBook();
                         break;
                    case "3":
                        Console.WriteLine("Not implement");
                        break;
                        
                     case "4":
                         return;
                     default:
                         Console.WriteLine("Enter the Valid option");
                         break;
   
                }
        }



          private void _handleBookCopyManagement()
        {
                Console.WriteLine("1.Add new Book Copy \n2. Update existing books Copy \n3.Delete Existing Book Copy \n4.Exit");
                string adminInput = Console.ReadLine()??"";
                switch (adminInput)
                 {
                     case "1":
                         _addNewBookCopy();
                         break;
                     case "2":
                         _updateBookCopy();
                         break;
                    case "3":
                        Console.WriteLine("Not implement");
                        break;
                        
                     case "4":
                         return;
                     default:
                         Console.WriteLine("Enter the Valid option");
                         break;
   
                }
        }

      // ==============================================. add methods  ========================================== 


        private void _addNewBook()
        {
            Console.WriteLine("\n========= Add New Book =========\n");
            Console.Write("Enter Book Title : ");
            string title = Console.ReadLine()??"";
            Console.Write("Enter Author Name : ");
            string author = Console.ReadLine()??"";
            Console.Write("Enter Published Year : ");

            if(!int.TryParse(Console.ReadLine(), out int publishedYear))
            {
                Console.WriteLine("Invalid year entered");
                return;
            }

            Console.WriteLine("\nAvailable Categories :");
            foreach(var cat in categoryService.ListAllCategory())
            {
                Console.WriteLine($"  Id: {cat.CategoryId} - {cat.Name}");
            }
            Console.Write("Enter Category Id : ");

            if(!int.TryParse(Console.ReadLine(), out int categoryId))
            {
                Console.WriteLine("Invalid category Id");
                return;
            }

            BookTitle newBook = new BookTitle
            {
                Title = title,
                Author = author,
                PublishedYear = publishedYear,
                CategoryId = categoryId
            };

            try
            {
                if(bookService.AddBookTitle(newBook))
                {
                    Console.WriteLine($"Book '{title}' added successfully");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unable to add book: {ex.Message}");
            }
        }

        private void _addNewBookCopy()
        {
            Console.WriteLine("Enter the book copy Code eg : BC-xxxx");
            string copyCode = Console.ReadLine()??"";
            Console.WriteLine("Enter the book Book Title Id");
            _showAllBookTitles();
            if(int.TryParse(Console.ReadLine(),out int bookTitleId))
            {
                BookCopy bookCopy = new BookCopy();
                bookCopy.CopyCode = copyCode;
                bookCopy.BookTitleId=bookTitleId;

                try{
                    if (bookService.AddBookCopy(bookCopy))
                    {
                     Console.WriteLine("Book copy added successfully..");
                    }
                    else
                    {
                      Console.WriteLine("Failed to Add new Book copy added ..");
                    }
                }
                catch(Exception ex)
                {   
                    Console.WriteLine("Unable to add copy",ex);
                }
                
            }
        }


      // ==============================================. update  methods  ========================================== 




        private void _updateBook()
        {
            _showAllBookTitles();
            Console.Write("Enter the book need to update : ");
            if(int.TryParse(Console.ReadLine(), out int bookId)){ 

                BookTitle? bookTitle =  bookService.GetBookTitleByID(bookId);
                if(bookTitle == null) return;
                Console.Write("What to edit: \n1.Title\n2.Author\n3.Categroy");
                string updatePref = Console.ReadLine()??"";
                switch (updatePref)
                {
                    case "1":
                        Console.WriteLine($"Update the exist Title : {bookTitle.Title}"); 
                        bookTitle.Title = Console.ReadLine()??"";   // no input validation done now 
                        break;
                    case "2":
                        Console.WriteLine($"Update the exist Author : {bookTitle.Author}"); 
                        bookTitle.Author = Console.ReadLine()??"";   // no input validation done now 
                        break;
                    case "3":
                        Console.WriteLine("Avaiable categories are : \n");
                        foreach(var category in categoryService.ListAllCategory())
                        {
                            Console.WriteLine($"Id : {category.CategoryId} - {category.Name}");
                        }
                        Console.WriteLine($"Update the exist Category : {bookTitle.Category.Name}\n Enter new Id"); 

                        if(int.TryParse(Console.ReadLine(),out int newcategoryId))
                        {
                            bookTitle.CategoryId = newcategoryId;
                        }  
                        break;
                    default:
                        Console.WriteLine("Enter valid option");
                        break;
                }
                bookService.UpdateBookTitle(bookTitle);

            }
        }


        private void _updateBookCopy()
        {
            Console.WriteLine("\n========= Update Book Copy Status =========\n");
            foreach(var copy in bookService.GetAllBookCopy())
            {
                Console.WriteLine($"Id: {copy.BookCopyId} | Code: {copy.CopyCode} | Title: {copy.BookTitle?.Title} | Status: {copy.Status}");
            }
            Console.Write("Enter BookCopy Id to update : ");

            if(!int.TryParse(Console.ReadLine(), out int copyId))
            {
                Console.WriteLine("Invalid Id");
                return;
            }

            BookCopy? bookCopy = bookService.GetBookCopyByID(copyId);
            if(bookCopy == null)
            {
                Console.WriteLine("Book copy not found");
                return;
            }

            Console.WriteLine($"Current status: {bookCopy.Status}");
            Console.WriteLine("1.Available\n2.Damaged\n3.Lost");
            string choice = Console.ReadLine()??"";

            switch(choice)
            {
                case "1":
                    bookCopy.Status = Core.Enums.BookStatus.Available;
                    break;
                case "2":
                    bookCopy.Status = Core.Enums.BookStatus.Damaged;
                    break;
                case "3":
                    bookCopy.Status = Core.Enums.BookStatus.Lost;
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    return;
            }

            if(bookService.UpdateBookCopy(bookCopy))
            {
                Console.WriteLine($"Book copy status updated to {bookCopy.Status}");
            }
        }


      
// ===========================================   Show all functionds ====================================================

         private void _showAllBookTitles()
        {   
            Console.WriteLine(" ========================= All available books =============================\n");
            foreach(var bookTitle in bookService.ViewBooks())
            {
                Console.WriteLine(bookTitle.ToString());
            }
             Console.WriteLine(" ========================= ==================== =============================\n");

        }

       

    
      

    }

}