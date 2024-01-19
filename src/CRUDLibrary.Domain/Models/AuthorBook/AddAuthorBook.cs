using System.ComponentModel.DataAnnotations;
using CRUDLibrary.Data.LIB_DB.Enum;
using CRUDLibrary.Domain.Services;

namespace CRUDLibrary.Domain.Models;

public class AddAuthorBookRequest : RequestModel
{

    public string AUTHOR_ID { get; set; } = string.Empty;
    public string? AUTHOR_NAME { get; set; } = string.Empty;
    public string? AUTHOR_BORN { get; set; } = string.Empty;
    public string? AUTHOR_DIED { get; set; } = string.Empty;

    public string? BOOK_ID { get; set; } = string.Empty;

    //If adding a new book
    public string BOOK_TITLE { get; set; } = string.Empty;
    public string? BOOK_PUB_DATE { get; set; } = string.Empty;
    public GenreDto? BOOK_GENRE { get; set; }
}
public class AddAuthorBookResponse : ResponseModel
{
    public int AUTHOR_BOOK_ID { get; set; }

    public int AUTHOR_ID { get; set; }
    public string? AUTHOR_NAME { get; set; } = string.Empty;
    public string? AUTHOR_BORN { get; set; } = string.Empty;
    public string? AUTHOR_DIED { get; set; } = string.Empty;

    public int BOOK_ID { get; set; }
    public string BOOK_TITLE { get; set; } = string.Empty;
    public string? BOOK_PUB_DATE { get; set; } = string.Empty;
    public GenreDto? BOOK_GENRE { get; set; }

}

public class AddAuthorBookSubmitRequest : RequestModel
{
    public string AUTHOR_ID { get; set; } = string.Empty;


    [CustomValidation(typeof(Validation), "NAME", ErrorMessage = "Author Name#Invalid#")]
    public string AUTHOR_NAME { get; set; } = string.Empty;
    
    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Author's Date of Birth#Invalid#")]
    public string? AUTHOR_BORN { get; set; } = string.Empty;

    public string? AUTHOR_DIED { get; set; } = string.Empty;


    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "Book ID#Invalid#")]
    public string BOOK_ID { get; set; } = string.Empty;

    [CustomValidation(typeof(Validation), "NAME", ErrorMessage = "Book Title#Invalid#")]
    public string? BOOK_TITLE { get; set; } = string.Empty;

    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Book Publication Date#Invalid#")]
    public string? BOOK_PUB_DATE { get; set; } = string.Empty;

    [CustomValidation(typeof(Validation), "GENRE", ErrorMessage = "Book Genre#Invalid#")]
    public string? BOOK_GENRE { get; set; } = string.Empty;

}

public class AddAuthorBookSubmitResponse : ResponseModel
{
    public int AUTHOR_ID { get; set; }
    public int BOOK_ID { get; set; }
}