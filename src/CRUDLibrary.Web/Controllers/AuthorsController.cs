using CRUDLibrary.Domain.Interfaces;
using CRUDLibrary.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDLibrary.Web.Controllers
{
    public class AuthorController : Controller
    {
        #region Services

        private IAuthor AuthorService;
        private IAuthorBook ABService;

        #endregion

        #region Constructor

        public AuthorController(IConfiguration configuration, IAuthor authorService, IAuthorBook aBService)
                {
                    AuthorService = authorService;
                    ABService = aBService;
                }

        #endregion
        
        #region GET
        //------------------------------------
        public async Task<IActionResult> Index()
        {
            AllAuthorsRequest _Request = new();
            AllAuthorsResponse _Response = new();

            _Response = await AuthorService.GetAllAuthors(_Request);
            
            return View(_Response);
        }
        //------------------------------------
        public async Task<IActionResult> View(int id)
        {
            
            ViewAuthorResponse _Response = new();
            MessageListItem msgs = new();
            try
            {
                ViewAuthorRequest _Request = new ViewAuthorRequest(){AUTHOR_ID = id};
                _Response = await AuthorService.GetViewAuthor(_Request);
            }
            catch (Exception ex)
            {
                msgs.MESSAGE = "Author cannot be found.";
                _Response.ERROR_MESSAGES.Add(msgs);
                return RedirectToAction("Index", _Response);
            }
            
            return View(_Response);
        }
        //------------------------------------
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddAuthorRequest _Request = new();
            AddAuthorResponse _Response = new();
            
            return View(_Response);
        }
        //------------------------------------
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            UpdateAuthorResponse _Response = new();
            
            try
            {
                UpdateAuthorRequest _Request = new UpdateAuthorRequest(){AUTHOR_ID = id.ToString()};
                _Response = await AuthorService.GetUpdateAuthor(_Request);
            }
            catch (Exception ex)
            {
                var msgs = new List<MessageListItem>() { new MessageListItem(){ MESSAGE = "Error updating Author." }};
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }
            
            return View(_Response);
        }
        //------------------------------------
        [HttpGet]
        public async Task<IActionResult> AddBook(int id)
        {
            AddAuthorBookResponse _Response = new();
            
            try
            {
                AddAuthorBookRequest _Request = new AddAuthorBookRequest(){AUTHOR_ID = id.ToString()};
                _Response = await ABService.GetAddAuthorBook(_Request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                var msgs = new MessageListItem(){ MESSAGE = "Error adding Book to Author" };
                _Response.ERROR_MESSAGES.Add(msgs);
            }
        
            //Set list of existing books to use in Select list in view 
            ViewBag.BookId = await ABService.GetBooks();
            return View(_Response);
        }
        //------------------------------------
        [HttpGet]
        public async Task<IActionResult> DeleteBook(int Id, int BookId)
        {
            DeleteAuthorBookResponse _Response = new();

            try
            {
                DeleteAuthorBookRequest _Request = new() { AUTHOR_ID = Id, BOOK_ID = BookId };
                _Response = await ABService.GetDeleteAuthorBook(_Request);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Unable to delete Author from Book" } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }
            
            return View(_Response);
        }
        //------------------------------------
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteAuthorResponse _Response = new();
            MessageListItem msgs = new();
            try
            {
                _Response = await AuthorService.GetDeleteAuthor(new DeleteAuthorRequest() { AUTHOR_ID = id.ToString() });
            }
            catch (Exception ex) 
            {
                msgs.MESSAGE = "Error deleting Author.";
                _Response.ERROR_MESSAGES.Add(msgs);
            }
        
            return View(_Response);
        }
        //------------------------------------
         #endregion
         
        #region POST
        //------------------------------------
        [HttpPost]
        public async Task<JsonResult> Add([FromBody] AddAuthorSubmitRequest _Request)
        {
            AddAuthorSubmitResponse _Response = new AddAuthorSubmitResponse();
            MessageListItem msgs = new();
            
            try
            { 
                _Response = await AuthorService.SubmitAddAuthor(_Request);
               msgs.MESSAGE = "Successfully added Author.";
                _Response.SUCCESS_MESSAGES.Add(msgs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                msgs.MESSAGE = "Error adding Author.";
                _Response.ERROR_MESSAGES.Add(msgs);
            }

            return Json(_Response);
        }
        //------------------------------------
        [HttpPost]
        public async Task<JsonResult> Update([FromBody] UpdateAuthorSubmitRequest _Request)
        {
            UpdateAuthorSubmitResponse _Response = new();
            
            MessageListItem msgs = new();
            try
            {
                _Response = await AuthorService.SubmitUpdateAuthor(_Request);
                msgs.MESSAGE = "Successfully updated Author.";
                _Response.SUCCESS_MESSAGES.Add(msgs);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                msgs.MESSAGE = "Error updating Author.";
                _Response.ERROR_MESSAGES.Add(msgs);
            }
            return Json(_Response);
        }
        //------------------------------------
        [HttpPost]
        public async Task<JsonResult> AddBook([FromBody] AddAuthorBookSubmitRequest _Request)
        {
            AddAuthorBookSubmitResponse _Response = new();
            MessageListItem msgs = new();
            try
            {
                _Response = await ABService.SubmitAddAuthorBook(_Request);
                msgs.MESSAGE = "Successfully added Book to Author.";
                _Response.SUCCESS_MESSAGES.Add(msgs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                msgs.MESSAGE = "Error adding Book to Author";
                _Response.ERROR_MESSAGES.Add(msgs);
            }

            //Set list of existing books to use in Select list in view 
            ViewBag.BookId = await ABService.GetBooks();
            return Json(_Response);
        }
        //------------------------------------
        [HttpPost]
        public async Task<JsonResult> DeleteBook([FromBody] DeleteAuthorBookSubmitRequest _Request)
        {
            DeleteAuthorBookSubmitResponse _Response = new();
            MessageListItem msgs = new();
            
            try
            {
                _Response = await ABService.SubmitDeleteAuthorBook(_Request);
                msgs.MESSAGE ="Successfully removed Book from Author.";
                _Response.SUCCESS_MESSAGES.Add(msgs);
            }
            catch (Exception ex)
            {
                msgs.MESSAGE ="Error removing Book from Author";
                _Response.ERROR_MESSAGES.Add(msgs);
            }
            return Json(_Response);
        }
        //------------------------------------
        [HttpPost, ActionName("Delete")]
        public async Task<JsonResult> DeleteConfirmed([FromBody] DeleteAuthorSubmitRequest _Request)
        {
            DeleteAuthorSubmitResponse _Response = new();
            MessageListItem msgs = new();
            try
            {
                _Response = await AuthorService.SubmitDeleteAuthor(_Request);
               msgs.MESSAGE = "Successfully deleted Author.";
                _Response.SUCCESS_MESSAGES.Add(msgs);
            }
            catch (Exception ex)
            {
                msgs.MESSAGE = "Error deleting Author.";
                _Response.ERROR_MESSAGES.Add(msgs);
            }
            
            return Json(_Response);
        }
        //------------------------------------
        #endregion
    }
}
