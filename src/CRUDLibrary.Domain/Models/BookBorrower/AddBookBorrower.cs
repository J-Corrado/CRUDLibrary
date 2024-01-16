using System.ComponentModel.DataAnnotations;
using CRUDLibrary.Data.LIB_DB.Enum;
using CRUDLibrary.Domain.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CRUDLibrary.Domain.Models;

public class AddBookBorrowerRequest : RequestModel
{
    //If using existing book
    public string? BOOK_ID { get; set; } = string.Empty;

    //If adding a new book
    public string BOOK_TITLE { get; set; } = string.Empty;
    public string? BOOK_PUB_DATE { get; set; } = string.Empty;
    public GenreDto? BOOK_GENRE { get; set; }

    //If using existing borrower
    public string? BORROWER_ID { get; set; } = string.Empty;

    //If adding a new borrower
    public string BORROWER_NAME { get; set; } = string.Empty;
}

public class AddBookBorrowerResponse : ResponseModel
{
    public string BOOK_BORROWER_ID { get; set; }
    
    public int BOOK_ID { get; set; }
    public string? BOOK_TITLE { get; set; } = string.Empty;
    public string? BOOK_PUB_DATE { get; set; } = string.Empty;
    public GenreDto? BOOK_GENRE { get; set; }

    public int BORROWER_ID { get; set; }
    public string? BORROWER_NAME { get; set; } = string.Empty;

    public string? BORROWED_DATE { get; set; } = string.Empty;

}

public class AddBookBorrowerSubmitRequest : RequestModel
{
    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "Borrower with given BORROWER_ID#Invalid#")]
    public string BORROWER_ID { get; set; } = string.Empty;
    
    [CustomValidation(typeof(Validation), "NAME", ErrorMessage = "Borrower Name#Invalid#")]
    public string BORROWER_NAME { get; set; } = string.Empty;

    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "Book with given BOOK_ID#Invalid#")]
    public string BOOK_ID { get; set; } = string.Empty;

    [CustomValidation(typeof(Validation), "TITLE", ErrorMessage = "Book Title#Invalid#")]
    public string BOOK_TITLE { get; set; } = string.Empty;

    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Book Publication Date#Invalid#")]
    public string? BOOK_PUB_DATE { get; set; } = string.Empty;
    
    public string? BOOK_GENRE { get; set; } = string.Empty;

    public string? BORROWED_DATE { get; set; } = string.Empty;
}

public class AddBookBorrowerSubmitResponse : ResponseModel
{
    public int? BOOK_ID { get; set; }
    public int? BORROWER_ID { get; set; }
    public string Message { get; set; } = string.Empty;
}