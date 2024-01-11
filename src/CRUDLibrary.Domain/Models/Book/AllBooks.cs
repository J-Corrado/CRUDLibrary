namespace CRUDLibrary.Domain.Models;

public class AllBooksRequest : RequestModel
{
    
}

public class AllBooksResponse : ResponseModel
{
    public List<BookDto> BOOKS { get; set; } = new List<BookDto>();
}