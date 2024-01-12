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
            
            try
            {
                ViewAuthorRequest _Request = new ViewAuthorRequest(){AUTHOR_ID = id};
                _Response = await AuthorService.GetViewAuthor(_Request);
                
            }
            catch (Exception ex)
            {
                var msg = new List<MessageListItem>() { new MessageListItem { MESSAGE = "Author cannot be found." } };
                _Response.ERROR_MESSAGES.AddRange(msg);
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
        public async Task<IActionResult> Update(int id)
        {
            UpdateAuthorResponse _Response = new();
            
            try
            {
                UpdateAuthorRequest _Request = new UpdateAuthorRequest(){ID = id};
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
        public async Task<IActionResult> AddBook(int id)
        {
            AddAuthorBookResponse _Response = new();
            try
            {
                AddAuthorBookRequest _Request = new AddAuthorBookRequest(){AUTHOR_ID = id};
                _Response = await ABService.GetAddAuthorBook(_Request);
                var msgs = new MessageListItem() { MESSAGE = "Successfully added Book to Author."};
                _Response.SUCCESS_MESSAGES.Add(msgs);
            }
            catch (Exception ex)
            {
                var msgs = new MessageListItem(){ MESSAGE = "Error adding Book to Author" };
                _Response.ERROR_MESSAGES.Add(msgs);
            }
        
            //Set list of existing books to use in Select list in view 
            ViewBag.BookId = await ABService.GetBooks();
            return View(_Response);
        }
        //------------------------------------
        public async Task<IActionResult> DeleteBook(int Id, int BookId) 
        {
            var authoredBook = await ABService.GetDeleteAuthorBook(new DeleteAuthorBookRequest()
            {
                AUTHOR_ID = Id, 
                BOOK_ID = BookId
            });
            
            return View(authoredBook);
        }
        //------------------------------------
        public async Task<IActionResult> Delete(int id)
                {
                    DeleteAuthorResponse _Response = new();
                    try
                    {
                        _Response = await AuthorService.GetDeleteAuthor(new DeleteAuthorRequest() { AUTHOR_ID = id });
                    }
                    catch (Exception ex)
                    {
                        var msgs = new List<MessageListItem>() { new MessageListItem() { MESSAGE = "Error deleting Author." } };
                        _Response.ERROR_MESSAGES.AddRange(msgs);
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

            try
            { 
                _Response = await AuthorService.SubmitAddAuthor(_Request);
                var msgs = new MessageListItem() { MESSAGE = "Successfully added Author." };
                _Response.SUCCESS_MESSAGES.Add(msgs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                var msgs = new MessageListItem() { MESSAGE = "Error adding Author in POST." };
                _Response.ERROR_MESSAGES.Add(msgs);
            }

            return Json(_Response);
        }
        //------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromBody] UpdateAuthorSubmitRequest _Request)
        {
            UpdateAuthorSubmitResponse _Response = new();
            try
            {
                _Response = await AuthorService.SubmitUpdateAuthor(_Request);
                var msgs = new List<MessageListItem>() { new MessageListItem() { MESSAGE = "Successfully updated Author." } };
                _Response.SUCCESS_MESSAGES.AddRange(msgs);
            }
            catch(Exception ex)
            {
                var msgs = new List<MessageListItem>() { new MessageListItem(){ MESSAGE = "Error adding Author." }};
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }
            return RedirectToAction("View", _Response);
        }
        //------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook([FromBody] AddAuthorBookSubmitRequest _Request)
        {
            AddAuthorBookSubmitResponse _Response = new();
            
            try
            {
                _Response = await ABService.SubmitAddAuthorBook(_Request);
                var msgs = new List<MessageListItem>() { new MessageListItem() { MESSAGE = "Successfully added Book to Author." } };
                _Response.SUCCESS_MESSAGES.AddRange(msgs);
                return RedirectToAction("Index", _Response);
            }
            catch (Exception ex)
            {
                var msgs = new List<MessageListItem>() { new MessageListItem(){ MESSAGE = "Error adding Book to Author" }};
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }

            //Set list of existing books to use in Select list in view 
            ViewBag.BookId = await ABService.GetBooks();
            return RedirectToAction("Index", _Response);
        }
        //------------------------------------
        [HttpPost, ActionName("DeleteBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBookConfirmed([FromBody] DeleteAuthorBookSubmitRequest _Request)
        {
            DeleteAuthorBookSubmitResponse _Response = new();

            try
            {
                _Response = await ABService.SubmitDeleteAuthorBook(_Request);
                var msgs = new List<MessageListItem>() { new MessageListItem() { MESSAGE = "Successfully removed Book from Author." } };
                _Response.SUCCESS_MESSAGES.AddRange(msgs);
            }
            catch (Exception ex)
            {
                var msgs = new List<MessageListItem>() { new MessageListItem(){ MESSAGE = "Error removing Book from Author" }};
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }
            return RedirectToAction("View", _Response);
        }
        //------------------------------------
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromBody] DeleteAuthorSubmitRequest _Request)
        {
            DeleteAuthorSubmitResponse _Response = new();

            try
            {
                _Response = await AuthorService.SubmitDeleteAuthor(_Request);
                var msgs = new List<MessageListItem>() { new MessageListItem() { MESSAGE = "Successfully deleted Author." } };
                _Response.SUCCESS_MESSAGES.AddRange(msgs);
            }
            catch (Exception ex)
            {
                var msgs = new List<MessageListItem>() { new MessageListItem() { MESSAGE = "Error deleting Author." } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }
            
            return RedirectToAction("Index", _Response);
        }
        //------------------------------------
        #endregion
    }
}
