using System.ComponentModel.DataAnnotations;
using CRUDLibrary.Data.LIB_DB.Enum;
using CRUDLibrary.Domain.Services;

namespace CRUDLibrary.Domain.Models;

public class UpdateBookRequest : RequestModel
{
    public decimal BOOK_ID { get; set; }
}

public class UpdateBookResponse : ResponseModel
{
    public decimal BOOK_ID { get; set; }

    public string BOOK_TITLE { get; set; } = string.Empty;
    public DateTime? BOOK_PUB_DATE { get; set; }
    public BookGenre? BOOK_GENRE { get; set; }

    public IList<AuthorDto> BOOK_AUTHORS { get; set; } = new List<AuthorDto>();
}

public class UpdateBookSubmitRequest : RequestModel
{
    [Required(ErrorMessage = "Book ID#Required#")]
    [CustomValidation(typeof(Validation), "PRIMARY_ID", ErrorMessage = "Book ID#Invalid#")]
    public decimal BOOK_ID { get; set; }
    
    [Required(ErrorMessage = "Book Title#Required#")]
    [CustomValidation(typeof(Validation), "TITLE", ErrorMessage = "Book Title#Invalid#")]
    public string BOOK_TITLE { get; set; } = string.Empty;
    
    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Book Publication Date#Invalid#")]
    public DateTime? BOOK_PUB_DATE { get; set; }
    
    [CustomValidation(typeof(Validation), "DROP_DOWN_REQ", ErrorMessage = "Book Genre#Invalid#")]
    public BookGenre? BOOK_GENRE { get; set; }
}

public class UpdateBookSubmitResponse : ResponseModel
{
    public decimal ID { get; set; }
    public string Message { get; set; } = string.Empty;
}