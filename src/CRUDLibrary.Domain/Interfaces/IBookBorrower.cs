using CRUDLibrary.Domain.Models;

namespace CRUDLibrary.Domain.Interfaces;

public interface IBookBorrower
{
    Task<IEnumerable<BorrowerDto>> GetBorrowers();
    Task<IEnumerable<BookDto>> GetBooks();
    Task<IEnumerable<GenreDto>> GetGenres();
    Task<AddBookBorrowerResponse> GetAddBookBorrower(AddBookBorrowerRequest _Request);
    Task<AddBookBorrowerSubmitResponse> SubmitAddBookBorrower(AddBookBorrowerSubmitRequest _Request);
    Task<UpdateBookBorrowerResponse> GetUpdateBookBorrower(UpdateBookBorrowerRequest _Request);
    Task<UpdateBookBorrowerSubmitResponse> SubmitUpdateBookBorrower(UpdateBookBorrowerSubmitRequest _Request);
}