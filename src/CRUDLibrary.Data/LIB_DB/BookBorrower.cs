using System.ComponentModel.DataAnnotations;

namespace CRUDLibrary.Data.LIB_DB
{
    public partial class BookBorrower
    {
        public decimal BookBorrowId { get; set; }
        public decimal BookId { get; set; }
        public virtual Book? Book { get; set; }

        public decimal BorrowerId { get; set; }
        public virtual Borrower? Borrower { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BorrowedDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReturnedDate { get; set; }
        public bool IsReturned { get; set; } = true;
    }
}
