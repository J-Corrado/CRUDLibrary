namespace CRUDLibrary.Domain.Models;

public class BookTitleSearchRequest : RequestModel
{
    public string SEARCH_TEXT { get; set; } = string.Empty;
}

public class BookTitleSearchResponse : ResponseModel
{
    public List<AutoCompleteItem> BOOKS { get; set; } = new List<AutoCompleteItem>();
}