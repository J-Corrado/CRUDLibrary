using Microsoft.EntityFrameworkCore;

namespace CRUDLibrary.Data.LIB_DB
{
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

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.AuthorId).HasName("XPKAUTHOR");

                entity.ToTable("AUTHOR");

                entity.Property(e => e.AuthorId).HasColumnName("AUTHOR_ID");
                entity.Property(e => e.Name)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("AUTHOR_NAME");
                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("datetime")
                    .HasColumnName("AUTHOR_BORN");
                entity.Property(e => e.DateOfDeath)
                    .HasColumnType("datetime")
                    .HasColumnName("AUTHOR_DIED");
                
                entity.HasMany(e => e.AuthorBooks)
                    .WithOne(ab => ab.Author)
                    .HasForeignKey(ab => ab.AuthorId);
            });
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BookId).HasName("XPKBOOK");
                entity.ToTable("BOOK");

                entity.Property(e => e.BookId).HasColumnName("BOOK_ID");
                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BOOK_TITLE");
                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasColumnName("BOOK_GENRE");
                entity.Property(e => e.PublicationDate)
                    .HasColumnType("date")
                    .HasColumnName("PUBLICATION_DATE");

                entity.HasMany(e => e.AuthorBooks)
                    .WithOne(ab => ab.Book)
                    .HasForeignKey(ab => ab.BookId);
                entity.HasMany(e => e.BookBorrows)
                    .WithOne(bb => bb.Book)
                    .HasForeignKey(bb => bb.BookId);
            });
            modelBuilder.Entity<Borrower>(entity =>
            {
                entity.HasKey(e => e.BorrowerId).HasName("XPKBORROWER");
                entity.ToTable("BORROWER");
                
                entity.Property(e => e.BorrowerId).HasColumnName("BORROWER_ID");
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BORROWER_NAME");
                
                entity.HasMany(e => e.BookBorrows)
                    .WithOne(bb => bb.Borrower)
                    .HasForeignKey(bb => bb.BorrowerId);
            });

            modelBuilder.Entity<AuthorBook>(entity =>
            {
                entity.HasKey(e => e.AuthorBookId).HasName("XPKAUTHOR_BOOK_ID");
                entity.ToTable("AUTHOR_BOOK");

                entity.Property(e => e.AuthorBookId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("AUTHOR_BOOK_ID");
                entity.Property(e => e.AuthorId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("AUTHOR_ID");
                entity.Property(e => e.BookId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BOOK_ID");
                
                entity.HasOne(e => e.Book)
                    .WithMany(ab => ab.AuthorBooks)
                    .HasForeignKey(ab => ab.BookId);
                entity.HasOne(a => a.Author)
                    .WithMany(ab => ab.AuthorBooks)
                    .HasForeignKey(a => a.AuthorId);
            });

            modelBuilder.Entity<BookBorrower>(entity =>
            {
                entity.HasKey(e => e.BookBorrowId).HasName("XPKBOOK_BORROWER");
                entity.ToTable("BOOK_BORROWER");
                
                entity.Property(e => e.BookBorrowId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BOOK_BORROWER_ID");
                entity.Property(e => e.BookId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BOOK_ID");
                entity.Property(e => e.BorrowerId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BORROWER_ID");
                entity.Property(e => e.BorrowedDate)
                    .HasColumnType("date")
                    .HasColumnName("BORROWED_DATE");
                entity.Property(e => e.ReturnedDate)
                    .HasColumnType("date")
                    .HasColumnName("RETURNED_DATE");
                entity.Property(e => e.IsReturned)
                    .HasColumnName("IS_RETURNED")
                    .HasDefaultValueSql("((1))");
                
                entity.HasOne(e => e.Book)
                    .WithMany(bb => bb.BookBorrows)
                    .HasForeignKey(e => e.BookId);
                
                entity.HasOne(e => e.Borrower)
                    .WithMany(bb => bb.BookBorrows)
                    .HasForeignKey(e => e.BorrowerId);
            });

        }
    }
}
