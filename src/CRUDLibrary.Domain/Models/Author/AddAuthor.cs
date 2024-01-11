using System.ComponentModel.DataAnnotations;
using CRUDLibrary.Data.LIB_DB.Enum;
using CRUDLibrary.Domain.Services;

namespace CRUDLibrary.Domain.Models;

public class AddAuthorRequest : RequestModel
{
    //public int? AUTHOR_ID { get; set; }
    /* public string AUTHOR_NAME { get; set; } = string.Empty;
    public DateTime? AUTHOR_BORN { get; set; } 
    public DateTime? AUTHOR_DIED { get; set; } = null;
    
    public int? BOOK_ID { get; set; }
    
    public string? BOOK_TITLE { get; set; } = string.Empty;
    public DateTime? BOOK_PUB_DATE { get; set; } = null;
    public BookGenre? BOOK_GENRE { get; set; } = null; 
    */
}

public class AddAuthorResponse : ResponseModel
{
    public int AUTHOR_ID { get; set; }
    public string AUTHOR_NAME { get; set; } = string.Empty;
    public string AUTHOR_BORN { get; set; } = string.Empty;
    public string AUTHOR_DIED { get; set; } = string.Empty;

    //public int BOOK_ID { get; set; }
    //public string BOOK_TITLE { get; set; } = string.Empty;
    //public DateTime? BOOK_PUB_DATE { get; set; }
    //public BookGenre? BOOK_GENRE { get; set; }
    //public List<BookDto> AUTHORED_BOOKS { get; set; } = new List<BookDto>();
}

public class AddAuthorSubmitRequest : RequestModel
{
    //[CustomValidation(typeof(Validation), "ID", ErrorMessage = "Author ID#Invalid#")]
    //public int? AUTHOR_ID { get; set; }
    
    
    [Required(ErrorMessage = "Author Name#Required#")]
    public string AUTHOR_NAME { get; set; }

    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Author Date of Birth#Invalid#")]
    public string AUTHOR_BORN { get; set; } = string.Empty;

    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Author Date of Death#Invalid#")]
    public string AUTHOR_DIED { get; set; } = string.Empty;
/*
    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "Book ID#Invalid#")]
    public int? BOOK_ID { get; set; } = null;
    
    [CustomValidation(typeof(Validation), "TITLE", ErrorMessage = "Book Title#Invalid#")]
    public string? BOOK_TITLE { get; set; } = string.Empty;
    
    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Book Publication Date#Invalid#")]
    public DateTime? BOOK_PUB_DATE { get; set; } = null;

    [CustomValidation(typeof(Validation), "DROP_DOWN_REQ", ErrorMessage = "Book Genre#Invalid#")]
    public BookGenre? BOOK_GENRE { get; set; } = null;

    public List<BookDto> AUTHORED_BOOKS { get; set; } = new List<BookDto>();
    */
}

public class AddAuthorSubmitResponse : ResponseModel
{
    public int ID { get; set; }
}