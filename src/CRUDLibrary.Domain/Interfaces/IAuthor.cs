using CRUDLibrary.Domain.Models;

namespace CRUDLibrary.Domain.Interfaces
{
    public interface IAuthor
    {
        Task<AddAuthorResponse> GetAddAuthor(AddAuthorRequest _Request);
        Task<AddAuthorSubmitResponse> SubmitAddAuthor(AddAuthorSubmitRequest _Request);
        
        Task<AllAuthorsResponse> GetAllAuthors(AllAuthorsRequest _Request);
        Task<AuthorNameSearchResponse> GetAuthorNameSearch(AuthorNameSearchRequest _Request);
        Task<ViewAuthorResponse> GetViewAuthor(ViewAuthorRequest _Request);
        Task<UpdateAuthorResponse> GetUpdateAuthor(UpdateAuthorRequest _Request);
        Task<UpdateAuthorSubmitResponse> SubmitUpdateAuthor(UpdateAuthorSubmitRequest _Request);
        Task<DeleteAuthorResponse> GetDeleteAuthor(DeleteAuthorRequest _Request);
        Task<DeleteAuthorSubmitResponse> SubmitDeleteAuthor(DeleteAuthorSubmitRequest _Request);
        
    }
}
