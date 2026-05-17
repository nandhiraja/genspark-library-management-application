
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Core.Models;


   
namespace LibraryManagementSystem.DAL.DBContext
{
    public class  LibraryDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=LibraryManagementDB;Username=nandhiraja;Password=");
            }
        }
       
        public DbSet<Member> Members { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookTitle> BookTitles { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<BorrowTransaction> BorrowTransactions { get; set; }
        public DbSet<Fine> Fines { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .HasOne(m => m.MembershipType)
                .WithMany(mt => mt.Members)
                .HasForeignKey(m => m.MembershipTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.Email)
                .IsUnique();

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.Phone)
                .IsUnique();

            modelBuilder.Entity<Member>()
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

        modelBuilder.Entity<BookTitle>()
            .HasOne(bt => bt.Category)
            .WithMany(c => c.BookTitles)
            .HasForeignKey(bt => bt.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BookCopy>()
            .HasOne(bc => bc.BookTitle)
            .WithMany(bt => bt.BookCopies)
            .HasForeignKey(bc => bc.BookTitleId);

        modelBuilder.Entity<BookCopy>()
            .HasIndex(bc => bc.CopyCode)
            .IsUnique();

        modelBuilder.Entity<BorrowTransaction>()
            .HasOne(bt => bt.Member)
            .WithMany(m => m.BorrowTransactions)
            .HasForeignKey(bt => bt.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BorrowTransaction>()
            .HasKey(bt=>bt.BorrowId);
       modelBuilder.Entity<BorrowTransaction>()
            .HasOne(bt => bt.BookCopy)
            .WithMany(bc => bc.BorrowTransactions)
            .HasForeignKey(bt => bt.BookCopyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Fine>()
             .HasOne(f => f.BorrowTransaction)
             .WithOne(bt => bt.Fine)
             .HasForeignKey<Fine>(f => f.BorrowId);

        modelBuilder.Entity<Fine>()
            .Property(f => f.Amount)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<BookCopy>()
            .Property(bc => bc.Status)
            .HasConversion<string>();


        modelBuilder.Entity<BorrowTransaction>()
            .Property(bt => bt.Status)
            .HasConversion<string>();
        
        }
    }
}
