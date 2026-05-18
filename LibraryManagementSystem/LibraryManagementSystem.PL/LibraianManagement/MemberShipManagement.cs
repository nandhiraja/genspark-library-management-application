using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.PL
{
    
    public partial class  Program
    {
        

        public void _handleMembershipManagement()
                 {
                 Console.WriteLine("1.View All Membership \n2.Add Membership \n3.Update Membership \n4.Exit");
                 string adminInput = Console.ReadLine()??"";
                 switch (adminInput)
                 {
                     case "1":
                         _ShowAllMembership();
                         break;
                     case "2":
                         _AddMembership();
                         break;

                    case "3":
                        _UpdateMembership();
                         break;
                     case "4":
                         return;
                     default:
                         Console.WriteLine("Enter the Valid option");
                         break;
                }
        }



        private void _AddMembership()
        {
            Console.WriteLine(" =================== Add new Membership ======================\n");
            Console.Write("Enter MemberShip Name : ");
            string membershipName = Console.ReadLine()??"";
            Console.WriteLine();
             Console.Write("Enter Max book allowed to borrow without returning : ");
            if(int.TryParse(Console.ReadLine(), out int maxBook))
            {
                Console.WriteLine();
                Console.Write("Enter Max days allow to keep borrowed book : ");
                if(int.TryParse(Console.ReadLine(), out int maxDays))
                {
                    MembershipType membership = new MembershipType
                     {
                        Name = membershipName,
                        MaxBooks = maxBook,
                        MaxDays =maxDays
                     };
                    if (memberShipService.AddMemberShip(membership))
                    {
                        Console.WriteLine("New Membership added ");
                    }
                    else
                    {
                        Console.WriteLine("Unable to create new Membership  ");

                    }
                 }
            }
 
        }

        private void _UpdateMembership()
        {
            _ShowAllMembership();
            Console.Write("Enter the MembershipType Id to update : ");

            if(!int.TryParse(Console.ReadLine(), out int membershipId))
            {
                Console.WriteLine("Invalid Id");
                return;
            }

            MembershipType? membership = memberShipService.GetMemberShipById(membershipId);
            if(membership == null)
            {
                Console.WriteLine("Membership not found");
                return;
            }

            Console.WriteLine("What to update? \n1.Name \n2.Max Books \n3.Max Days");
            string choice = Console.ReadLine()??"";

            switch(choice)
            {
                case "1":
                    Console.Write($"Current Name : {membership.Name} | New Name : ");
                    membership.Name = Console.ReadLine()??"";
                    break;
                case "2":
                    Console.Write($"Current MaxBooks : {membership.MaxBooks} | New MaxBooks : ");
                    if(int.TryParse(Console.ReadLine(), out int newMaxBooks))
                        membership.MaxBooks = newMaxBooks;
                    break;
                case "3":
                    Console.Write($"Current MaxDays : {membership.MaxDays} | New MaxDays : ");
                    if(int.TryParse(Console.ReadLine(), out int newMaxDays))
                        membership.MaxDays = newMaxDays;
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    return;
            }

            if(memberShipService.UpdateMemberShip(membership))
            {
                Console.WriteLine("Membership updated successfully");
            }
        }


        private void _ShowAllMembership()
        {   
            Console.WriteLine("\n============================== Available MemberShipType ========================================\n");

            List<MembershipType>? allMemberships = memberShipService.GetAllMembershipType();
            if(allMemberships == null || allMemberships.Count == 0)
            {
                Console.WriteLine("No MemberShips are available");
                return;
            }
            int index = 1;
            foreach (var memberShip in allMemberships)
            {
                Console.WriteLine($"SI No: {index++} | Id: {memberShip.MembershipTypeId} | Name: {memberShip.Name} | MaxBooks: {memberShip.MaxBooks} | MaxDays: {memberShip.MaxDays}");
            }
        }
     
    }
}