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

                if (!string.IsNullOrEmpty(_Request.BORROWER_ID))
                {
                    var borrower = await _DAL.GetBorrowerById(int.Parse(_Request.BORROWER_ID)); 
                    _Response.BORROWER_ID = borrower.BorrowerId;
                    _Response.BORROWER_NAME = borrower.Name;
                }
                else
                {
                    _Response.BORROWER_NAME = _Request.BORROWER_NAME;
                }
                
                _Response.BOOKS = await _DAL.QueryGetBooks();
                _Response.GENRES = await _DAL.QueryGetGenres();
                _Response.BORROWERS = await _DAL.QueryGetBorrowers();
            }
            return _Response;
        }
    //------------------------------------
    public async Task<AddBookBorrowerSubmitResponse> SubmitAddBookBorrower(AddBookBorrowerSubmitRequest _Request)
        {
            AddBookBorrowerSubmitResponse _Response = new();
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.InsertAddBookBorrower(_Request);
            }

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

            _Response.BOOK_ID = _Request.BOOK_ID;
            _Response.BORROWER_ID = _Request.BORROWER_ID;
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