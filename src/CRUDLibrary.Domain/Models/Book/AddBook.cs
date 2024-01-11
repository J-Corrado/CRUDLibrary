using System.ComponentModel.DataAnnotations;
using CRUDLibrary.Data.LIB_DB.Enum;
using CRUDLibrary.Domain.Services;

namespace CRUDLibrary.Domain.Models;

public class AddBookRequest : RequestModel
{
    public decimal? AUTHOR_ID { get; set; }
    public string? AUTHOR_NAME { get; set; }
    public DateTime? AUTHOR_BORN { get; set; }
    public DateTime? AUTHOR_DIED { get; set; }
}

public class AddBookResponse : ResponseModel
{
    public decimal? AUTHOR_BOOK_ID { get; set; }
    
    public decimal AUTHOR_ID { get; set; }
    public string AUTHOR_NAME { get; set; } = string.Empty;
    public DateTime? AUTHOR_BORN { get; set; }
    public DateTime? AUTHOR_DIED { get; set; }

    public decimal BOOK_ID { get; set; }
    public string BOOK_TITLE { get; set; } = string.Empty;
    public DateTime? BOOK_PUB_DATE { get; set; }
    public BookGenre BOOK_GENRE { get; set; }
}

public class AddBookSubmitRequest : RequestModel
{
    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "Book ID#Invalid#")]
    public decimal BOOK_ID { get; set; }
    
    
    [Required(ErrorMessage = "Book Title#Required#")]
    [CustomValidation(typeof(Validation), "TITLE", ErrorMessage = "Book Title#Invalid#")]
    public string BOOK_TITLE { get; set; } = string.Empty;
    
    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Book Publication Date#Invalid#")]
    public DateTime? BOOK_PUB_DATE { get; set; }

    [Required(ErrorMessage = "Book Genre#Required#")]
    [CustomValidation(typeof(Validation), "DROP_DOWN_REQ", ErrorMessage = "Book Genre#Invalid#")]
    public BookGenre BOOK_GENRE { get; set; }

    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "Author ID#Invalid#")]
    public decimal AUTHOR_ID { get; set; }
    
    [Required(ErrorMessage = "Author Name#Required#")]
    [CustomValidation(typeof(Validation), "NAME", ErrorMessage = "Author Name#Invalid#")]
    public string AUTHOR_NAME { get; set; } = string.Empty;
    
    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Author Date of Birth#Invalid#")]
    public DateTime? AUTHOR_BORN { get; set; } 
    
    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Author Date of Death#Invalid#")]
    public DateTime? AUTHOR_DIED { get; set; }
    
}

public class AddBookSubmitResponse : ResponseModel
{
    public decimal ID { get; set; }
    public string Message { get; set; } = string.Empty;
}