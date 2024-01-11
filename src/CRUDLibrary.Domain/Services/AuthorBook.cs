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
                _Response.RESP_AUTHOR_ID = _Request.REQ_AUTHOR_ID;
                _Response.RESP_AUTHOR_NAME = _Request.REQ_AUTHOR_NAME;
                _Response.AUTHORS = await _DAL.QueryGetAuthors();

                _Response.RESP_BOOK_ID = _Request.REQ_BOOK_ID;
                _Response.RESP_BOOK_TITLE = _Request.REQ_BOOK_TITLE;
                _Response.BOOKS = await _DAL.QueryGetBooks();
            }

            return _Response;
            
        }
    //------------------------------------
    public async Task<AddAuthorBookSubmitResponse> SubmitAddAuthorBook(AddAuthorBookSubmitRequest _Request)
        {
            AddAuthorBookSubmitResponse _Response = new();
                    
            var author = await _DAL.GetAuthorById(_Request.AUTHOR_ID);
            var book = await _DAL.GetBookById(_Request.BOOK_ID);
                
            // If no Author Id is provided but an Author Name is provided, create a new author
            if (author == null && !string.IsNullOrEmpty(_Request.AUTHOR_NAME))
            {
                AddAuthorSubmitResponse _rtn = new();
                var newAuthorRequest = new AddAuthorSubmitRequest()
                {
                    AUTHOR_NAME = _Request.AUTHOR_NAME
                };
                    
                _validate.SubmitAddAuthor(newAuthorRequest);
                if (_rtn.ERROR_MESSAGES.Count == 0)
                {
                    var newAuthorResponse = await _DAL.InsertAddAuthor(newAuthorRequest);
                    _Response.RESP_AUTHOR_ID = newAuthorResponse.ID;
                }
                    
            }
            else if (author != null)
            {
                _Response.RESP_AUTHOR_ID = author.AuthorId;
            }
            else
            {
                throw new Exception("Author information is insufficient.");
            }
                
            // If a Book Id is not provided, create a new Book.
            if (_Request.BOOK_ID == null)
            {
                AddBookSubmitResponse _rtn = new();
                var newBookRequest = new AddBookSubmitRequest()
                {
                    BOOK_TITLE = _Request.BOOK_TITLE,
                    BOOK_GENRE = (Data.LIB_DB.Enum.BookGenre)_Request.BOOK_GENRE,
                    BOOK_PUB_DATE = _Request.BOOK_PUB_DATE
                };
                _validate.SubmitAddBook(newBookRequest);
                if (_rtn.ERROR_MESSAGES.Count == 0)
                {
                    var newBookResponse = await _DAL.InsertAddBook(newBookRequest);
                    _Response.RESP_BOOK_ID = newBookResponse.ID;
                }
            }
            else
            {
                if (book == null)
                {
                    throw new Exception("Book not found.");
                }
                _Response.RESP_BOOK_ID = _Request.BOOK_ID;
            }
            
            var newAuthorBookRequest = new AddAuthorBookSubmitRequest
            {
                AUTHOR_ID = author.AuthorId, 
                BOOK_ID = book.BookId
            };
            var newAuthorBookResponse = await _DAL.InsertAddAuthorBook(newAuthorBookRequest);
                    
            if (newAuthorBookResponse == null)
                throw new Exception("Failed to create AuthorBook relation.");

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