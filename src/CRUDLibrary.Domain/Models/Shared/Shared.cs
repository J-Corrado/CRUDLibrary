using System.Security.AccessControl;
using CRUDLibrary.Data.LIB_DB.Enum;

namespace CRUDLibrary.Domain.Models;

public class AuthorDto
{
    public string NAME { get; set; } = string.Empty;
    public int ID { get; set; }
    public string? BORN { get; set; } = string.Empty;
    public string? DIED { get; set; } = string.Empty;
}

public class BookDto
{
    public string TITLE { get; set; } = string.Empty;
    public int ID { get; set; }
    public string? PUB_DATE { get; set; } = string.Empty;
    public BookGenre GENRE { get; set; }
}

public class BorrowerDto
{
    public string NAME { get; set; } = string.Empty;
    public int ID { get; set; }
}

public class AuthorBookDto
{
    public int ID { get; set; }
    public int AUTHOR_ID { get; set; }
    public string AUTHOR_NAME { get; set; } = string.Empty;
    public string AUTHOR_BORN { get; set; } = string.Empty;
    public string AUTHOR_DIED { get; set; } = string.Empty;
    
    public int BOOK_ID { get; set; }
    public string BOOK_TITLE { get; set; } = string.Empty;
    public string BOOK_PUB_DATE { get; set; } = string.Empty;
    public BookGenre BOOK_GENRE { get; set; }
}
public class BookBorrowerDto
{
    public int ID { get; set; }
    public int BORROWER_ID { get; set; }
    public string BORROWER_NAME { get; set; } = string.Empty;
    
    public int BOOK_ID { get; set; }
    public string BOOK_TITLE { get; set; } = string.Empty;
    public string BOOK_PUB_DATE { get; set; } = string.Empty;
    public string BOOK_GENRE { get; set; } = string.Empty;


    public string BORROWED_DATE { get; set; } = string.Empty;
    public string RETURNED_DATE { get; set; } = string.Empty;
}

public class GenreDto
{
    public string NAME { get; set; } = string.Empty;
    public int ID { get; set; }
}
public class AutoCompleteItem
{
    public string value { get; set; } = string.Empty;
    public string label { get; set; } = string.Empty;
}