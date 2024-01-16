using CRUDLibrary.Data;
using CRUDLibrary.Data.LIB_DB.Enum;
using CRUDLibrary.Domain.Interfaces;
using CRUDLibrary.Domain.Models;

namespace CRUDLibrary.Domain.Services
{
    public class Book : IBook
    {
        public IValidation _validate;
        public IDAL _DAL;
        public Book(IValidation validate, IDAL DAL)
        {
            _validate = validate;
            _DAL = DAL;
        }

        //------------------------------------
        public async Task<AddBookResponse> GetAddBook(AddBookRequest _Request)
        {
            AddBookResponse _Response = new();

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response.RESP_BOOK_ID = _Request.REQ_AUTHOR_ID;
                _Response.RESP_BOOK_TITLE = _Request.REQ_AUTHOR_NAME;
                _Response.GENRES = await _DAL.QueryGetGenres();
                _Response.AUTHORS = await _DAL.QueryGetAuthors();
            }

            return _Response;
        }
        //------------------------------------
        public async Task<AddBookSubmitResponse> SubmitAddBook(AddBookSubmitRequest _Request)
        {
            AddBookSubmitResponse _Response = new();
            //_validate.SubmitAddBook(_Request);

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.InsertAddBook(_Request);
                if (_Request.AUTHOR_ID != null)
                {
                    await _DAL.InsertAddAuthorBook(new AddAuthorBookSubmitRequest()
                    {
                        AUTHOR_ID = _Request.AUTHOR_ID, 
                        BOOK_ID = _Response.ID.ToString()
                    });
                }
                else if (!string.IsNullOrEmpty(_Request.AUTHOR_NAME))
                {
                    var author = await _DAL.InsertAddAuthor(new AddAuthorSubmitRequest()
                    {
                        AUTHOR_NAME = _Request.AUTHOR_NAME, 
                    });
                    await _DAL.InsertAddAuthorBook(new AddAuthorBookSubmitRequest()
                    {
                        AUTHOR_ID = author.ID.ToString(), 
                        BOOK_ID = _Request.BOOK_ID
                    });
                }
            }
            return _Response;
        }
        //------------------------------------
        public async Task<AllBooksResponse> GetAllBooks(AllBooksRequest _Request)
        {
            AllBooksResponse _Response = new();

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.QueryGetAllBooks(_Request);
            }
            
            return _Response;
        }
        //------------------------------------
        public async Task<IEnumerable<GenreDto>> GetGenres()
        {
            return await _DAL.QueryGetGenres();
        }
        //------------------------------------
        public async Task<BookTitleSearchResponse> GetBookTitleSearch(BookTitleSearchRequest _Request)
        {
            BookTitleSearchResponse _Response = new();

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.QueryGetBookTitleSearch(_Request);
            }
            
            return _Response;
        }
        //------------------------------------
        public async Task<ViewBookResponse> GetViewBook(ViewBookRequest _Request)
        {
            ViewBookResponse _Response = new();
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                var book = await _DAL.GetBookById(_Request.BOOK_ID);
                if (book != null)
                {
                    _Response = await _DAL.QueryGetViewBook(_Request);
                    if (_Response.ERROR_MESSAGES.Count() == 0)
                    {
                        _Response.BOOK_TITLE = book.Title;
                        _Response.BOOK_PUB_DATE = book.PublicationDate;
                        _Response.BOOK_GENRE = book.Genre;
                    }

                    var authoredBooks = await _DAL.QueryGetBookAuthors(_Request.BOOK_ID);
                    if (authoredBooks != null)
                    {
                        _Response.BOOK_AUTHORS = authoredBooks.Select(ab => new AuthorBookDto
                                            {
                                                ID = ab.ID,
                                                AUTHOR_ID = ab.AUTHOR_ID,
                                                AUTHOR_NAME = ab.AUTHOR_NAME,
                                                AUTHOR_BORN = ab.AUTHOR_BORN,
                                                AUTHOR_DIED = ab.AUTHOR_DIED
                                            }).ToList();
                    }
                    
                    var borrowedBooks = await _DAL.QueryGetBorrowersByBook(_Request.BOOK_ID);
                    if (borrowedBooks != null)
                    {
                        _Response.BOOK_BORROWERS = borrowedBooks.Select(bb => new BookBorrowerDto
                                            {
                                                ID = bb.ID,
                                                BORROWER_ID = bb.BORROWER_ID,
                                                BORROWER_NAME = bb.BORROWER_NAME,
                                                BORROWED_DATE = bb.BORROWED_DATE
                                            }).ToList();
                    }
                }
            }
            return _Response;
        }
        //------------------------------------
        public async Task<UpdateBookResponse> GetUpdateBook(UpdateBookRequest _Request)
        {
            UpdateBookResponse _Response = new();
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.QueryUpdateBook(_Request);
                _Response.RESP_BOOK_TITLE = _Request.REQ_BOOK_TITLE;
                _Response.RESP_BOOK_ID = _Request.REQ_BOOK_ID;
            }

            return _Response;
        }
        //------------------------------------
        public async Task<UpdateBookSubmitResponse> SubmitUpdateBook(UpdateBookSubmitRequest _Request)
        {
            UpdateBookSubmitResponse _Response = new();
            
            _validate.SubmitUpdateBook(ref _Request, ref _Response);
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.UpdateBook(_Request);
            }

            return _Response;
        }
        //------------------------------------
        public async Task<DeleteBookResponse> GetDeleteBook(DeleteBookRequest _Request)
        {
            DeleteBookResponse _Response = new();
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.QueryDeleteBook(_Request);
                _Response.RESP_BOOK_TITLE = _Request.REQ_BOOK_TITLE;
                _Response.RESP_BOOK_ID = _Request.REQ_BOOK_ID;
            }

            return _Response;
        }
        //------------------------------------
        public async Task<DeleteBookSubmitResponse> SubmitDeleteBook(DeleteBookSubmitRequest _Request)
        {
            DeleteBookSubmitResponse _Response = new();
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.SubmitDeleteBook(_Request);
                _Response.Successful = true;
                _Response.Message = "Book deleted successfully.";
            }
            else
            {
                _Response.Successful = false;
                _Response.Message = "Failed to delete book.";
            }
            return _Response;
        }
        //------------------------------------
    }
}
