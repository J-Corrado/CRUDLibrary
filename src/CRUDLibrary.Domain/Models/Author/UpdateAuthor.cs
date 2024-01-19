using System.ComponentModel.DataAnnotations;
using CRUDLibrary.Domain.Services;

namespace CRUDLibrary.Domain.Models;

public class UpdateAuthorRequest : RequestModel
{
    public string AUTHOR_ID { get; set; } = string.Empty;
}

public class UpdateAuthorResponse : ResponseModel
{
    public string AUTHOR_ID { get; set; } = string.Empty;
    public string AUTHOR_NAME { get; set; } = string.Empty;
    public string? AUTHOR_BORN { get; set; } = string.Empty;
    public string? AUTHOR_DIED { get; set; } = string.Empty;
}

public class UpdateAuthorSubmitRequest : RequestModel
{
    [Required(ErrorMessage = "Author ID#Required#")]
    [CustomValidation(typeof(Validation), "ID", ErrorMessage = "Author ID#Invalid#")]
    public string AUTHOR_ID { get; set; } = string.Empty;
    
    
    [CustomValidation(typeof(Validation), "NAME", ErrorMessage = "Author Name#Invalid#")]
    public string AUTHOR_NAME { get; set; } = string.Empty;

    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Author Date of Birth#Invalid#")]
    public string AUTHOR_BORN { get; set; } = string.Empty;

    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Author Date of Death#Invalid#")]
    public string AUTHOR_DIED { get; set; } = string.Empty;
    
}

public class UpdateAuthorSubmitResponse : ResponseModel
{
    public int ID { get; set; }
}