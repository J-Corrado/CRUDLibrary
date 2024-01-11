using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDLibrary.Data.LIB_DB
{
    public partial class Borrower
    {
        public int BorrowerId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BookBorrower> BookBorrows { get; set; } = new List<BookBorrower>();
    }
}
