
using CRUDLibrary.Data.LIB_DB.Enum;
using CRUDLibrary.Domain.Interfaces;
using CRUDLibrary.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _13_LibraryCRUD.Controllers
{
    public class BorrowersController : Controller
    {
        #region Services

        private readonly IBorrower BorrowerService;
        private readonly IBookBorrower BBService;

        #endregion

        #region Constructor

        public BorrowersController(IConfiguration configuration, ILogger logger, IBorrower borrowerService,
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
                AddBookBorrowerRequest _Request = new AddBookBorrowerRequest() { BORROWER_ID = id };
                _Response = await BBService.GetAddBookBorrower(_Request);
            }
            catch
            {
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Unable to find Book or Borrower" } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }

            ViewBag.BookId = new SelectList(await BBService.GetBooks(), "BookId", "Title");
            ViewBag.Genre = new SelectList(Enum.GetValues(typeof(BookGenre)));
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
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Unable to find Borrower." } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
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

            return RedirectToAction("Index", _Response);
        }
        //------------------------------------

        #endregion

        #region POST

        //------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromBody] AddBorrowerSubmitRequest _Request)
        {
            AddBorrowerSubmitResponse _Response = new();

            try
            {
                _Response = await BorrowerService.SubmitAddBorrower(_Request);
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Successfully added Borrower." } };
                _Response.SUCCESS_MESSAGES.AddRange(msgs);
            }
            catch
            {
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Error when attempting to add Borrower." } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }

            return Json(_Response);
        }

        //------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBorrow([FromBody] AddBookBorrowerSubmitRequest _Request)
        {
            AddBookBorrowerSubmitResponse _Response = new();

            try
            {
                _Response = await BBService.SubmitAddBookBorrower(_Request);
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Successfully added Book to Borrower." } };
                _Response.SUCCESS_MESSAGES.AddRange(msgs);
            }
            catch
            {
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Error when attempting to add Book to Borrower." } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }

            return RedirectToAction("View", _Response);
        }

        //------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromBody] UpdateBorrowerSubmitRequest _Request)
        {
            UpdateBorrowerSubmitResponse _Response = new();

            try
            {
                _Response = await BorrowerService.SubmitUpdateBorrower(_Request);
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Successfully updated Borrower." } };
                _Response.SUCCESS_MESSAGES.AddRange(msgs);
                return RedirectToAction("View", _Response);
            }
            catch
            {
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Error when attempting to update Borrower." } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }

            return Json(_Response);
        }

        //------------------------------------
        [HttpPost, ActionName("ReturnBorrow")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnBorrowConfirm([FromBody] UpdateBookBorrowerSubmitRequest _Request)
        {
            UpdateBookBorrowerSubmitResponse _Response = new();

            try
            {
                _Response = await BBService.SubmitUpdateBookBorrower(_Request);
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Successfully returned borrowed Book." } };
                _Response.SUCCESS_MESSAGES.AddRange(msgs);
            }
            catch
            {
                var msgs = new List<MessageListItem>()
                    { new MessageListItem() { MESSAGE = "Error while attempting to return borrowed Book." } };
                _Response.ERROR_MESSAGES.AddRange(msgs);
            }

            return RedirectToAction("View", _Response);
        }

        //------------------------------------
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromBody] DeleteBorrowerSubmitRequest _Request)
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

            return RedirectToAction("Index", _Response);
        }

        //------------------------------------

        #endregion


    }

}
