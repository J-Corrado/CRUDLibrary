namespace CRUDLibrary.Domain.Models;

public class AllBorrowersRequest : RequestModel
{
    
}

public class AllBorrowersResponse : ResponseModel
{
    public List<BorrowerDto> BORROWERS { get; set; } = new List<BorrowerDto>();
}