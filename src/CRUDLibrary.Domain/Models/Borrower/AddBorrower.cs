using System.ComponentModel.DataAnnotations;
using CRUDLibrary.Domain.Services;
using CRUDLibrary.Data.LIB_DB.Enum;

namespace CRUDLibrary.Domain.Models;

public class AddBorrowerRequest : RequestModel
{
    public decimal BOOK_ID { get; set; }
    
    public string BOOK_TITLE { get; set; } = string.Empty;
    public DateTime? BOOK_PUB_DATE { get; set; }
    public BookGenre? BOOK_GENRE { get; set; }
}

public class AddBorrowerResponse : ResponseModel
{
    public decimal BORROWER_ID { get; set; }
    public string BORROWER_NAME { get; set; } = string.Empty;

    public decimal BOOK_ID { get; set; }
    public string BOOK_TITLE { get; set; } = string.Empty;
    public DateTime? BOOK_PUB_DATE { get; set; }
    public BookGenre? BOOK_GENRE { get; set; }
}

public class AddBorrowerSubmitRequest : RequestModel
{
    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "Borrower ID#Invalid#")]
    public decimal BORROWER_ID { get; set; }

    [Required(ErrorMessage="Borrower Name#Required#")]
    [CustomValidation(typeof(Validation), "NAME", ErrorMessage = "Borrower Name#Invalid#")]
    public string BORROWER_NAME { get; set; } = string.Empty;

    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "Book ID#Invalid#")]
    public decimal BOOK_ID { get; set; }

    [CustomValidation(typeof(Validation), "TITLE", ErrorMessage = "Book Title#Invalid#")]
    public string BOOK_TITLE { get; set; } = string.Empty;
    
    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Book Publication Date#Invalid#")]
    public DateTime? BOOK_PUB_DATE { get; set; }
    
    [CustomValidation(typeof(Validation), "DROP_DOWN_REQ", ErrorMessage = "Book Genre#Invalid#")]
    public BookGenre? BOOK_GENRE { get; set; }
}

public class AddBorrowerSubmitResponse : ResponseModel
{
    public decimal ID { get; set; }
    public string Message { get; set; } = string.Empty;
}