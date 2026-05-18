using LibraryManagementSystem.DAL.Repositories;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;
using LibraryManagementSystem.DAL.Interface;
using LibraryManagementSystem.BLL.Interfaces;

namespace LibraryManagementSystem.BLL.Services
{
    public class FineService:IFineService
    {
        private FineRepository _fineRepository;
        private LibraryDbContext _context;
        public FineService()
        {
            _context = new LibraryDbContext();
            _fineRepository = new FineRepository(_context);   
        }


        public decimal GetUnPaidFineTotalAmount(Member member)
        {
            return _fineRepository.GetTotalUnpaidFine(member.MemberId);
        }
        public List<Fine> GetUnpaidFines(Member member)
        {
            return _fineRepository.GetPendingFines(member.MemberId);
        }

        public bool UpdateFine(Fine fine)
        {
            try { 
                _fineRepository.Update(fine);
                return true;
            }
            catch(Exception)
            {
                throw new Exception("Unable to update the fine");
            }
        }
        public Fine? GetFine(int fineId)
        {
            return _fineRepository.GetById(fineId);
        }

        public bool AddNewFine(Fine fine)
        {
            try{ 
                _fineRepository.Add(fine);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Uable to create New Fine",ex);
                return false;
            }
        }

        public decimal GetTotalFineAmountById(int memberId)
        {
            decimal totalFine =0;
            totalFine += _fineRepository.GetTotalUnpaidFine(memberId);
            return totalFine;
        }

        public List<Fine> GetAllUnPaidFinesListById(int memberId)
        {
            return _fineRepository.GetPendingFines(memberId);
        }

        public List<Fine> GetAllUnPaidFines()
        {
            return _fineRepository.GetAllFines().Where(f=>f.IsPaid==false).ToList();
        }

        public List<Fine> GetAllFines()
        {
             return _fineRepository.GetAllFines();
        }
    }
}