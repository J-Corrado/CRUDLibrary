using CRUDLibrary.Domain.Models;
using CRUDLibrary.Data.LIB_DB;

namespace CRUDLibrary.Domain.Interfaces
{
    public interface IDAL
    {
        #region Author
        Task<Author> GetAuthorById(decimal authorId);
        Task<AllAuthorsResponse> QueryGetAllAuthors(AllAuthorsRequest _Request);
        Task<UpdateAuthorResponse> QueryUpdateAuthor(UpdateAuthorRequest _Request);
        Task<AddAuthorSubmitResponse> InsertAddAuthor(AddAuthorSubmitRequest _Request);
        Task<IEnumerable<AuthorBookDto>> QueryGetAuthoredBooks(decimal authorId);
        Task<UpdateAuthorSubmitResponse> UpdateAuthor(UpdateAuthorSubmitRequest _Request);
        Task<AuthorNameSearchResponse> QueryGetAuthorNameSearch(AuthorNameSearchRequest _Request);
        Task<ViewAuthorResponse> QueryGetViewAuthor(ViewAuthorRequest _Request);
        Task<DeleteAuthorResponse> QueryDeleteAuthor(DeleteAuthorRequest _Request);
        Task<DeleteAuthorSubmitResponse> SubmitDeleteAuthor(DeleteAuthorSubmitRequest _Request);
        #endregion

        #region Book
        
        Task<Book> GetBookById(decimal bookId);
        Task<IEnumerable<GenreEnum>> QueryGetGenres();
        Task<AllBooksResponse> QueryGetAllBooks(AllBooksRequest _Request);
        Task<AddBookSubmitResponse> InsertAddBook(AddBookSubmitRequest _Request);
        Task<IEnumerable<AuthorBookDto>> QueryGetBookAuthors(decimal bookId);
        Task<IEnumerable<BookBorrowerDto>> QueryGetBorrowersByBook(decimal bookId);
        Task<UpdateBookResponse> QueryUpdateBook(UpdateBookRequest _Request);
        Task<UpdateBookSubmitResponse> UpdateBook(UpdateBookSubmitRequest _Request);
        Task<BookTitleSearchResponse> QueryGetBookTitleSearch(BookTitleSearchRequest _Request);
        Task<ViewBookResponse> QueryGetViewBook(ViewBookRequest _Request);
        Task<DeleteBookResponse> QueryDeleteBook(DeleteBookRequest _Request);
        Task<DeleteBookSubmitResponse> SubmitDeleteBook(DeleteBookSubmitRequest _Request);
        #endregion

        #region Borrower
        Task<Borrower> GetBorrowerById(decimal borrowerId);
        Task<AllBorrowersResponse> QueryGetAllBorrowers(AllBorrowersRequest _Request);
        Task<AddBorrowerSubmitResponse> InsertAddBorrower(AddBorrowerSubmitRequest _Request);
        Task<IEnumerable<BookBorrowerDto>> QueryGetBooksByBorrower(decimal borrowerId);
        Task<UpdateBorrowerResponse> QueryUpdateBorrower(UpdateBorrowerRequest _Request);
        Task<UpdateBorrowerSubmitResponse> UpdateBorrower(UpdateBorrowerSubmitRequest _Request);
        Task<BorrowerNameSearchResponse> QueryGetBorrowerNameSearch(BorrowerNameSearchRequest _Request);
        Task<ViewBorrowerResponse> QueryGetViewBorrower(ViewBorrowerRequest _Request);
        Task<DeleteBorrowerResponse> QueryDeleteBorrower(DeleteBorrowerRequest _Request);
        Task<DeleteBorrowerSubmitResponse> SubmitDeleteBorrower(DeleteBorrowerSubmitRequest _Request);
        #endregion

        #region AuthorBook
        Task<AddAuthorBookSubmitResponse> InsertAddAuthorBook(AddAuthorBookSubmitRequest _Request);
        Task<UpdateAuthorBookSubmitResponse> UpdateAuthorBook(UpdateAuthorBookSubmitRequest _Request);
        Task<DeleteAuthorBookResponse> QueryDeleteAuthorBook(DeleteAuthorBookRequest _Request);
        Task<DeleteAuthorBookSubmitResponse> SubmitDeleteAuthorBook(DeleteAuthorBookSubmitRequest _Request);
        #endregion

        #region BookBorrower
        
        Task<AddBookBorrowerSubmitResponse> InsertAddBookBorrower(AddBookBorrowerSubmitRequest _Request);
        Task<UpdateBookBorrowerResponse> QueryUpdateBookBorrower(UpdateBookBorrowerRequest _Request);
        Task<UpdateBookBorrowerSubmitResponse> UpdateBookBorrower(UpdateBookBorrowerSubmitRequest _Request);
        Task<DeleteBookBorrowerResponse> QueryDeleteBookBorrower(DeleteBookBorrowerRequest _Request);
        Task<DeleteBookBorrowerSubmitResponse> SubmitDeleteBookBorrower(DeleteBookBorrowerSubmitRequest _Request);
        #endregion

        #region Get
        Task<List<BookDto>> QueryGetBooks();
        Task<List<BorrowerDto>> QueryGetBorrowers(); 
        Task<List<AuthorDto>> QueryGetAuthors();

        #endregion

        #region Validation

        Task<List<MessageListItem>> ValidateInsertAuthor(AddAuthorSubmitRequest _Request);
        Task<List<MessageListItem>> ValidateInsertBook(AddBookSubmitRequest _Request);
        Task<List<MessageListItem>> ValidateInsertBorrower(AddBorrowerSubmitRequest _Request);

        #endregion
    }
}
