using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.Arm;
using System.Text.RegularExpressions;
using LibraryManagementSystem.BLL.Interfaces;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;
using LibraryManagementSystem.DAL.Interface;
using LibraryManagementSystem.DAL.Repositories;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace LibraryManagementSystem.BLL.Services
{
    public class MemberService:IMemberService
    {
        private MemeberRepository _memberRepository;
        private readonly LibraryDbContext _context;
        public MemberService()
        {
            _context = new LibraryDbContext();
            _memberRepository = new MemeberRepository(_context);   
        }

        public bool AddMember(Member member)
        {   try{
                member.IsActive = true;
                _memberRepository.AddMember(member);
                // incase no error happen at this stage
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to register new user" ,ex);
                return false;
            }

        }

        public Member? GetMemberByEmail (string email)
        {
            if(_validateEmail(email)){
                return _memberRepository.GetByEmail(email);
            }
            throw new Exception("Invalid Email format");
        }

        public Member? GetMemberByPhone(string phone)
        {
            return _memberRepository.GetAllMembers().FirstOrDefault(m => m.Phone == phone);
        }

        public Member? GetMemberById(int memberId)
        {
            return _memberRepository.GetById(memberId);
        }

        public List<Member>? GetAllMembers()
        {
            return _memberRepository.GetAllMembers();
        }

        public List<Member>? GetAllMembersByMemberShip(MembershipType membershipType)
        {
            return _memberRepository.GetAllMembers().Where(m=>m.MembershipType == membershipType).ToList();
        }

        public List<Member>? GetAllActiveMembers()
        {
            return _memberRepository.GetAllMembers().Where(m=>m.IsActive==true).ToList();
        }

        public bool UpdateMember(Member member)
        {
    
            return _memberRepository.UpdateMember(member);

        }

        public bool DeleteMember(Member member)
        {
            throw new NotImplementedException();
        }


         public bool _validateEmail(string email)
        {
            string pattern=@"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email,pattern);
        }
    }
}