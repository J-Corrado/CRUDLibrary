namespace CRUDLibrary.Domain.Models;

public class SearchRequest: RequestModel
{
    public string SEARCH_TEXT { get; set; } = string.Empty;
    public string SEARCH_TYPE { get; set; } = string.Empty;
}

public class SearchResponse: ResponseModel
{
    public string SEARCH_TEXT { get; set; } = string.Empty;
    public string SEARCH_TYPE { get; set; } = string.Empty;
    public List<SearchResultItem> RESULTS = new List<SearchResultItem>();
}
public class SearchResultItem
{
    public decimal ID { get; set; }
    public string TYPE { get; set; } = string.Empty;
    public string RESULT { get; set; } = string.Empty;
}