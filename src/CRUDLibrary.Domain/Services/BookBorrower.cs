using CRUDLibrary.Data.LIB_DB.Enum;
using CRUDLibrary.Domain.Interfaces;
using CRUDLibrary.Domain.Models;

namespace CRUDLibrary.Domain.Services;

public class BookBorrower : IBookBorrower
{
    public IValidation _validate;
    public IDAL _DAL;

    public BookBorrower(IValidation validate, IDAL DAL)
    {
        _validate = validate;
        _DAL = DAL;
    }
    
    //------------------------------------
    public async Task<IEnumerable<BookDto>> GetBooks()
    {
        return await _DAL.QueryGetBooks();
    }
    //------------------------------------
    public async Task<IEnumerable<GenreDto>> GetGenres()
    {
        return await _DAL.QueryGetGenres();
    }
    //------------------------------------
    public async Task<IEnumerable<BorrowerDto>> GetBorrowers()
    {
        return await _DAL.QueryGetBorrowers();
    }
    //------------------------------------
    public async Task<AddBookBorrowerResponse> GetAddBookBorrower(AddBookBorrowerRequest _Request)
        {
            AddBookBorrowerResponse _Response = new();
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response.RESP_BOOK_ID = _Request.REQ_BOOK_ID;
                _Response.RESP_BOOK_TITLE = _Request.BOOK_TITLE;
                
                _Response.RESP_BORROWER_ID = _Request.REQ_BORROWER_ID;
                _Response.RESP_BORROWER_NAME = _Request.BORROWER_NAME;
                
                _Response.BOOKS = await _DAL.QueryGetBooks();
                _Response.BORROWERS = await _DAL.QueryGetBorrowers();
            }
            return _Response;
        }
    //------------------------------------
    public async Task<AddBookBorrowerSubmitResponse> SubmitAddBookBorrower(AddBookBorrowerSubmitRequest _Request)
        {
            AddBookBorrowerSubmitResponse _Response = new();
            var borrower = await _DAL.GetBorrowerById(_Request.BORROWER_ID);
            var book = await _DAL.GetBookById(_Request.BOOK_ID);
                
                // If no Book Id is provided but an Book Title is provided, create a new book
                if (book == null && !string.IsNullOrEmpty(_Request.BOOK_TITLE))
                {
                    AddBookSubmitResponse _rtn = new();
                    var newBookRequest = new AddBookSubmitRequest()
                    {
                        BOOK_TITLE = _Request.BOOK_TITLE,
                        BOOK_GENRE = _Request.BOOK_GENRE,
                        BOOK_PUB_DATE = _Request.BOOK_PUB_DATE.ToString()
                    };
                    
                    _validate.SubmitAddBook(newBookRequest);
                    if (_rtn.ERROR_MESSAGES.Count == 0)
                    {
                        var newBookResponse = await _DAL.InsertAddBook(newBookRequest);
                        _Response.RESP_BOOK_ID = newBookResponse.ID;
                    }
                    
                }
                else if (book != null)
                {
                    _Response.RESP_BOOK_ID = book.BookId;
                }
                else
                {
                    throw new Exception("Book information is insufficient.");
                }
                
                // If a Borrower Id is not provided, create a new Borrower.
                if (_Request.BORROWER_ID == null)
                {
                    AddBorrowerSubmitResponse _rtn = new();
                    var newBorrowerRequest = new AddBorrowerSubmitRequest()
                    {
                        BORROWER_NAME = _Request.BORROWER_NAME
                    };
                    _validate.SubmitAddBorrower(newBorrowerRequest);
                    if (_rtn.ERROR_MESSAGES.Count == 0)
                    {
                        var newBorrowerResponse = await _DAL.InsertAddBorrower(newBorrowerRequest);
                        _Response.RESP_BORROWER_ID = newBorrowerResponse.ID;
                    }
                }
                else
                {
                    if (borrower == null)
                    {
                        throw new Exception("Borrower not found.");
                    }
                    _Response.RESP_BORROWER_ID = _Request.BORROWER_ID;
                }
                
                // Create a BookBorrower relation
                var newBookBorrowerRequest = new AddBookBorrowerSubmitRequest
                {
                    BORROWER_ID = borrower.BorrowerId, 
                    BOOK_ID = book.BookId
                };
                var newBookBorrowerResponse = await _DAL.InsertAddBookBorrower(newBookBorrowerRequest);
                    
                if (newBookBorrowerResponse == null)
                    throw new Exception("Failed to create BookBorrower relation.");
            return _Response;
        }
    //------------------------------------
    public async Task<UpdateBookBorrowerResponse> GetUpdateBookBorrower(UpdateBookBorrowerRequest _Request)
        {
            UpdateBookBorrowerResponse _Response = new();
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.QueryUpdateBookBorrower(_Request);
            }

            return _Response;
        }
    //------------------------------------
    public async Task<UpdateBookBorrowerSubmitResponse> SubmitUpdateBookBorrower(UpdateBookBorrowerSubmitRequest _Request)
         {
                UpdateBookBorrowerSubmitResponse _Response = new();
            
                if (_Response.ERROR_MESSAGES.Count == 0)
                {
                    _Response = await _DAL.UpdateBookBorrower(_Request);
                }
            
                return _Response;
         }
    //------------------------------------
}