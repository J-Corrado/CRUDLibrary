using CRUDLibrary.Data.LIB_DB.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDLibrary.Data.LIB_DB
{
    public partial class Book
    {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public BookGenre Genre { get; set; }
        
        public string PublicationDate { get; set; }

        public virtual ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
        public virtual ICollection<BookBorrower> BookBorrows { get; set; } = new List<BookBorrower>();
    }
}
