using System.ComponentModel;

namespace CRUDLibrary.Domain.Models;

public class ViewAuthorRequest : RequestModel
{
    public decimal AUTHOR_ID { get; set; }
}

public class ViewAuthorResponse : ResponseModel
{
    public decimal AUTHOR_ID { get; set; }
    
    public string AUTHOR_NAME { get; set; } = string.Empty;
    public DateTime? AUTHOR_BORN { get; set; }
    public DateTime? AUTHOR_DIED { get; set; }

    public IList<AuthorBookDto> AUTHORED_BOOKS { get; set; } = new List<AuthorBookDto>();

}
