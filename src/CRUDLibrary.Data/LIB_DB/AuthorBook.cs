namespace CRUDLibrary.Data.LIB_DB
{
    public partial class AuthorBook
    {
        public decimal AuthorBookId { get; set; }
        public decimal AuthorId { get; set; }
        public virtual Author? Author { get; set; }

        public decimal BookId { get; set; }
        public virtual Book? Book { get; set; }
    }
}
