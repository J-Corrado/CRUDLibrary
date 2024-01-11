using System.ComponentModel;

namespace CRUDLibrary.Domain.Models;

public class AuthorNameSearchRequest : RequestModel
{
    public string SEARCH_TEXT { get; set; } = string.Empty;
}

public class AuthorNameSearchResponse : ResponseModel
{
    public List<AutoCompleteItem> AUTHORS { get; set; } = new List<AutoCompleteItem>();
}