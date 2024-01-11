using System.ComponentModel.DataAnnotations;
using CRUDLibrary.Domain.Services;

namespace CRUDLibrary.Domain.Models;

public class UpdateBookBorrowerRequest : RequestModel
{
    public decimal BOOK_BORROWER_ID { get; set; }
    public decimal BOOK_ID { get; set; }
    public decimal BORROWER_ID { get; set; }
}

public class UpdateBookBorrowerResponse : ResponseModel
{
    public decimal BOOK_BORROWER_ID { get; set; }
    public decimal BOOK_ID { get; set; }
    public string BOOK_TITLE { get; set; } = string.Empty;
    public decimal BORROWER_ID { get; set; }
    public string BORROWER_NAME { get; set; } = string.Empty;
    public DateTime? BORROWED_DATE { get; set; }
    public DateTime? RETURNED_DATE { get; set; }
}

public class UpdateBookBorrowerSubmitRequest : RequestModel
{
    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "BorrowedBook with given BOOK_BORROWER_ID#Invalid#")]
    public decimal BOOK_BORROWER_ID { get; set; }
    
    public DateTime? RETURNED_DATE { get; set; }
}

public class UpdateBookBorrowerSubmitResponse : ResponseModel
{
    public decimal ID { get; set; }
}