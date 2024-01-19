
using CRUDLibrary.Data.LIB_DB.Enum;
using CRUDLibrary.Domain.Interfaces;
using CRUDLibrary.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRUDLibrary.Web.Controllers
{
    public class BorrowerController : Controller
    {
        #region Services

        private readonly IBorrower BorrowerService;
        private readonly IBookBorrower BBService;

        #endregion

        #region Constructor

        public BorrowerController(IConfiguration configuration, IBorrower borrowerService,
            IBookBorrower bBService)
        {
            BorrowerService = borrowerService;
            BBService = bBService;
        }

        #endregion

        #region GET

        //------------------------------------
        public async Task<IActionResult> Index()
        {
            AllBorrowersRequest _Request = new();
            AllBorrowersResponse _Response = new();

            _Response = await BorrowerService.GetAllBorrowers(_Request);

            return View(_Response);
        }

        //------------------------------------
        public async Task<IActionResult> View(int id)
        {
            ViewBorrowerResponse _Response = new();

            try
            {
                ViewBorrowerRequest _Request = new ViewBorrowerRequest() { BORROWER_ID = id };
                _Response = await BorrowerService.GetViewBorrower(_Request);
            }
            catch
            {
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Unable to find Borrower." } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
                return RedirectToAction("Index", _Response);
            }

            return View(_Response);
        }

        //------------------------------------
        public async Task<IActionResult> Add()
        {
            AddBorrowerRequest _Request = new();
            AddBorrowerResponse _Response = new();

            try
            {
                _Response = await BorrowerService.GetAddBorrower(_Request);
            }
            catch
            {
                var msgs = new List<MessageListItem>() { new MessageListItem() { MESSAGE = "Unable to add Borrower" } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }

            return View(_Response);
        }

        //------------------------------------
        public async Task<IActionResult> AddBorrow(int id)
        {
            AddBookBorrowerResponse _Response = new();

            try
            {
                AddBookBorrowerRequest _Request = new AddBookBorrowerRequest() { BORROWER_ID = id.ToString() };
                _Response = await BBService.GetAddBookBorrower(_Request);
            }
            catch
            {
                var msgs = new MessageListItem() { MESSAGE = "Unable to find Book or Borrower"  };
                _Response.ERROR_MESSAGES.Add(msgs);
            }

            ViewBag.BookId = await BBService.GetBooks();
            ViewBag.Genre = await BBService.GetGenres();
            return View(_Response);
        }
        //------------------------------------
        public async Task<IActionResult> Update(int id)
        {
            UpdateBorrowerResponse _Response = new();

            try
            {
                UpdateBorrowerRequest _Request = new UpdateBorrowerRequest() { BORROWER_ID = id };
                _Response = await BorrowerService.GetUpdateBorrower(_Request);
            }
            catch
            {
                var msgs = new MessageListItem() { MESSAGE = "Unable to find Borrower."  };
                _Response.ERROR_MESSAGES.Add(msgs);
            }

            return View(_Response);
        }
        //------------------------------------
        public async Task<IActionResult> ReturnBorrow(int Id, int BookId)
        {
            UpdateBookBorrowerResponse _Response = new();

            try
            {
                UpdateBookBorrowerRequest _Request = new UpdateBookBorrowerRequest()
                    { BOOK_ID = BookId, BORROWER_ID = Id };
                _Response = await BBService.GetUpdateBookBorrower(_Request);
            }
            catch
            {
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Unable to locate Book or Borrower." } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }

            return View(_Response);
        }
        //------------------------------------
        public async Task<IActionResult> Delete(int id)
        {
            DeleteBorrowerResponse _Response = new();

            try
            {
                DeleteBorrowerRequest _Request = new DeleteBorrowerRequest() { BORROWER_ID = id };
                _Response = await BorrowerService.GetDeleteBorrower(_Request);
            }
            catch
            {
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Error while attempting to find Borrower." } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }

            return View(_Response);
        }
        //------------------------------------

        #endregion

        #region POST

        //------------------------------------
        [HttpPost]
        public async Task<JsonResult> Add([FromBody] AddBorrowerSubmitRequest _Request)
        {
            AddBorrowerSubmitResponse _Response = new();
            MessageListItem msgs = new();
            try
            {
                _Response = await BorrowerService.SubmitAddBorrower(_Request);
                msgs.MESSAGE = "Successfully added Borrower.";
                _Response.SUCCESS_MESSAGES.Add(msgs);
            }
            catch
            {
                msgs.MESSAGE = "Error when attempting to add Borrower.";
                _Response.ERROR_MESSAGES.Add(msgs);
            }

            return Json(_Response);
        }

        //------------------------------------
        [HttpPost]
        public async Task<JsonResult> AddBorrow([FromBody] AddBookBorrowerSubmitRequest _Request)
        {
            AddBookBorrowerSubmitResponse _Response = new();
            MessageListItem msgs = new();
            
            try
            {
                _Response = await BBService.SubmitAddBookBorrower(_Request);
                msgs.MESSAGE = "Successfully added Book to Borrower.";
                _Response.SUCCESS_MESSAGES.Add(msgs);
            }
            catch
            {
                msgs.MESSAGE = "Error when attempting to add Book to Borrower." ;
                _Response.ERROR_MESSAGES.Add(msgs);
            }

            return Json(_Response);
        }

        //------------------------------------
        [HttpPost]
        public async Task<JsonResult> Update([FromBody] UpdateBorrowerSubmitRequest _Request)
        {
            UpdateBorrowerSubmitResponse _Response = new();
            MessageListItem msgs = new();
            
            try
            {
                _Response = await BorrowerService.SubmitUpdateBorrower(_Request);
                msgs.MESSAGE = "Successfully updated Borrower." ;
                _Response.SUCCESS_MESSAGES.Add(msgs);
            }
            catch
            {
                msgs.MESSAGE = "Error when attempting to update Borrower.";
                _Response.ERROR_MESSAGES.Add(msgs);
            }

            return Json(_Response);
        }

        //------------------------------------
        [HttpPost]
        public async Task<JsonResult> ReturnBorrow([FromBody] UpdateBookBorrowerSubmitRequest _Request)
        {
            UpdateBookBorrowerSubmitResponse _Response = new();

            MessageListItem msgs = new();
            try
            {
                _Response = await BBService.SubmitUpdateBookBorrower(_Request);
                msgs.MESSAGE = "Successfully returned borrowed Book.";
                _Response.SUCCESS_MESSAGES.Add(msgs);
            }
            catch
            {
                msgs.MESSAGE = "Error while attempting to return borrowed Book.";
                _Response.ERROR_MESSAGES.Add(msgs);
            }

            return Json(_Response);
        }

        //------------------------------------
        [HttpPost]
        public async Task<JsonResult> Delete([FromBody] DeleteBorrowerSubmitRequest _Request)
        {
            DeleteBorrowerSubmitResponse _Response = new();
            try
            {
                _Response = await BorrowerService.SubmitDeleteBorrower(_Request);
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Successfully deleted Borrower." } };
                _Response.SUCCESS_MESSAGES.AddRange(msgs);
            }
            catch
            {
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Error while attempting to delete Borrower." } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }

            return Json( _Response);
        }

        //------------------------------------

        #endregion


    }

}
