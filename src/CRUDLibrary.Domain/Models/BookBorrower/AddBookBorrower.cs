using System.ComponentModel.DataAnnotations;
using CRUDLibrary.Data.LIB_DB.Enum;
using CRUDLibrary.Domain.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CRUDLibrary.Domain.Models;

public class AddBookBorrowerRequest : RequestModel
{
    //If using existing book
    public decimal? BOOK_ID { get; set; }

    //If adding a new book
    public string BOOK_TITLE { get; set; } = string.Empty;
    public DateTime? BOOK_PUB_DATE { get; set; }
    public BookGenre? BOOK_GENRE { get; set; }

    //If using existing borrower
    public decimal? BORROWER_ID { get; set; }

    //If adding a new borrower
    public string BORROWER_NAME { get; set; } = string.Empty;
}

public class AddBookBorrowerResponse : ResponseModel
{
    public decimal BOOK_BORROWER_ID { get; set; }
    public decimal BOOK_ID { get; set; }
    public string BOOK_TITLE { get; set; } = string.Empty;
    public DateTime? BOOK_PUB_DATE { get; set; }
    public BookGenre? BOOK_GENRE { get; set; }
    
    public decimal BORROWER_ID { get; set; }
    public string BORROWER_NAME { get; set; } = string.Empty;
    
    public DateTime? BORROWED_DATE { get; set; }
    
}

public class AddBookBorrowerSubmitRequest : RequestModel
{
    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "Borrower with given BORROWER_ID#Invalid#")]
    public decimal BORROWER_ID { get; set; }
    
    [CustomValidation(typeof(Validation), "NAME", ErrorMessage = "Borrower Name#Invalid#")]
    public string BORROWER_NAME { get; set; } = string.Empty;

    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "Book with given BOOK_ID#Invalid#")]
    public decimal BOOK_ID { get; set; }

    [CustomValidation(typeof(Validation), "TITLE", ErrorMessage = "Book Title#Invalid#")]
    public string BOOK_TITLE { get; set; } = string.Empty;
    
    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Book Publication Date#Invalid#")]
    public DateTime? BOOK_PUB_DATE { get; set; }
    
    [CustomValidation(typeof(Validation), "DROP_DOWN_REQ", ErrorMessage = "Book Genre#Invalid#")]
    public BookGenre? BOOK_GENRE { get; set; }
    
    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Book Borrowed Date#Invalid#")]
    public DateTime? BORROWED_DATE { get; set; }
}

public class AddBookBorrowerSubmitResponse : ResponseModel
{
    public decimal ID { get; set; }
    public string Message { get; set; } = string.Empty;
}