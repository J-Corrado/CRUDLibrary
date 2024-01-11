using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using CRUDLibrary.Domain.Services;

namespace CRUDLibrary.Domain.Models;

public class UpdateBorrowerRequest : RequestModel
{
    public int BORROWER_ID { get; set; }
}

public class UpdateBorrowerResponse : ResponseModel
{
    public int BORROWER_ID { get; set; }
    public string BORROWER_NAME { get; set; } = string.Empty;
}

public class UpdateBorrowerSubmitRequest : RequestModel
{
    [Required(ErrorMessage = "Borrower ID#Required#")]
    [CustomValidation(typeof(Validation), "PRIMARY_ID", ErrorMessage = "Borrower ID#Invalid#")]
    public int BORROWER_ID { get; set; }


    [CustomValidation(typeof(Validation), "NAME", ErrorMessage = "Borrower Name#Invalid#")]
    public string BORROWER_NAME { get; set; } = string.Empty;
}

public class UpdateBorrowerSubmitResponse : ResponseModel
{
    public int ID { get; set; }
    public string Message { get; set; } = string.Empty;
}