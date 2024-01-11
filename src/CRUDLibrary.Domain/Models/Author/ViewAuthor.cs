using System.ComponentModel;

namespace CRUDLibrary.Domain.Models;

public class ViewAuthorRequest : RequestModel
{
    public int AUTHOR_ID { get; set; }
}

public class ViewAuthorResponse : ResponseModel
{
    public int AUTHOR_ID { get; set; }
    
    public string AUTHOR_NAME { get; set; } = string.Empty;
    public string AUTHOR_BORN { get; set; } = string.Empty;
    public string AUTHOR_DIED { get; set; } = string.Empty;

    public IList<AuthorBookDto> AUTHORED_BOOKS { get; set; } = new List<AuthorBookDto>();

}
