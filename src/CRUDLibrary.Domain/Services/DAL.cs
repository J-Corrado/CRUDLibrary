using CRUDLibrary.Domain.Interfaces;
using CRUDLibrary.Data.LIB_DB;
using CRUDLibrary.Data.LIB_DB.Enum;
using CRUDLibrary.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;

namespace CRUDLibrary.Domain.Services
{
    public class DAL : IDAL
    {
        public IConfiguration _config;
        public LIBDBContext _context;

        public DAL(IConfiguration config, LIBDBContext context)
        {
            _config = config;
            _context = context;
        }

        #region Author

        public async Task<List<AuthorDto>> QueryGetAuthors()
        {
            List<AuthorDto> _Response = new List<AuthorDto>();

            _Response = await _context.Authors.Select(x => new AuthorDto() { ID = x.AuthorId, NAME = x.Name }).ToListAsync();

            return _Response;
        }
        public async Task<Data.LIB_DB.Author> GetAuthorById(decimal authorId)
        {
            return await _context.Authors.FirstOrDefaultAsync(x => x.AuthorId == authorId);
        }
        public async Task<AllAuthorsResponse> QueryGetAllAuthors(AllAuthorsRequest _Request)
        {
            AllAuthorsResponse _Response = new AllAuthorsResponse();

            var authors = await _context.Authors.ToListAsync();
            if (authors != null)
            {
                foreach (var author in authors)
                {
                    _Response.AUTHORS.Add(new AuthorDto()
                    {
                        ID = author.AuthorId,
                        NAME = author.Name,
                        BORN = author.DateOfBirth,
                        DIED = author.DateOfDeath
                    });
                }
            }

            _Response.RESP_AUTHOR_ID = _Request.REQ_AUTHOR_ID;
            _Response.RESP_AUTHOR_NAME = _Request.REQ_AUTHOR_NAME;
            return _Response;
        }

        public async Task<UpdateAuthorResponse> QueryUpdateAuthor(UpdateAuthorRequest _Request)
        {
            UpdateAuthorResponse _Response = new();

            var _author = await _context.Authors.Where(a => a.AuthorId == _Request.ID)
                .Include(ab => ab.AuthorBooks)
                .ThenInclude(b => b.Book)
                .FirstOrDefaultAsync();

            if (_author != null)
            {
                _Response.AUTHOR_ID = _author.AuthorId;
                _Response.AUTHOR_NAME = _author.Name;
                _Response.AUTHOR_BORN = _author.DateOfBirth;
                _Response.AUTHOR_DIED = _author.DateOfDeath;

            }
            else
            {
                _Response.ERROR_MESSAGES.Add(new MessageListItem(){CDE = "CRITICAL", MESSAGE = "Author does not exist"});
            }

            _Response.RESP_AUTHOR_ID = _Request.REQ_AUTHOR_ID;
            return _Response;
            
        }
        public async Task<AddAuthorSubmitResponse> InsertAddAuthor(AddAuthorSubmitRequest _Request)
        {
            AddAuthorSubmitResponse _Response = new AddAuthorSubmitResponse();
            try
            {
                var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == _Request.AUTHOR_ID);

                if (author != null)
                {
                    // If AUTHOR_ID is found, Author exists
                    _Response.ERROR_MESSAGES.Add(new MessageListItem
                        { MESSAGE = "Author with given AUTHOR_ID already exists" });
                    return _Response;

                }
                else if (!string.IsNullOrEmpty(_Request.AUTHOR_NAME))
                {
                    // If a AUTHOR_NAME is provided, create a new author
                    author = new Data.LIB_DB.Author()
                    {
                        Name = _Request.AUTHOR_NAME,
                        DateOfBirth = _Request.AUTHOR_BORN,
                        DateOfDeath = _Request.AUTHOR_DIED
                    };
                    _context.Authors.Add(author);
                    await _context.SaveChangesAsync();
                }

                _Response.RESP_AUTHOR_ID = _Request.REQ_AUTHOR_ID;


                if (_Request.BOOK_ID != null)
                {
                    // If BOOK_ID is found, Book exists
                    var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == _Request.BOOK_ID);
                    if (book == null)
                    {
                        _Response.ERROR_MESSAGES.Add(new MessageListItem
                            { MESSAGE = "No Book found with the given BOOK_ID" });
                        return _Response;
                    }

                    //Add AuthorBook relationship
                    var authorBook = new Data.LIB_DB.AuthorBook()
                    {
                        Author = author,
                        AuthorId = author.AuthorId,
                        Book = book,
                        BookId = book.BookId
                    };
                    _context.AuthorBooks.Add(authorBook);
                    await _context.SaveChangesAsync();
                }
                else if (!string.IsNullOrEmpty(_Request.BOOK_TITLE))
                {
                    //If BOOK_TITLE is provided, create a new Book
                    var book = new Data.LIB_DB.Book()
                    {
                        Title = _Request.BOOK_TITLE,
                        PublicationDate = (DateTime)_Request.BOOK_PUB_DATE,
                        Genre = (Data.LIB_DB.Enum.BookGenre)_Request.BOOK_GENRE
                    };
                    _context.Books.Add(book);
                    await _context.SaveChangesAsync();

                    //Add AuthorBook relationship
                    var authorBook = new Data.LIB_DB.AuthorBook()
                    {
                        Author = author,
                        AuthorId = author.AuthorId,
                        Book = book,
                        BookId = book.BookId
                    };
                    _context.AuthorBooks.Add(authorBook);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
              _Response.ERROR_MESSAGES.Add(new MessageListItem(){MESSAGE = "Please enter Author"});  
            }

