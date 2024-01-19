using System.ComponentModel.DataAnnotations;
using CRUDLibrary.Data.LIB_DB.Enum;
using CRUDLibrary.Domain.Services;

namespace CRUDLibrary.Domain.Models;

public class AddBookRequest : RequestModel
{
    public int? AUTHOR_ID { get; set; }
    public string? AUTHOR_NAME { get; set; }
    public string? AUTHOR_BORN { get; set; } = string.Empty;
    public string? AUTHOR_DIED { get; set; } = string.Empty;
}

public class AddBookResponse : ResponseModel
{
    public int? AUTHOR_BOOK_ID { get; set; }
    
    public int AUTHOR_ID { get; set; }
    public string AUTHOR_NAME { get; set; } = string.Empty;
    public string? AUTHOR_BORN { get; set; } = string.Empty;
    public string? AUTHOR_DIED { get; set; } = string.Empty;

    public int BOOK_ID { get; set; }
    public string BOOK_TITLE { get; set; } = string.Empty;
    public string? BOOK_PUB_DATE { get; set; } = string.Empty;
    public GenreDto BOOK_GENRE { get; set; }
}

public class AddBookSubmitRequest : RequestModel
{
    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "Book ID#Invalid#")]
    public string BOOK_ID { get; set; } = string.Empty;
    
    
    [Required(ErrorMessage = "Book Title#Required#")]
    [CustomValidation(typeof(Validation), "TITLE", ErrorMessage = "Book Title#Invalid#")]
    public string? BOOK_TITLE { get; set; } = string.Empty;

    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Book Publication Date#Invalid#")]
    public string? BOOK_PUB_DATE { get; set; } = string.Empty;

    [Required(ErrorMessage = "Book Genre#Required#")]
    [CustomValidation(typeof(Validation), "GENRE", ErrorMessage = "Book Genre#Invalid#")]
    public string BOOK_GENRE { get; set; } = string.Empty;

    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "Author ID#Invalid#")]
    public string? AUTHOR_ID { get; set; } = string.Empty;
    
    [CustomValidation(typeof(Validation), "NAME", ErrorMessage = "Author Name#Invalid#")]
    public string AUTHOR_NAME { get; set; } = string.Empty;


}

public class AddBookSubmitResponse : ResponseModel
{
    public int ID { get; set; }
    public string Message { get; set; } = string.Empty;
}