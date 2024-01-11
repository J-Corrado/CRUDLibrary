namespace CRUDLibrary.Domain.Models;

public class BorrowerNameSearchRequest : RequestModel
{
    public string SEARCH_TEXT { get; set; } = string.Empty;
}

public class BorrowerNameSearchResponse : ResponseModel
{
    public List<AutoCompleteItem> BORROWERS { get; set; } = new List<AutoCompleteItem>();
}