namespace CRUDLibrary.Domain.Models;

public class AllAuthorsRequest : RequestModel
{
    
}

public class AllAuthorsResponse : ResponseModel
{
    public List<AuthorDto> AUTHORS { get; set; } = new List<AuthorDto>();
}