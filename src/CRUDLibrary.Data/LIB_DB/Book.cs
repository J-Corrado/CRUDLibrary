using CRUDLibrary.Data.LIB_DB.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDLibrary.Data.LIB_DB
{
    public partial class Book
    {
        public decimal BookId { get; set; }
        public string? Title { get; set; }
        public BookGenre Genre { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }

        public virtual ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
        public virtual ICollection<BookBorrower> BookBorrows { get; set; } = new List<BookBorrower>();
    }
}
