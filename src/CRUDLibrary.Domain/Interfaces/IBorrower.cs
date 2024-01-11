using CRUDLibrary.Domain.Models;

namespace CRUDLibrary.Domain.Interfaces
{
    public interface IBorrower
    {
        Task<AddBorrowerResponse> GetAddBorrower(AddBorrowerRequest _Request);
        Task<AddBorrowerSubmitResponse> SubmitAddBorrower(AddBorrowerSubmitRequest _Request);
        Task<AllBorrowersResponse> GetAllBorrowers(AllBorrowersRequest _Request);
        Task<BorrowerNameSearchResponse> GetBorrowerNameSearch(BorrowerNameSearchRequest _Request);
        Task<ViewBorrowerResponse> GetViewBorrower(ViewBorrowerRequest _Request);
        Task<UpdateBorrowerResponse> GetUpdateBorrower(UpdateBorrowerRequest _Request);
        Task<UpdateBorrowerSubmitResponse> SubmitUpdateBorrower(UpdateBorrowerSubmitRequest _Request);
        Task<DeleteBorrowerResponse> GetDeleteBorrower(DeleteBorrowerRequest _Request);
        Task<DeleteBorrowerSubmitResponse> SubmitDeleteBorrower(DeleteBorrowerSubmitRequest _Request);
        
    }
}
