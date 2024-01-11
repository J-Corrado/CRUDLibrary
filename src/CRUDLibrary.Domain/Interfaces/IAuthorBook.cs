using CRUDLibrary.Domain.Models;

namespace CRUDLibrary.Domain.Interfaces;

public interface IAuthorBook
{
    Task<IEnumerable<BookDto>> GetBooks();
    Task<IEnumerable<AuthorDto>> GetAuthors();
    Task<AddAuthorBookResponse> GetAddAuthorBook(AddAuthorBookRequest _Request);
    Task<AddAuthorBookSubmitResponse> SubmitAddAuthorBook(AddAuthorBookSubmitRequest _Request);
    Task<DeleteAuthorBookResponse> GetDeleteAuthorBook(DeleteAuthorBookRequest _Request);
    Task<DeleteAuthorBookSubmitResponse> SubmitDeleteAuthorBook(DeleteAuthorBookSubmitRequest _Request);
}