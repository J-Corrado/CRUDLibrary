using System.ComponentModel.DataAnnotations;
using CRUDLibrary.Domain.Services;
using CRUDLibrary.Data.LIB_DB.Enum;

namespace CRUDLibrary.Domain.Models;

public class AddBorrowerRequest : RequestModel
{
    
}

public class AddBorrowerResponse : ResponseModel
{
    public string BORROWER_ID { get; set; } = string.Empty;
    public string BORROWER_NAME { get; set; } = string.Empty;
}

public class AddBorrowerSubmitRequest : RequestModel
{
    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "Borrower ID#Invalid#")]
    public string BORROWER_ID { get; set; } = string.Empty;

    [Required(ErrorMessage = "Borrower Name#Required#")]
    [CustomValidation(typeof(Validation), "NAME", ErrorMessage = "Borrower Name#Invalid#")]
    public string BORROWER_NAME { get; set; } = string.Empty;
    
}

public class AddBorrowerSubmitResponse : ResponseModel
{
    public int ID { get; set; }
    public string Message { get; set; } = string.Empty;
}