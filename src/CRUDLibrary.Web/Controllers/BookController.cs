using CRUDLibrary.Data.LIB_DB;
using CRUDLibrary.Domain.Interfaces;
using CRUDLibrary.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRUDLibrary.Web.Controllers
{
    public class BookController : Controller
    {
        #region Services
        private  IBook BookService;
        private  IAuthorBook ABService;
        private  IBookBorrower BBService;
        #endregion
        
        #region Constructor
        public BookController(IConfiguration configuration, IBook bookService, IAuthorBook aBService, IBookBorrower bBService)
                {
                    BookService = bookService;
                    ABService = aBService;
                    BBService = bBService;
                }
        #endregion

        #region GET
        //------------------------------------
        public async Task<IActionResult> Index()
        {
            AllBooksRequest _Request = new();
            AllBooksResponse _Response = new();
        
            _Response = await BookService.GetAllBooks(_Request);
                    
            return View(_Response);
        }
        //------------------------------------
        public async Task<IActionResult> View(int id)
        {
            ViewBookResponse _Response = new();
        
            try
            {
                ViewBookRequest _Request = new ViewBookRequest(){BOOK_ID = id};
                _Response = await BookService.GetViewBook(_Request);
                        
            }
            catch (Exception ex)
            {
                var msg = new List<MessageListItem>() { new MessageListItem { MESSAGE = "Book cannot be found." } };
                _Response.ERROR_MESSAGES.AddRange(msg);
                return RedirectToAction("Index", _Response);
            }
                    
            return View(_Response);
        }
        //------------------------------------
        public async Task<IActionResult> Add()
        {
            AddBookRequest _Request = new();
            AddBookResponse _Response = new();

            try
            {
                _Response = await BookService.GetAddBook(_Request);
            }
            catch (Exception ex)
            {
                var msgs =new MessageListItem(){ MESSAGE = "Error adding Book." };
                _Response.ERROR_MESSAGES.Add(msgs);
            }

            
            return View(_Response); 
        }
        //------------------------------------
        public async Task<IActionResult> Update(int id) 
        {
            UpdateBookResponse _Response = new();

            try
            {
                UpdateBookRequest _Request = new UpdateBookRequest() { BOOK_ID = id };
                _Response = await BookService.GetUpdateBook(_Request);
            }
            catch
            {
                var msgs = new List<MessageListItem>() { new MessageListItem(){ MESSAGE = "Book does not exist." }};
                _Response.ERROR_MESSAGES.AddRange(msgs);
                return RedirectToAction("Index", _Response);
            }
            
            return View(_Response);
        }
        //------------------------------------
        public async Task<IActionResult> AddBorrow(int id)
        {
            AddBookBorrowerResponse _Response = new();

            try
            {
                AddBookBorrowerRequest _Request = new AddBookBorrowerRequest() { BOOK_ID = id };
                _Response = await BBService.GetAddBookBorrower(_Request);
            }
            catch
            {
                var msgs = new List<MessageListItem>() { new MessageListItem() { MESSAGE = "Unable to find Book." } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }

            ViewBag.BorrowerId = await BBService.GetBorrowers();
            return View(_Response);
        }
        //------------------------------------
        public async Task<IActionResult> ReturnBorrow(int Id, int BorrowerId)
        {
            UpdateBookBorrowerResponse _Response = new();
         
            try
            {
                UpdateBookBorrowerRequest _Request = new UpdateBookBorrowerRequest(){ BOOK_ID = Id, BORROWER_ID = BorrowerId };
                _Response = await BBService.GetUpdateBookBorrower(_Request);
            }
            catch (Exception ex)
            {
                var msgs = new List<MessageListItem>() { new MessageListItem() { MESSAGE = "Unable to find Book or Borrower." } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }
                             
            return View(_Response);
        }
        //------------------------------------
        public async Task<IActionResult> AddAuthor(int id)
                {
                    AddAuthorBookResponse _Response = new();
                    try
                    {
                        AddAuthorBookRequest _Request = new AddAuthorBookRequest() { BOOK_ID = id.ToString() };
                    }
                    catch
                    {
                        var msgs = new List<MessageListItem> { new MessageListItem() { MESSAGE = "Unable to find Book or Author." } };
                        _Response.ERROR_MESSAGES.AddRange(msgs);
                    }
        
                    ViewBag.AuthorId = await ABService.GetAuthors();
                    return View(_Response);
                }
        //------------------------------------
        public async Task<IActionResult> DeleteAuthor(int Id, int AuthorId)
        {
            DeleteAuthorBookResponse _Response = new();
            try
            {
                DeleteAuthorBookRequest _Request = new DeleteAuthorBookRequest() { AUTHOR_ID = AuthorId, BOOK_ID = Id };
                _Response = await ABService.GetDeleteAuthorBook(_Request);
            }
            catch
            {
                var msgs = new List<MessageListItem> { new MessageListItem() { MESSAGE = "Unable to find Book or Author." } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }
        
            return View(_Response);
        }
        //------------------------------------
        public async Task<IActionResult> Delete(int id)
        {
            DeleteBookResponse _Response = new();
        
            try
            {
                DeleteBookRequest _Request = new DeleteBookRequest() { BOOK_ID = id.ToString() };
                _Response = await BookService.GetDeleteBook(_Request);
            }
            catch
            {
                var msgs = new List<MessageListItem> { new MessageListItem() { MESSAGE = "Unable to find Book." } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }
        
            return View(_Response);
        }
        //------------------------------------
        
        #endregion
        
        #region POST
        //------------------------------------
        [HttpPost]
        public async Task<JsonResult> Add([FromBody] AddBookSubmitRequest _Request)
        {
            AddBookSubmitResponse _Response = new();

            try
            {
                _Response = await BookService.SubmitAddBook(_Request);
                var msgs = new MessageListItem() { MESSAGE = "Successfully added Book."  };
                _Response.SUCCESS_MESSAGES.Add(msgs);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                var msgs =  new MessageListItem(){ MESSAGE = "Error adding Book" };
                _Response.ERROR_MESSAGES.Add(msgs);
            }
            
            return Json(_Response);
        }
        //------------------------------------
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateBookSubmitRequest _Request)
        {
            UpdateBookSubmitResponse _Response = new();

            try
            {
                _Response = await BookService.SubmitUpdateBook(_Request);
                var msgs = new List<MessageListItem>() { new MessageListItem() { MESSAGE = "Successfully updated Book." } };
                _Response.SUCCESS_MESSAGES.AddRange(msgs);
            }
            catch
            {
                var msgs = new List<MessageListItem>() { new MessageListItem(){ MESSAGE = "Error updating Book." }};
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }
            
            return RedirectToAction("View", _Response);
        }
        //------------------------------------
        [HttpPost]
        public async Task<IActionResult> AddBorrow([FromBody] AddBookBorrowerSubmitRequest _Request)
        {
            AddBookBorrowerSubmitResponse _Response = new();

            try 
            {
                _Response = await BBService.SubmitAddBookBorrower(_Request);
                var msgs = new List<MessageListItem>() { new MessageListItem() { MESSAGE = "Successfully added Borrower to Book." } };
                _Response.SUCCESS_MESSAGES.AddRange(msgs);
            }
            catch
            {
                var msgs = new List<MessageListItem>() { new MessageListItem(){ MESSAGE = "Error adding Borrower to Book." }};
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }
            return RedirectToAction("View", _Response);
        }
        //------------------------------------
        [HttpPost, ActionName("ReturnBorrow")]
        public async Task<IActionResult> ReturnBorrowConfirmed([FromBody] UpdateBookBorrowerSubmitRequest _Request)
        {
            UpdateBookBorrowerSubmitResponse _Response = new();
        
            try
            {
                _Response = await BBService.SubmitUpdateBookBorrower(_Request);
                var msgs = new List<MessageListItem>() { new MessageListItem() { MESSAGE = "Successfully returned Book." } };
                _Response.SUCCESS_MESSAGES.AddRange(msgs); 
            }
            catch (Exception ex) 
            { 
                var msgs = new List<MessageListItem>() { new MessageListItem() { MESSAGE = "Unable to return Book." } }; 
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }
            return RedirectToAction("View", _Response);
        }
        //------------------------------------
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorBookSubmitRequest _Request)
        {
            AddAuthorBookSubmitResponse _Response = new();
            try
            {
                _Response = await ABService.SubmitAddAuthorBook(_Request);
                var msgs = new List<MessageListItem>() { new MessageListItem() { MESSAGE = "Successfully added and Author to this Book." } };
                _Response.SUCCESS_MESSAGES.AddRange(msgs); 
            }
            catch
            { 
                var msgs = new List<MessageListItem>() { new MessageListItem() { MESSAGE = "Unable to add an Author to this Book." } }; 
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }
                    
            return RedirectToAction("View", _Response);
        }
        //-------------------------------------
        [HttpPost, ActionName("DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthorConfirmed([FromBody] DeleteAuthorBookSubmitRequest _Request)
        {
            DeleteAuthorBookSubmitResponse _Response = new();
            try
            {
                _Response = await ABService.SubmitDeleteAuthorBook(_Request);
                var msgs = new List<MessageListItem> { new MessageListItem() { MESSAGE = "Successfully deleted Author from Book." } };
                _Response.SUCCESS_MESSAGES.AddRange(msgs);
            }
            catch
            {
                var msgs = new List<MessageListItem> { new MessageListItem() { MESSAGE = "Unable to delete Author from Book." } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }
                    
            return RedirectToAction("View", _Response);
        }
        //------------------------------------
        [HttpPost, ActionName("Delete")]
        public async Task<JsonResult> DeleteConfirmed([FromBody] DeleteBookSubmitRequest _Request)
        {
            DeleteBookSubmitResponse _Response = new();
            MessageListItem msgs = new();
            try
            {
                _Response = await BookService.SubmitDeleteBook(_Request);
                msgs.MESSAGE = "Successfully deleted Book.";
                _Response.SUCCESS_MESSAGES.Add(msgs);
            }
            catch
            {
                msgs.MESSAGE = "Unable to delete Book.";
                _Response.ERROR_MESSAGES.Add(msgs);
            }
        
            return Json(_Response);
        }
        //------------------------------------
        
        
        #endregion
        

    }
}
