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
                if (!string.IsNullOrEmpty(_Request.AUTHOR_ID))
                {
                    var author = await _DAL.GetAuthorById(int.Parse(_Request.AUTHOR_ID));
                    _Response.AUTHOR_ID = author.AuthorId;
                    _Response.AUTHOR_NAME = author.Name;
                }
                else
                {
                    _Response.AUTHOR_NAME = _Request.AUTHOR_NAME;
                    _Response.AUTHOR_BORN = _Request.AUTHOR_BORN;
                    _Response.AUTHOR_DIED = _Request.AUTHOR_DIED ?? string.Empty;
                }

                if (!string.IsNullOrEmpty(_Request.BOOK_ID))
                {
                    var book = await _DAL.GetBookById(int.Parse(_Request.BOOK_ID));
                    _Response.BOOK_ID = book.BookId;
                    _Response.BOOK_TITLE = book.Title;
                }
                else
                {
                    _Response.BOOK_TITLE = _Request.BOOK_TITLE;
                    _Response.BOOK_GENRE = _Request.BOOK_GENRE;
                    _Response.BOOK_PUB_DATE = _Request.BOOK_PUB_DATE;
                }

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

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.InsertAddAuthorBook(_Request);
            }

            return _Response;
        }
    //------------------------------------
    public async Task<DeleteAuthorBookResponse> GetDeleteAuthorBook(DeleteAuthorBookRequest _Request)
        {
            DeleteAuthorBookResponse _Response = new();
            var authoredBook = await _DAL.QueryDeleteAuthorBook(_Request);
            
            if (_Response.RESP_AUTHOR_BOOK_ID != null)
            {
                _Response.AUTHOR_ID = int.Parse(_Request.AUTHOR_ID);
                _Response.BOOK_ID = int.Parse(_Request.BOOK_ID);
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
            }
            
            return _Response;
        }
    //------------------------------------
    
}