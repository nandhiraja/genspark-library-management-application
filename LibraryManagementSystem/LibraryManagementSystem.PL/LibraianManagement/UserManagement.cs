
using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.PL
{
    
    public partial class  Program
    {
        public void _handleUserManagement()
        {
            Console.WriteLine("1.View All Members \n2.Update Member Active Status \n3.Search Member \n4.Exit");
            string adminInput = Console.ReadLine()??"";
            switch (adminInput)
            {
                case "1":
                    _ShowAllMembers();
                    break;
                case "2":
                    _handleMemberActiveStatus();
                    break;
                case "3":
                    _handleSearchMember();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Enter the Valid option");
                    break;
            }
        }

        private void _handleMemberActiveStatus()
        {
            Console.Write("Enter Member Id to update: ");
            if(int.TryParse(Console.ReadLine(), out int memberId))
            {
                Member? member = memberService.GetMemberById(memberId);
                if (member == null)
                {
                    Console.WriteLine("Member not found");
                }
                else
                {
                    Console.WriteLine($"Id: {member.MemberId} | Name: {member.Name} | Active: {member.IsActive}");
                    string question = member.IsActive ? "Deactivate" : "Activate";
                    Console.WriteLine($"Need to {question}? yes/no ");
                    string userInput = Console.ReadLine()??"";
                    switch (userInput.ToLower())
                    {
                        case "yes":
                            member.IsActive = !member.IsActive;
                            memberService.UpdateMember(member);
                            Console.WriteLine($"Member status updated to Active: {member.IsActive}");
                            break;
                        case "no":
                            Console.WriteLine("No changes");
                            return;
                    }
                }
            }
        }

        private void _ShowAllMembers()
        {
            Console.WriteLine("\n====================== Members  ========================\n");
            List<Member>? allMembers = memberService.GetAllActiveMembers();
            if(allMembers == null || allMembers.Count == 0)
            {
                Console.WriteLine("\n====================== No Members found  ========================\n");
                return;
            }
            foreach(var member in allMembers)
            {
                Console.WriteLine($"Id: {member.MemberId} | Name: {member.Name} | Email: {member.Email} | Phone: {member.Phone} | Active: {member.IsActive}");
            }
        }

        private void _handleSearchMember()
        {
            Console.Write("Search by (1.Email  2.Phone) : ");
            string choice = Console.ReadLine()??"";

            if(choice == "1")
            {
                Console.Write("Enter Email : ");
                string email = Console.ReadLine()??"";
                Member? found = memberService.GetMemberByEmail(email);
                if(found == null)
                    Console.WriteLine("Member not found");
                else
                    Console.WriteLine($"Id: {found.MemberId} | Name: {found.Name} | Email: {found.Email} | Phone: {found.Phone} | Active: {found.IsActive}");
            }
            else if(choice == "2")
            {
                Console.Write("Enter Phone : ");
                string phone = Console.ReadLine()??"";
                Member? found = memberService.GetMemberByPhone(phone);
                if(found == null)
                    Console.WriteLine("Member not found");
                else
                    Console.WriteLine($"Id: {found.MemberId} | Name: {found.Name} | Email: {found.Email} | Phone: {found.Phone} | Active: {found.IsActive}");
            }
            else
            {
                Console.WriteLine("Invalid option");
            }
        }
    }
}