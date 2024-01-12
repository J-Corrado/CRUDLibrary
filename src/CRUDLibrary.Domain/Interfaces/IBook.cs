using CRUDLibrary.Data.LIB_DB.Enum;
using CRUDLibrary.Domain.Models;

namespace CRUDLibrary.Domain.Interfaces
{
    public interface IBook
    {
        Task<AddBookResponse> GetAddBook(AddBookRequest _Request);
        Task<AddBookSubmitResponse> SubmitAddBook(AddBookSubmitRequest _Request);
        Task<IEnumerable<GenreDto>> GetGenres();
        Task<AllBooksResponse> GetAllBooks(AllBooksRequest _Request);
        Task<BookTitleSearchResponse> GetBookTitleSearch(BookTitleSearchRequest _Request);
        Task<ViewBookResponse> GetViewBook(ViewBookRequest _Request);
        Task<UpdateBookResponse> GetUpdateBook(UpdateBookRequest _Request);
        Task<UpdateBookSubmitResponse> SubmitUpdateBook(UpdateBookSubmitRequest _Request);

        Task<DeleteBookResponse> GetDeleteBook(DeleteBookRequest _Request);
        Task<DeleteBookSubmitResponse> SubmitDeleteBook(DeleteBookSubmitRequest _Request);
    }
}
