using System.Reflection.Metadata;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.BLL.Services;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq.Expressions;
using System.Net;
using System.Data;


namespace LibraryManagementSystem.PL
{
    public partial class Program
    {   
        private BookService bookService;
        private FineService fineService;
        private MemberService memberService;
        private BookReturnService bookReturnService;
        private BorrowService borrowService;
        private ReportService reportService;
        private MemberShipService memberShipService;
        private CategoryService categoryService;

    
        Program()
        {   
            memberService= new MemberService();
            bookService = new BookService();
            borrowService = new BorrowService();
            bookReturnService =new BookReturnService();
            fineService = new FineService();
            reportService = new ReportService();
            memberShipService = new MemberShipService();
            categoryService = new CategoryService();



        }

        void Run()
        {   
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n ================================== Library ========================================\n");
            Console.ResetColor();
            while(true){
                Console.WriteLine("\t1.Login\n\t2.Register Member\n\t3.Quit App\n");
                string userInput = Console.ReadLine()??"";
                switch (userInput)
                {
                   case "1":
                        _handleLogin();
                        break;
                   case "2":
                        Member? member = _handleRegistration();

                        if(member==null) Console.WriteLine("Unable to register a Member");
                        else Console.WriteLine($"Register successfully - Welcome {member.Name}\n");

                        break;
                    case "3":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\n================================ --- Thank you --- =================================\n");
                        Console.ResetColor();
                        return;
                    default:
                        Console.WriteLine("\nEnter the valid input\n");
                        break;

                }
            }
        }


       private Member? _handleRegistration()
        {   
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n============================= Registration ======================================\n");
            Console.ResetColor();
            Console.Write("Enter your Full Name : ");
            string userName = Console.ReadLine()??"";
            Console.WriteLine();
            Console.Write("Enter your Email id : ");
            string userEmail = Console.ReadLine()??"";
            Console.WriteLine();
            
            Console.Write("Enter your password : ");
            string userPassword = Console.ReadLine()??"";
            Console.WriteLine();

            Console.Write("Enter your Phone No : ");
            string userPhoneNo = Console.ReadLine()??"";
          

            _ShowAllMembership();    // show all the MemberShip 
            Member? registerMember = null;

            List<MembershipType>? memberships = memberShipService.GetAllMembershipType();
            if(memberships == null)
            {
                    registerMember = new Member(){
                        Name=userName,
                        Email = userEmail,
                        Phone =userPhoneNo,
                        Password = userPassword,
                        MembershipTypeId = 0, // assume now as 0 as default membership
                        MemberRole = Enums.Role.User
                    };   
            }
            else
            {
                    for(int attmpt=0; attmpt<3;attmpt++) // Give 3 chance to choose the valid membership
                    {
                    Console.Write("\nEnter your MemberShip ID/SI No \n");

                        if (int.TryParse(Console.ReadLine() ,out int membershipIndex))
                        {
                            if(membershipIndex<= memberships.Count)
                            {
                                    registerMember = new Member(){
                                        Name=userName,
                                        Email = userEmail,
                                        Phone =userPhoneNo,
                                        Password = userPassword,
                                        MembershipTypeId = memberships[membershipIndex].MembershipTypeId,
                                        MemberRole = Enums.Role.User
                                    };
                                break;   // Stop if user enter valid input
                            }
                            else
                            {
                                Console.WriteLine($"Membership not avilalbe in Id {membershipIndex}");
                                continue;
                            }
                        }

                        else
                        {
                            Console.WriteLine("Unalbe to get Membership please enter valid input SiNo");
                        }
                     }
            }
            try
            {
                if ( registerMember != null && memberService.AddMember(registerMember))
                {
                  return memberService.GetMemberByEmail(userEmail);  
                }
                
            }
            catch(Exception ex)
             {
              Console.WriteLine("Unable to register new memeber",ex);
             }
                
            
            return registerMember;
        }

        private void _handleLogin()
        {
            Member? currentMember = null;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n================================= LOGIN ==========================================\n");
            Console.ResetColor();
            Console.Write("Enter your Email id : ");
            string userEmail = Console.ReadLine()??"";
            Console.Write("Enter your Password : ");
            string userPassword = Console.ReadLine()??"";

            try{
                currentMember = memberService.GetMemberByEmail(userEmail);

                if (currentMember == null)
                {
                    Console.WriteLine($"Member Not Found : {userEmail} .. Try to register new\n");
                    return;
                }

                if(currentMember.Password != userPassword)
                {
                    Console.WriteLine("Invalid password. Please try again\n");
                    return;
                }

                if(currentMember.MemberRole == Enums.Role.User)
                {
                    _HandleMemberOperations(currentMember);
                }
                else if(currentMember.MemberRole == Enums.Role.Admin)
                {
                    _HandleAdminOperations(currentMember);
                }
                else
                {
                    Console.WriteLine("Sorry Unable to identify your access role\n");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Unable to Login: {e.Message}\n");
            }
        }
         private  void _HandleAdminOperations(Member member)
        {
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n==================== Library Management : Access - Admin ========================= \n");
            Console.WriteLine($"Admin Name : {member.Name} \n=================================== \n");
            Console.ResetColor();

            while (true)
            {
   
                 Console.WriteLine("\nOur Management options\n");
                 Console.WriteLine("\t1.Book Management\n\t2.User Management\n\t3.Category Management\n\t4.Membership Management\n\t5.Report\n\t6.Exit");
                 string adminInput = Console.ReadLine()??"";

                // all handle by partial admin helper
                 switch (adminInput)
                 {
                     case "1":
                         _handleBookManagement();
                         break;
                     case "2":
                         _handleUserManagement();
                         break;
                     case "3":
                         _handleCategoryManagement();
                         break;
                     case "4":
                         _handleMembershipManagement();
                         break;
                     case "5":
                         _handleReportManagement();
                         break;
                     case "6":
                         return;
                     default:
                         Console.WriteLine("Enter the Valid option");
                         break;

                 }
            }

        }

        private void _HandleMemberOperations(Member member)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n======================== Library  : Access - User ================================ \n");
            Console.WriteLine($"User Name : {member.Name} \n=================================== \n");
            Console.ResetColor();

            while (true)
            {
   
                 Console.WriteLine("\nChoose options\n");
                 Console.WriteLine("\t1.See all books \n\t2.Borrow New Book \n\t3.Return Books\n\t4.My Borrow History \n\t5.My Fine History \n\t6.Exit");
                 string adminInput = Console.ReadLine()??"";

                // all handle by partial admin helper
                 switch (adminInput)
                 {
                     case "1":
                         _showAllBookTitles();
                         
                         break;
                     case "2":
                         _handleBorrowBooks(member);
                         break;
                     case "3":
                         _handleReturnBook(member);
                         break;
                     case "4":
                         _ShowUserBorrowHistory(member);
                         break;
                     case "5":
                         _ShowUserFineHistory(member);
                         break;
                     case "6":
                         return;
                     default:
                         Console.WriteLine("Enter the Valid option");
                         break;

                 }
            }
        }

       

        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}