using Microsoft.EntityFrameworkCore;

namespace CRUDLibrary.Data.LIB_DB;

using Enum;

public partial class LIBDBContext : DbContext
    {

        public LIBDBContext(DbContextOptions<LIBDBContext> options ): base(options) { }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Borrower> Borrowers { get; set; }

        public virtual DbSet<AuthorBook> AuthorBooks { get; set; }
        public virtual DbSet<BookBorrower> BookBorrows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AuthorBook>()
                .HasKey(ab => new {ab.AuthorBookId});
            modelBuilder.Entity<AuthorBook>()
                .HasOne(a => a.Author)
                .WithMany(ab => ab.AuthorBooks)
                .HasForeignKey(a => a.AuthorId);
            modelBuilder.Entity<AuthorBook>()
                .HasOne(b => b.Book)
                .WithMany(ab => ab.AuthorBooks)
                .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<BookBorrower>()
                .HasKey(bb => new { bb.BookBorrowId});
            modelBuilder.Entity<BookBorrower>()
                .HasOne(b => b.Book)
                .WithMany(bb => bb.BookBorrows)
                .HasForeignKey(b => b.BookId);
            modelBuilder.Entity<BookBorrower>()
                .HasOne(b => b.Borrower)
                .WithMany(bb => bb.BookBorrows)
                .HasForeignKey(b => b.BorrowerId);
        }

    }
