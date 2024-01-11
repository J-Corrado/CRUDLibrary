using System.ComponentModel.DataAnnotations;

namespace CRUDLibrary.Data.LIB_DB
{
    public partial class BookBorrower
    {
        public int BookBorrowId { get; set; }
        
        public int BookId { get; set; }
        public virtual Book? Book { get; set; }

        public int BorrowerId { get; set; }
        public virtual Borrower? Borrower { get; set; }
        
        public string BorrowedDate { get; set; }
        public string? ReturnedDate { get; set; }
        public bool IsReturned { get; set; } = true;
    }
}
