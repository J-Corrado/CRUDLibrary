using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CRUDLibrary.Data.LIB_DB.Enum;
using CRUDLibrary.Domain.Services;

namespace CRUDLibrary.Domain.Models;

public class AddAuthorRequest : RequestModel
{
}

public class AddAuthorResponse : ResponseModel
{
    public int AUTHOR_ID { get; set; }
    public string AUTHOR_NAME { get; set; } = string.Empty;
    public string AUTHOR_BORN { get; set; } = string.Empty;
    public string AUTHOR_DIED { get; set; } = string.Empty;

}

public class AddAuthorSubmitRequest : RequestModel
{

    [Required(ErrorMessage = "Author's Name#Required#")]
    [CustomValidation(typeof(Validation), "NAME", ErrorMessage = "Author Name#Invalid#")]
    public string? AUTHOR_NAME { get; set; } = string.Empty;

    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Author Date of Birth#Invalid#")]
    public string? AUTHOR_BORN { get; set; } = string.Empty;

    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Author Date of Death#Invalid#")]
    public string? AUTHOR_DIED { get; set; } = string.Empty;

}

public class AddAuthorSubmitResponse : ResponseModel
{
    public int ID { get; set; }
}