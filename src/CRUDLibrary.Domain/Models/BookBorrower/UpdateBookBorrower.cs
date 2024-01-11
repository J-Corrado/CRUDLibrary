using System.ComponentModel.DataAnnotations;
using CRUDLibrary.Domain.Services;

namespace CRUDLibrary.Domain.Models;

public class UpdateBookBorrowerRequest : RequestModel
{
    public int BOOK_BORROWER_ID { get; set; }
    public int BOOK_ID { get; set; }
    public int BORROWER_ID { get; set; }
}

public class UpdateBookBorrowerResponse : ResponseModel
{
    public int BOOK_BORROWER_ID { get; set; }
    public int BOOK_ID { get; set; }
    public string BOOK_TITLE { get; set; } = string.Empty;
    public int BORROWER_ID { get; set; }
    public string BORROWER_NAME { get; set; } = string.Empty;
    public string? BORROWED_DATE { get; set; } = string.Empty;
    public string? RETURNED_DATE { get; set; } = string.Empty;
}

public class UpdateBookBorrowerSubmitRequest : RequestModel
{
    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "BorrowedBook with given BOOK_BORROWER_ID#Invalid#")]
    public int BOOK_BORROWER_ID { get; set; }

    public string? RETURNED_DATE { get; set; } = string.Empty;
}

public class UpdateBookBorrowerSubmitResponse : ResponseModel
{
    public int ID { get; set; }
}