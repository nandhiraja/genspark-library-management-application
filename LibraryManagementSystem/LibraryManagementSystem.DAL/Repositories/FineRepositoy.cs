using System.Net.NetworkInformation;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;
using LibraryManagementSystem.DAL.Interface;

namespace LibraryManagementSystem.DAL.Repositories
{
    public class FineRepository : IFineRepository
    {   
        readonly LibraryDbContext _context;
        public FineRepository(LibraryDbContext libraryDbContext)
        {
            _context = libraryDbContext;
        }


        public void Add(Fine fine)
        {
            _context.Fines.Add(fine);
            _context.SaveChanges();
        }

        public Fine? GetById(int fineId)
        {
            return _context.Fines.Find(fineId);
        }

        public List<Fine> GetFineHistory(int memberId)
        {
            return _context.Fines.Where(f=>f.BorrowTransaction.MemberId==memberId).ToList();
        }

        public List<Fine> GetPendingFines(int memberId)
        {
            return _context.Fines.Where(f=>f.BorrowTransaction.MemberId==memberId && f.IsPaid == false).ToList();
        }

        public decimal GetTotalUnpaidFine(int memberId)
        {
            return _context.Fines.Where(f=>f.BorrowTransaction.MemberId==memberId && f.IsPaid == false).Sum(f=>f.Amount);
        }

        public void Update(Fine fine)
        {
            Fine? oldFine =  _context.Fines.Find(fine.FineId);
            if (oldFine == null)
            {
                throw new Exception("Fine not found");
            }
            oldFine.IsPaid= fine.IsPaid;
            _context.SaveChanges();


        }
    }

}