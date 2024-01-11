using System.Security.AccessControl;
using CRUDLibrary.Data.LIB_DB.Enum;

namespace CRUDLibrary.Domain.Models;

public class AuthorDto
{
    public string NAME { get; set; } = string.Empty;
    public decimal ID { get; set; }
    public DateTime? BORN { get; set; }
    public DateTime? DIED { get; set; }
}

public class BookDto
{
    public string TITLE { get; set; } = string.Empty;
    public decimal ID { get; set; }
    public DateTime? PUB_DATE { get; set; }
    public BookGenre GENRE { get; set; }
}

public class BorrowerDto
{
    public string NAME { get; set; } = string.Empty;
    public decimal ID { get; set; }
}

public class AuthorBookDto
{
    public decimal ID { get; set; }
    public decimal AUTHOR_ID { get; set; }
    public string AUTHOR_NAME { get; set; } = string.Empty;
    public DateTime AUTHOR_BORN { get; set; }
    public DateTime AUTHOR_DIED { get; set; }
    
    public decimal BOOK_ID { get; set; }
    public string BOOK_TITLE { get; set; } = string.Empty;
    public DateTime BOOK_PUB_DATE { get; set; }
    public BookGenre BOOK_GENRE { get; set; }
}
public class BookBorrowerDto
{
    public decimal ID { get; set; }
    public decimal BORROWER_ID { get; set; }
    public string BORROWER_NAME { get; set; } = string.Empty;
    
    public decimal BOOK_ID { get; set; }
    public string BOOK_TITLE { get; set; } = string.Empty;
    public DateTime? BOOK_PUB_DATE { get; set; }
    public BookGenre BOOK_GENRE { get; set; }


    public DateTime? BORROWED_DATE { get; set; }
    public DateTime? RETURNED_DATE { get; set; }
}

public class AutoCompleteItem
{
    public string value { get; set; } = string.Empty;
    public string label { get; set; } = string.Empty;
}