            return _Response;
        }
        public async Task<IEnumerable<AuthorBookDto>> QueryGetAuthoredBooks(decimal authorId)
        { 
            return await _context.AuthorBooks.Include(ab => ab.Book)
                .ThenInclude(b => b.AuthorBooks)
                .Where(ab => ab.AuthorId == authorId)
                .Select(ab => new AuthorBookDto()
                {
                    ID = ab.AuthorBookId,
                    AUTHOR_ID = ab.AuthorId,
                    BOOK_ID = ab.BookId,
                    AUTHOR_NAME = ab.Author.Name,
                    BOOK_TITLE = ab.Book.Title
                }).ToListAsync();
        }
        public async Task<UpdateAuthorSubmitResponse> UpdateAuthor(UpdateAuthorSubmitRequest _Request)
        {
            UpdateAuthorSubmitResponse _Response = new UpdateAuthorSubmitResponse();

            var author = await _context.Authors.Where(a => a.AuthorId == _Request.AUTHOR_ID)
                .Include(ab => ab.AuthorBooks)
                    .ThenInclude(b => b.Book)
                .FirstOrDefaultAsync();

            if (author != null)
            {
                if (!string.IsNullOrEmpty(_Request.AUTHOR_NAME))
                {
                    author.Name = _Request.AUTHOR_NAME;
                }
                if (_Request.AUTHOR_BORN != null)
                {
                    author.DateOfBirth = _Request.AUTHOR_BORN;
                }
                if (_Request.AUTHOR_DIED != null)
                {
                    author.DateOfDeath = _Request.AUTHOR_DIED;
                }
                
                await _context.SaveChangesAsync();
                _Response.ID = author.AuthorId;
            }

            _Response.RESP_AUTHOR_ID = _Request.REQ_AUTHOR_ID;
            
            return _Response;
        }
        public async Task<AuthorNameSearchResponse> QueryGetAuthorNameSearch(AuthorNameSearchRequest _Request)
        {
            AuthorNameSearchResponse _Response = new AuthorNameSearchResponse();

            var authors = await _context.Authors
                .Where(a => a.Name.ToLower().Contains(_Request.SEARCH_TEXT.ToLower()) || _Request.SEARCH_TEXT.ToLower().Contains(a.Name.ToLower()))
                .ToListAsync();

            if (authors != null)
            {
                _Response.AUTHORS = authors.Select(x => new AutoCompleteItem()
                {
                    value = x.AuthorId.ToString(), 
                    label = x.Name
                }).ToList();
            }
            
            return _Response;
        }
        public async Task<ViewAuthorResponse> QueryGetViewAuthor(ViewAuthorRequest _request)
        {
            ViewAuthorResponse _Response = new ViewAuthorResponse();

            var author = await _context.Authors.Where(x => x.AuthorId == _request.AUTHOR_ID)
                .Include(x => x.AuthorBooks)
                    .ThenInclude(x => x.Book).FirstOrDefaultAsync();
            if (author != null)
            {
                _Response.AUTHOR_ID = author.AuthorId;
                _Response.AUTHOR_NAME = author.Name;
                _Response.AUTHOR_BORN = author.DateOfBirth;
                _Response.AUTHOR_DIED = author.DateOfDeath;
                _Response.AUTHORED_BOOKS = author.AuthorBooks.Select(ab => new AuthorBookDto
                {
                    ID = ab.AuthorBookId, 
                    BOOK_TITLE = ab.Book.Title, 
                    BOOK_ID = ab.Book.BookId, 
                    BOOK_GENRE = ab.Book.Genre, 
                    BOOK_PUB_DATE = ab.Book.PublicationDate,
                    AUTHOR_ID = ab.Author.AuthorId,
                    AUTHOR_NAME = ab.Author.Name
                }).ToList();
            }
            
            return _Response;
        }

