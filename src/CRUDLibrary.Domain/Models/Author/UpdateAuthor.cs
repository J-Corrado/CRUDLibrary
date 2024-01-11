using System.ComponentModel.DataAnnotations;
using CRUDLibrary.Domain.Services;

namespace CRUDLibrary.Domain.Models;

public class UpdateAuthorRequest : RequestModel
{
    public decimal ID { get; set; }
}

public class UpdateAuthorResponse : ResponseModel
{
    public decimal AUTHOR_ID { get; set; }
    public string AUTHOR_NAME { get; set; } = string.Empty;
    public DateTime? AUTHOR_BORN { get; set; }
    public DateTime? AUTHOR_DIED { get; set; }
}

public class UpdateAuthorSubmitRequest : RequestModel
{
    [Required(ErrorMessage = "Author ID#Required#")]
    [CustomValidation(typeof(Validation), "PRIMARY_ID", ErrorMessage = "Author ID#Invalid#")]
    public decimal AUTHOR_ID { get; set; }
    
    
    [CustomValidation(typeof(Validation), "NAME", ErrorMessage = "Author Name#Invalid#")]
    public string AUTHOR_NAME { get; set; } = string.Empty;
    
    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Author Date of Birth#Invalid#")]
    public DateTime? AUTHOR_BORN { get; set; }
    
    [CustomValidation(typeof(Validation), "VAL_DATE", ErrorMessage = "Author Date of Death#Invalid#")]
    public DateTime? AUTHOR_DIED { get; set; }
    
    public List<AuthorBookDto> AUTHORED_BOOKS { get; set; } = new List<AuthorBookDto>();
}

public class UpdateAuthorSubmitResponse : ResponseModel
{
    public decimal ID { get; set; }
    public string Message { get; set; } = string.Empty;
}