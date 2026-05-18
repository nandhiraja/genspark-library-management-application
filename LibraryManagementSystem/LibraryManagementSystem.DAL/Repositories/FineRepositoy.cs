using System.Net.NetworkInformation;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;
using LibraryManagementSystem.DAL.Interface;
using Microsoft.EntityFrameworkCore;

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
            return _context.Fines
                    .Include(f => f.BorrowTransaction)
                    .ThenInclude(bt => bt.Member)
                    .Where(f => f.BorrowTransaction.MemberId == memberId)
                    .ToList();
        }

        public List<Fine> GetPendingFines(int memberId)
        {
            return _context.Fines
                    .Include(f => f.BorrowTransaction)
                    .ThenInclude(bt => bt.Member)
                    .Where(f => f.BorrowTransaction.MemberId == memberId && f.IsPaid == false)
                    .ToList();
        }

        public decimal GetTotalUnpaidFine(int memberId)
        {
            var result = _context.Database.SqlQuery<decimal>($"SELECT calculate_member_fine({memberId})").FirstOrDefault();
            return result;
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

        public List<Fine> GetAllFines()
        {
            return _context.Fines
                    .Include(f => f.BorrowTransaction)
                    .ThenInclude(bt => bt.Member)
                    .ToList();
        }
    } 

}