        public async Task<DeleteAuthorResponse> QueryDeleteAuthor(DeleteAuthorRequest _Request)
        {
            DeleteAuthorResponse _Response = new();
            
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorId == _Request.AUTHOR_ID);
            if (author != null)
            {
                _Response.AUTHOR_ID = author.AuthorId;
                _Response.AUTHOR_NAME = author.Name;
                _Response.AUTHOR_BORN = author.DateOfBirth;
                _Response.AUTHOR_DIED = author.DateOfDeath;
            }

            return _Response;
        }
        public async Task<DeleteAuthorSubmitResponse> SubmitDeleteAuthor(DeleteAuthorSubmitRequest _Request)
        {
            DeleteAuthorSubmitResponse _Response = new();
            
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == _Request.AUTHOR_ID);
            
            if (author == null)
            {
                _Response.ERROR_MESSAGES.Add(new MessageListItem { MESSAGE = "Author with given AUTHOR_ID does not exist" });
                return _Response;
            }
            else
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                _Response.Successful = true;
                _Response.Message = "Author deleted successfully";
            }
            
            return _Response;
        }
        #endregion

        #region Book
        public async Task<List<BookDto>> QueryGetBooks()
        {
            List<BookDto> _Response = new List<BookDto>();

            _Response = await _context.Books.Select(x => new BookDto() { ID = x.BookId, TITLE = x.Title }).ToListAsync();

            return _Response;
        }
        public async Task<Data.LIB_DB.Book> GetBookById(decimal bookId)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.BookId == bookId);
        }
        public async Task<AllBooksResponse> QueryGetAllBooks(AllBooksRequest _Request)
        {
            AllBooksResponse _Response = new AllBooksResponse();
            
            var books = await _context.Books.ToListAsync();
            if (books != null)
            {
                foreach (var book in books)
                {
                    _Response.BOOKS.Add(new BookDto()
                    {
                        ID = book.BookId, 
                        TITLE = book.Title,
                        PUB_DATE = book.PublicationDate,
                        GENRE = book.Genre
                    });
                }
            }

            _Response.RESP_BOOK_ID = _Request.REQ_BOOK_ID;
            _Response.RESP_BOOK_TITLE = _Request.REQ_BOOK_TITLE;
            return _Response;
        }
        public async Task<AddBookSubmitResponse> InsertAddBook(AddBookSubmitRequest _Request)
        {
            AddBookSubmitResponse _Response = new AddBookSubmitResponse();

            try
            {
                var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == _Request.BOOK_ID);

                if (book != null)
                {
                    _Response.ERROR_MESSAGES.Add(new MessageListItem()
                        { MESSAGE = "Book with given BOOK_ID already exists" });
                    return _Response;
                }
                else if (!string.IsNullOrEmpty(_Request.BOOK_TITLE))
                {
                    // If BOOK_TITLE is provided, create a new Book
                    book = new Data.LIB_DB.Book()
                    {
                        Title = _Request.BOOK_TITLE, 
                        PublicationDate = (DateTime)_Request.BOOK_PUB_DATE, 
                        Genre = (Data.LIB_DB.Enum.BookGenre)_Request.BOOK_GENRE
                    };
                    _context.Books.Add(book);
                    await _context.SaveChangesAsync();
                    _Response.RESP_BOOK_ID = book.BookId;
                }
                else
                {
                    _Response.ERROR_MESSAGES.Add(new MessageListItem(){MESSAGE = "Book title is required"});
                }

                if (_Request.AUTHOR_ID != null)
                {
                    var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == _Request.AUTHOR_ID);
                    if (author == null)
                    {
                        _Response.ERROR_MESSAGES.Add(new MessageListItem() { MESSAGE = "Author with given AUTHOR_ID does not exist" });
                        return _Response;
                    }
                    // Add AuthorBook relationship
                    var authorBook = new Data.LIB_DB.AuthorBook()
                    {
                        Author = author,
                        AuthorId = author.AuthorId,
                        Book = book,
                        BookId = book.BookId
                    };
                    _context.AuthorBooks.Add(authorBook);
                    await _context.SaveChangesAsync();
                }
                else if (!string.IsNullOrEmpty(_Request.AUTHOR_NAME))
                {
                    var author = new Data.LIB_DB.Author()
                    {
                        Name = _Request.AUTHOR_NAME,
                        DateOfBirth = _Request.AUTHOR_BORN,
                        DateOfDeath = _Request.AUTHOR_DIED
                    };
                    _context.Authors.Add(author);
                    await _context.SaveChangesAsync();
                    
                    var authorBook = new Data.LIB_DB.AuthorBook()
                    {
                        Author = author,
                        AuthorId = author.AuthorId,
                        Book = book,
                        BookId = book.BookId
                    };

                    _context.AuthorBooks.Add(authorBook);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                _Response.ERROR_MESSAGES.Add(new MessageListItem() { MESSAGE = "Please enter Book" });
            }
            
            return _Response;
        }
        public async Task<IEnumerable<AuthorBookDto>> QueryGetBookAuthors(decimal bookId)
        {
            return await _context.AuthorBooks.Include(ab => ab.Author)
                .ThenInclude(b => b.AuthorBooks)
                .Where(ab => ab.BookId == bookId)
                .Select(ab => new AuthorBookDto()
                {
                    ID = ab.AuthorBookId,
                    AUTHOR_ID = ab.AuthorId,
                    BOOK_ID = ab.BookId,
                    AUTHOR_NAME = ab.Author.Name,
                    BOOK_TITLE = ab.Book.Title
                }).ToListAsync();
        }
        public async Task<IEnumerable<BookBorrowerDto>> QueryGetBorrowersByBook(decimal bookId)
        {
            return await _context.BookBorrows.Include(bb => bb.Borrower)
                .ThenInclude(bb => bb.BookBorrows)
                .Where(bb => bb.BookId == bookId)
                .Select(bb => new BookBorrowerDto()
                {
                    ID = bb.BookBorrowId,
                    BOOK_ID = bb.BookId,
                    BORROWER_ID = bb.BorrowerId,
                    BORROWER_NAME = bb.Borrower.Name,
                    BOOK_TITLE = bb.Book.Title
                })
                .ToListAsync();
        }

        public async Task<UpdateBookResponse> QueryUpdateBook(UpdateBookRequest _Request)
        {
            return await _context.Books.Where(b => b.BookId == _Request.BOOK_ID)
                .Select(x => new UpdateBookResponse()
                {
                    BOOK_ID = x.BookId,
                    BOOK_TITLE = x.Title,
                    BOOK_PUB_DATE = x.PublicationDate,
                    BOOK_GENRE = (BookGenre)x.Genre,
                    BOOK_AUTHORS = x.AuthorBooks.Select(ab => new AuthorDto() { ID = ab.AuthorId, NAME = ab.Author.Name }).ToList()
                })
                .FirstOrDefaultAsync();
        }
        public async Task<UpdateBookSubmitResponse> UpdateBook(UpdateBookSubmitRequest _Request)
        {
            UpdateBookSubmitResponse _Response = new UpdateBookSubmitResponse();
            var book = await _context.Books.Where(b => b.BookId == _Request.BOOK_ID)
                .FirstOrDefaultAsync();
            
            if (book != null)
            {
                // Update book properties
                if (_Request.BOOK_TITLE != null)
                {
                    book.Title = _Request.BOOK_TITLE;
                }
                if (_Request.BOOK_PUB_DATE != null)
                {
                    book.PublicationDate = (DateTime)_Request.BOOK_PUB_DATE;
                }
                if (_Request.BOOK_GENRE != null)
                {
                    book.Genre = (Data.LIB_DB.Enum.BookGenre)_Request.BOOK_GENRE;
                }
                
                // Save changes to the database
                await _context.SaveChangesAsync();
                _Response.RESP_BOOK_ID = _Request.REQ_BOOK_ID;
            }
            return _Response;
        }
        public async Task<BookTitleSearchResponse> QueryGetBookTitleSearch(BookTitleSearchRequest _Request)
        {
            BookTitleSearchResponse _Response = new BookTitleSearchResponse();
            var books = await _context.Books.Where(b => b.Title.ToLower().Contains(_Request.SEARCH_TEXT.ToLower()) ||
                                                        _Request.SEARCH_TEXT.ToLower().Contains(b.Title.ToLower()))
                                                        .ToListAsync();
            
            if (books != null)
            {
                // Process the books
                _Response.BOOKS = books.Select(b => new AutoCompleteItem()
                {
                    value = b.BookId.ToString(), 
                    label = b.Title
                }).ToList();
            }
            return _Response;
        }
        public async Task<ViewBookResponse> QueryGetViewBook(ViewBookRequest _Request)
        {
            ViewBookResponse _Response = new ViewBookResponse();
            var book = await _context.Books.Where(b => b.BookId == _Request.BOOK_ID)
                .Include(b => b.AuthorBooks)
                .ThenInclude(ab => ab.Author)
                .FirstOrDefaultAsync();
            if (book != null)
            {
                // Process the book
                _Response.BOOK_ID = book.BookId;
                _Response.BOOK_TITLE = book.Title;
                _Response.BOOK_PUB_DATE = book.PublicationDate;
                _Response.BOOK_GENRE = book.Genre;
                _Response.BOOK_AUTHORS = book.AuthorBooks.Select(ab => new AuthorBookDto()
                {
                    ID = ab.AuthorBookId, 
                    AUTHOR_ID = ab.Author.AuthorId, 
                    AUTHOR_NAME = ab.Author.Name, 
                    BOOK_ID = ab.Book.BookId,
                    BOOK_TITLE = ab.Book.Title
                }).ToList();
            }
            return _Response;
        }

        public async Task<DeleteBookResponse> QueryDeleteBook(DeleteBookRequest _Request)
        {
            DeleteBookResponse _Response = new();

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                var book = await _context.Books.FirstOrDefaultAsync(x => x.BookId == _Request.BOOK_ID);

                _Response.BOOK_ID = book.BookId;
                _Response.BOOK_TITLE = book.Title;
                _Response.BOOK_GENRE = book.Genre;
                _Response.BOOK_PUB_DATE = book.PublicationDate;
            }

            return _Response;
        }
        public async Task<DeleteBookSubmitResponse> SubmitDeleteBook(DeleteBookSubmitRequest _Request)
        {
            DeleteBookSubmitResponse _Response = new();
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == _Request.BOOK_ID);

            if (book == null)
            {
                _Response.ERROR_MESSAGES.Add(new MessageListItem { MESSAGE = "Book with given BOOK_ID does not exist" });
                return _Response;
            }
            else
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                _Response.Successful = true;
                _Response.Message = "Book deleted successfully";
            }

            return _Response;
        }
        #endregion

        #region Borrower

        public async Task<List<BorrowerDto>> QueryGetBorrowers()
        {
            List<BorrowerDto> _Response = new List<BorrowerDto>();

            _Response = await _context.Borrowers.Select(x => new BorrowerDto() { ID = x.BorrowerId, NAME = x.Name }).ToListAsync();

            return _Response;
        }
        public async Task<Data.LIB_DB.Borrower> GetBorrowerById(decimal borrowerId)
        {
            return await _context.Borrowers.FirstOrDefaultAsync(b => b.BorrowerId == borrowerId);
        }
        public async Task<AllBorrowersResponse> QueryGetAllBorrowers(AllBorrowersRequest _Request)
        {
            AllBorrowersResponse _Response = new AllBorrowersResponse();

            var borrowers =  _context.Borrowers.ToList();
            if (borrowers != null)
            {
                foreach (var borrower in borrowers)
                {
                    _Response.BORROWERS.Add(new BorrowerDto()
                    {
                        ID = borrower.BorrowerId,
                        NAME = borrower.Name,
                    });
                }
            }

            _Response.RESP_BORROWER_ID = _Request.REQ_BORROWER_ID;
            _Response.RESP_BORROWER_NAME = _Request.REQ_BORROWER_NAME;
            return _Response;
        }
        public async Task<AddBorrowerSubmitResponse> InsertAddBorrower(AddBorrowerSubmitRequest _Request)
        {
            AddBorrowerSubmitResponse _Response = new AddBorrowerSubmitResponse();
            
            try
            {
                var borrower = await _context.Borrowers.FirstOrDefaultAsync(b => b.BorrowerId == _Request.BORROWER_ID);

                if (borrower != null)
                {
                    _Response.ERROR_MESSAGES.Add(new MessageListItem(){MESSAGE = "Borrower with BORROWER_ID already exists"});
                    return _Response;
                }
                else if (!string.IsNullOrEmpty(_Request.BORROWER_NAME))
                {
                    borrower = new Data.LIB_DB.Borrower()
                    {
                        Name = _Request.BORROWER_NAME
                    };
                    _context.Borrowers.Add(borrower);
                    await _context.SaveChangesAsync();
                    _Response.RESP_BORROWER_ID = borrower.BorrowerId;
                }
                else
                {
                    _Response.ERROR_MESSAGES.Add(new MessageListItem() { MESSAGE = "Borrower name is required" });
                }

                if (_Request.BOOK_ID != null)
                {
                    var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == _Request.BOOK_ID);
                    var bookBorrow = new Data.LIB_DB.BookBorrower()
                    {
                        Book = book,
                        BookId = book.BookId,
                        Borrower = borrower,
                        BorrowerId = borrower.BorrowerId,
                        BorrowedDate = DateTime.Now,
                    };
                    _context.BookBorrows.Add(bookBorrow);
                    _context.SaveChangesAsync();
                }
                else if (!string.IsNullOrEmpty(_Request.BOOK_TITLE))
                {
                    var book = new Data.LIB_DB.Book()
                    {
                        Title = _Request.BOOK_TITLE,
                        PublicationDate = (DateTime)_Request.BOOK_PUB_DATE,
                        Genre = (Data.LIB_DB.Enum.BookGenre)_Request.BOOK_GENRE
                    };
                    _context.Books.Add(book);
                    _context.SaveChangesAsync();
                    
                    var bookBorrow = new Data.LIB_DB.BookBorrower()
                    {
                        Book = book,
                        BookId = book.BookId,
                        Borrower = borrower,
                        BorrowerId = borrower.BorrowerId,
                        BorrowedDate = DateTime.Now,
                    };
                    _context.BookBorrows.Add(bookBorrow);
                    _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                _Response.ERROR_MESSAGES.Add(new MessageListItem(){MESSAGE = "Borrower name is required"});
            }

            return _Response;
        }
        public async Task<IEnumerable<BookBorrowerDto>> QueryGetBooksByBorrower(decimal borrowerId)
        {
            return await _context.BookBorrows.Include(bb => bb.Book)
                .ThenInclude(bb => bb.BookBorrows)
                .Where(bb => bb.BorrowerId == borrowerId)
                .Select(bb => new BookBorrowerDto()
                {
                    ID = bb.BookBorrowId,
                    BORROWER_ID = bb.BorrowerId,
                    BOOK_ID = bb.BookId,
                    BORROWER_NAME = bb.Borrower.Name,
                    BOOK_TITLE = bb.Book.Title
                })
                .ToListAsync();
        }

        public async Task<UpdateBorrowerResponse> QueryUpdateBorrower(UpdateBorrowerRequest _Request)
        {
            UpdateBorrowerResponse _Response = new();

            
            
            return _Response;
        }
        public async Task<UpdateBorrowerSubmitResponse> UpdateBorrower(UpdateBorrowerSubmitRequest _Request)
        {
            UpdateBorrowerSubmitResponse _Response = new UpdateBorrowerSubmitResponse();
            var borrower = await _context.Borrowers.FirstOrDefaultAsync(b => b.BorrowerId == _Request.BORROWER_ID);
            
            if (_Request.BORROWER_NAME != null)
            {
                borrower.Name = _Request.BORROWER_NAME;
            }
            
            await _context.SaveChangesAsync();
            _Response.RESP_BORROWER_ID = borrower.BorrowerId;
            
            return _Response;
        }
        public async Task<BorrowerNameSearchResponse> QueryGetBorrowerNameSearch(BorrowerNameSearchRequest _Request)
        {
            BorrowerNameSearchResponse _Response = new BorrowerNameSearchResponse();
            
                var borrowers = await _context.Borrowers
                    .Where(b => b.Name.ToLower().Contains(_Request.SEARCH_TEXT.ToLower()) || _Request.SEARCH_TEXT.ToLower().Contains(b.Name.ToLower()))
                    .ToListAsync();

                if (borrowers != null)
                {
                    _Response.BORROWERS = borrowers.Select(x => new AutoCompleteItem()
                    {
                        value = x.BorrowerId.ToString(), 
                        label = x.Name
                    }).ToList();
                }
            return _Response;
        }
        public async Task<ViewBorrowerResponse> QueryGetViewBorrower(ViewBorrowerRequest _Request)
        {
            ViewBorrowerResponse _Response = new ViewBorrowerResponse();
            var borrower = await _context.Borrowers.Where(x => x.BorrowerId == _Request.BORROWER_ID).FirstOrDefaultAsync();
            if (borrower != null)
            {
                _Response.BORROWER_ID = borrower.BorrowerId;
                _Response.BORROWER_NAME = borrower.Name;
                _Response.BOOK_BORROWS = borrower.BookBorrows.Select(bb => new BookBorrowerDto { BOOK_TITLE = bb.Book.Title, BOOK_ID = bb.Book.BookId, BORROWED_DATE = bb.BorrowedDate, RETURNED_DATE = bb.ReturnedDate}).ToList();
            }

            return _Response;
        }
        public async Task<DeleteBorrowerResponse> QueryDeleteBorrower(DeleteBorrowerRequest _Request)
        {
            DeleteBorrowerResponse _Response = new();

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                var borrower = await _context.Borrowers.FirstOrDefaultAsync(x => x.BorrowerId == _Request.BORROWER_ID);
                if (borrower == null)
                {
                    _Response.ERROR_MESSAGES.Add(new MessageListItem { MESSAGE = "Borrower with given BORROWER_ID does not exist" });
                    return _Response;
                }
                else
                {
                    _Response.BORROWER_ID = _Request.BORROWER_ID;
                    _Response.BORROWER_NAME = borrower.Name;
                }
            }
            return _Response;
        }
        public async Task<DeleteBorrowerSubmitResponse> SubmitDeleteBorrower(DeleteBorrowerSubmitRequest _Request)
        {
            DeleteBorrowerSubmitResponse _Response = new();
            var borrower = await _context.Borrowers.FirstOrDefaultAsync(b => b.BorrowerId == _Request.BORROWER_ID);
            if (borrower == null)
            {
                _Response.ERROR_MESSAGES.Add(new MessageListItem { MESSAGE = "Borrower with given BORROWER_ID does not exist" });
                return _Response;
            }
            else
            {
                _context.Borrowers.Remove(borrower);
                await _context.SaveChangesAsync();
                _Response.Successful = true;
                _Response.Message = "Borrower deleted successfully";
            }
            
            return _Response;
        }
        #endregion

        #region AuthorBook
        public async Task<AddAuthorBookSubmitResponse> InsertAddAuthorBook(AddAuthorBookSubmitRequest _Request)
        {
            AddAuthorBookSubmitResponse _Response = new AddAuthorBookSubmitResponse();
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == _Request.AUTHOR_ID);
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == _Request.BOOK_ID);
            var authorBook = new Data.LIB_DB.AuthorBook()
            {
                Author = author, 
                AuthorId = author.AuthorId, 
                Book = book, 
                BookId = book.BookId
            };
            _context.AuthorBooks.Add(authorBook);
            await _context.SaveChangesAsync();
            
            
            _Response.RESP_AUTHOR_BOOK_ID = authorBook.AuthorBookId;
            
            return _Response;
        }
        public async Task<UpdateAuthorBookSubmitResponse> UpdateAuthorBook(UpdateAuthorBookSubmitRequest _Request)
        {
            UpdateAuthorBookSubmitResponse _Response = new UpdateAuthorBookSubmitResponse();

            var authorBook = await _context.AuthorBooks.Where(ab => ab.AuthorBookId == _Request.AUTHOR_BOOK_ID)
                .FirstOrDefaultAsync();
            if (authorBook != null)
            {
                if (_Request.AUTHOR_NAME != null)
                {
                    authorBook.Author.Name = _Request.AUTHOR_NAME;
                }

                if (_Request.AUTHOR_BORN != null)
                {
                    authorBook.Author.DateOfBirth = _Request.AUTHOR_BORN;
                }

                if (_Request.AUTHOR_DIED != null)
                {
                    authorBook.Author.DateOfDeath = _Request.AUTHOR_DIED;
                }

                if (_Request.BOOK_TITLE != null)
                {
                    authorBook.Book.Title = _Request.BOOK_TITLE;
                }
                if (_Request.BOOK_PUB_DATE != null)
                {
                    authorBook.Book.PublicationDate = (DateTime)_Request.BOOK_PUB_DATE;
                }
                if (_Request.BOOK_GENRE != null)
                {
                    authorBook.Book.Genre = (Data.LIB_DB.Enum.BookGenre)_Request.BOOK_GENRE;
                }

            await _context.SaveChangesAsync();
                _Response.RESP_AUTHOR_BOOK_ID = authorBook.AuthorBookId;
            }
            
            return _Response;
        }

        public async Task<DeleteAuthorBookResponse> QueryDeleteAuthorBook(DeleteAuthorBookRequest _Request)
              {
                  DeleteAuthorBookResponse _Response = new();
                  _Response.AUTHOR_ID = _Request.AUTHOR_ID;
                  _Response.BOOK_ID = _Request.BOOK_ID;
                  _Response.AUTHOR_BOOK_ID = await _context.AuthorBooks
                      .Where(ab => ab.AuthorId == _Request.AUTHOR_ID && ab.BookId == _Request.BOOK_ID)
                      .Select(ab=> ab.AuthorBookId).FirstOrDefaultAsync();
      
                  return _Response;
              }
        public async Task<DeleteAuthorBookSubmitResponse> SubmitDeleteAuthorBook(DeleteAuthorBookSubmitRequest _Request)
        {
            DeleteAuthorBookSubmitResponse _Response = new DeleteAuthorBookSubmitResponse();
            var authorBook =
                await _context.AuthorBooks.FirstOrDefaultAsync(ab => ab.AuthorBookId == _Request.AUTHOR_BOOK_ID);
            if (authorBook != null)
            {
                _context.AuthorBooks.Remove(authorBook);
                await _context.SaveChangesAsync();
                _Response.Successful = true;
                _Response.Message = "Author book deleted successfully";
            }

            return _Response;
        }
        #endregion

        #region BookBorrower

        public async Task<AddBookBorrowerSubmitResponse> InsertAddBookBorrower(AddBookBorrowerSubmitRequest _Request)
        {
            AddBookBorrowerSubmitResponse _Response = new AddBookBorrowerSubmitResponse();

            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == _Request.BOOK_ID);
            var borrower = await _context.Borrowers.FirstOrDefaultAsync(b => b.BorrowerId == _Request.BORROWER_ID);
            if (book == null)
            {
                book = new Data.LIB_DB.Book()
                {
                    Title = _Request.BOOK_TITLE,
                    Genre = (Data.LIB_DB.Enum.BookGenre)_Request.BOOK_GENRE,
                    PublicationDate = (DateTime)_Request.BOOK_PUB_DATE
                };
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
            }

            if (borrower == null)
            {
                borrower = new Data.LIB_DB.Borrower()
                {
                    Name = _Request.BORROWER_NAME
                };
                _context.Borrowers.Add(borrower);
                await _context.SaveChangesAsync();
            }
            var bookBorrower = new Data.LIB_DB.BookBorrower()
            {
                Book = book,
                BookId = book.BookId, 
                Borrower = borrower,
                BorrowerId = borrower.BorrowerId
            };
            _context.BookBorrows.Add(bookBorrower);
            await _context.SaveChangesAsync();
            
            return _Response;
        }

        public async Task<UpdateBookBorrowerResponse> QueryUpdateBookBorrower(UpdateBookBorrowerRequest _Request)
        {
            UpdateBookBorrowerResponse _Response = new();

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                var borrowedBook = await _context.BookBorrows.FirstOrDefaultAsync(x =>
                                 x.BookId == _Request.BOOK_ID && x.BorrowerId == _Request.BORROWER_ID && x.BorrowedDate != null && x.ReturnedDate == null);
                _Response.BOOK_BORROWER_ID = borrowedBook.BookBorrowId;
                _Response.BORROWER_ID = _Request.BORROWER_ID;
                _Response.BOOK_ID = _Request.BOOK_ID;
                _Response.BORROWED_DATE = borrowedBook.BorrowedDate;
            }

            return _Response;
        }
        public async Task<UpdateBookBorrowerSubmitResponse> UpdateBookBorrower(UpdateBookBorrowerSubmitRequest _Request)
        {
            UpdateBookBorrowerSubmitResponse _Response = new UpdateBookBorrowerSubmitResponse();
            var bookBorrower =
                await _context.BookBorrows.FirstOrDefaultAsync(bb => bb.BookBorrowId == _Request.BOOK_BORROWER_ID);
            if (bookBorrower != null)
            {
                bookBorrower.ReturnedDate = DateTime.Now;
                if (bookBorrower.ReturnedDate > bookBorrower.BorrowedDate)
                {
                    bookBorrower.IsReturned = true;
                }

                _context.BookBorrows.Update(bookBorrower);
                await _context.SaveChangesAsync();
            }

            return _Response;
        }

        public async Task<DeleteBookBorrowerResponse> QueryDeleteBookBorrower(DeleteBookBorrowerRequest _Request)
        {
            DeleteBookBorrowerResponse _Response = new();
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                var bookBorrower = await _context.BookBorrows.FirstOrDefaultAsync(x =>
                    x.BookId == _Request.BOOK_ID && x.BorrowerId == _Request.BORROWER_ID);
                _Response.BOOK_BORROWER_ID = bookBorrower.BookBorrowId;
                _Response.BOOK_ID = _Request.BOOK_ID;
                _Response.BORROWER_ID = _Request.BORROWER_ID;
            }

            return _Response;
        }
        public async Task<DeleteBookBorrowerSubmitResponse> SubmitDeleteBookBorrower(DeleteBookBorrowerSubmitRequest _Request)
        {
            DeleteBookBorrowerSubmitResponse _Response = new();
            
            var bookBorrower = await _context.BookBorrows.FirstOrDefaultAsync(bb => bb.BookBorrowId == _Request.BOOK_BORROWER_ID);
            if (bookBorrower != null)
            {
                _context.BookBorrows.Remove(bookBorrower);
                await _context.SaveChangesAsync();
                _Response.Successful = true;
                _Response.Message = "Book borrower deleted successfully";
            }
            return _Response;
        }
        #endregion
        
        #region Validation

        public async Task<List<MessageListItem>> ValidateInsertAuthor(AddAuthorSubmitRequest _Request)
        {
            List<MessageListItem> _Response = new();

            var _dup_authorId = await (from author in _context.Authors
                where author.AuthorId == _Request.AUTHOR_ID
                select author.AuthorId).FirstOrDefaultAsync();
            if (_dup_authorId != 0)
            {
                _Response.Add(new MessageListItem(){CDE = "CRITICAL", MESSAGE = "Author with this AuthorID already exists"});
            }

            return _Response;
        }

        public async Task<List<MessageListItem>> ValidateInsertBook(AddBookSubmitRequest _Request)
        {
            List<MessageListItem> _Response = new();
            var _dup_bookId = await(from book in _context.Books 
                where book.BookId == _Request.BOOK_ID 
                select book.BookId).FirstOrDefaultAsync();
            if (_dup_bookId != 0)
            {
                _Response.Add(new MessageListItem() { CDE = "CRITICAL", MESSAGE = "Book with this BookID already exists" });
            }

            return _Response;
        }

        public async Task<List<MessageListItem>> ValidateInsertBorrower(AddBorrowerSubmitRequest _Request)
        {
            List<MessageListItem> _Response = new();
            var _dup_borrowerID = await (from borrower in _context.Borrowers
                where borrower.BorrowerId == _Request.BORROWER_ID
                select borrower.BorrowerId).FirstOrDefaultAsync();
            if (_dup_borrowerID != 0)
            {
                _Response.Add(new MessageListItem() { CDE = "CRITICAL", MESSAGE = "Borrower with this BorrowerID already exists" });
            }

            return _Response;
        }
        #endregion
    }
}
