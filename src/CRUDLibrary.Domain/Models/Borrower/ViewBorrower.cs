namespace CRUDLibrary.Domain.Models;

public class ViewBorrowerRequest : RequestModel
{
    public int BORROWER_ID { get; set; }
}

public class ViewBorrowerResponse : ResponseModel
{
    public int BORROWER_ID { get; set; }
    public string BORROWER_NAME { get; set; }
    public IList<BookBorrowerDto> BOOK_BORROWS { get; set; } = new List<BookBorrowerDto>();
}