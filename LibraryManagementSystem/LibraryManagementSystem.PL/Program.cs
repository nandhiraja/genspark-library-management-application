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
            Console.WriteLine("\n ==================================== Library =========================================\n");
            while(true){
                Console.WriteLine("\n1.Login\n2.Register Member\n3.Exit");
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
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Enter the valid input");
                        break;

                }
            }
        }


       private Member? _handleRegistration()
        {
            Console.WriteLine("\n============================== Registration ========================================\n");
            Console.Write("\nEnter your Full Name : \n");
            string userName = Console.ReadLine()??"";
            Console.WriteLine();
            Console.Write("\nEnter your Email id : \n");
            string userEmail = Console.ReadLine()??"";
            Console.WriteLine();
            
            Console.Write("\nEnter your password\n");
            string userPassword = Console.ReadLine()??"";
            Console.WriteLine();

            Console.Write("\nEnter your Phone No : \n");
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
                    Console.Write("\nEnter your MemberShip SI no \n");

                        if (int.TryParse(Console.ReadLine() ,out int membershipIndex)){
                            registerMember = new Member(){
                                Name=userName,
                                Email = userEmail,
                                Phone =userPhoneNo,
                                Password = userPassword,
                                MembershipTypeId = memberships[membershipIndex].MembershipTypeId,
                                MemberRole = Enums.Role.User
                            };

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
            Console.WriteLine("\n============================== LOGIN ========================================\n");
            Console.Write("\nEnter your Email id : ");
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
            

            Console.WriteLine("\n=============================== Library Management : Access - Admin =================================== \n");
            Console.WriteLine($"Admin Name : {member.Name} \n=================================== \n");
            while (true)
            {
   
                 Console.WriteLine("Our Management options\n");
                 Console.WriteLine("1.Book Management\n2.User Management\n3.Category Management\n4.Membership Management\n5.Report\n6.Exit");
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
            Console.WriteLine("\n=============================== Library  : Access - User =================================== \n");
            Console.WriteLine($"User Name : {member.Name} \n=================================== \n");
            while (true)
            {
   
                 Console.WriteLine("Choose options\n");
                 Console.WriteLine("1.See all books \n2.Borrow New Book \n3.Return Books\n4.My Borrow History \n5.My Fine History \n6.Exit");
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