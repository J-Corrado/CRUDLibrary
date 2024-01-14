using CRUDLibrary.Domain.Interfaces;
using CRUDLibrary.Domain.Models;

namespace CRUDLibrary.Domain.Services;

public class AuthorBook : IAuthorBook
{
    public IValidation _validate;
    public IDAL _DAL;

    public AuthorBook(IValidation validate, IDAL DAL)
    {
        _validate = _validate;
        _DAL = DAL;
    }
    
    //------------------------------------
    public async Task<IEnumerable<BookDto>> GetBooks()
    {
        return await _DAL.QueryGetBooks();
    }
    //------------------------------------
    public async Task<IEnumerable<AuthorDto>> GetAuthors()
    {
        return await _DAL.QueryGetAuthors();
    }
    //------------------------------------
    public async Task<AddAuthorBookResponse> GetAddAuthorBook(AddAuthorBookRequest _Request)
        {
            AddAuthorBookResponse _Response = new();
            
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                var author = await _DAL.GetAuthorById(int.Parse(_Request.AUTHOR_ID));
                _Response.AUTHOR_ID = author.AuthorId;
                _Response.AUTHOR_NAME = author.Name;
                _Response.AUTHORS = await _DAL.QueryGetAuthors();
                _Response.BOOKS = await _DAL.QueryGetBooks();
                _Response.GENRES = await _DAL.QueryGetGenres();
            }

            return _Response;
            
        }
    //------------------------------------
    public async Task<AddAuthorBookSubmitResponse> SubmitAddAuthorBook(AddAuthorBookSubmitRequest _Request)
        {
            AddAuthorBookSubmitResponse _Response = new();
                    
            var author = await _DAL.GetAuthorById(int.Parse(_Request.AUTHOR_ID));
            
                
            // If a Book Id is not provided, create a new Book.
            if (!string.IsNullOrEmpty(_Request.BOOK_ID))
            {
                var book = await _DAL.GetBookById(int.Parse(_Request.BOOK_ID));

                _Request.AUTHOR_ID = author.AuthorId.ToString();
                _Request.BOOK_ID = book.BookId.ToString();
                await _DAL.InsertAddAuthorBook(_Request);
            }
            else if (!string.IsNullOrEmpty(_Request.BOOK_TITLE))
            {
                var reqAddBook = new AddBookSubmitRequest()
                {
                    BOOK_TITLE = _Request.BOOK_TITLE,
                    BOOK_GENRE = _Request.BOOK_GENRE,
                    BOOK_PUB_DATE = _Request.BOOK_PUB_DATE
                };
                var respAddBook = await _DAL.InsertAddBook(reqAddBook);
                _Request.BOOK_ID = respAddBook.ID.ToString();
                await _DAL.InsertAddAuthorBook(_Request);
            }

            _Response.ID = int.Parse(_Request.AUTHOR_ID);
            return _Response;
        }
    //------------------------------------
    public async Task<DeleteAuthorBookResponse> GetDeleteAuthorBook(DeleteAuthorBookRequest _Request)
        {
            DeleteAuthorBookResponse _Response = new();
            var authoredBook = await _DAL.QueryDeleteAuthorBook(_Request);
            
            if (_Response.RESP_AUTHOR_BOOK_ID != null)
            {
                _Response.AUTHOR_ID = _Request.AUTHOR_ID;
                _Response.BOOK_ID = _Request.BOOK_ID;
                _Response.AUTHOR_BOOK_ID = authoredBook.AUTHOR_BOOK_ID;
            }
            
            return _Response;
        }
    //------------------------------------
    public async Task<DeleteAuthorBookSubmitResponse> SubmitDeleteAuthorBook(DeleteAuthorBookSubmitRequest _Request)
        {
            DeleteAuthorBookSubmitResponse _Response = new();
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.SubmitDeleteAuthorBook(_Request);
                _Response.Successful = true;
                _Response.Message = "AuthorBook deleted successfully.";
            }
            else
            {
                _Response.Successful = false;
                _Response.Message = "Failed to delete AuthorBook.";
            }
            return _Response;
        }
    //------------------------------------
    
}