using CRUDLibrary.Data.LIB_DB;
using CRUDLibrary.Domain.Interfaces;
using CRUDLibrary.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CRUDLibrary.Domain.Services
{
    public class Borrower : IBorrower
    {
        public IDAL _DAL;
        public IValidation _validate;

        public Borrower(IDAL DAL, IValidation validate)
        {
            _DAL = DAL;
            _validate = validate;
        }
        
        //------------------------------------
        public async Task<AddBorrowerResponse> GetAddBorrower(AddBorrowerRequest _Request)
        {
            AddBorrowerResponse _Response = new();
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response.RESP_BORROWER_ID = _Request.REQ_BORROWER_ID;
                _Response.RESP_BORROWER_NAME = _Request.REQ_BORROWER_NAME;
                _Response.BORROWERS = await _DAL.QueryGetBorrowers();
            }

            return _Response;
        }
        //------------------------------------
        public async Task<AddBorrowerSubmitResponse> SubmitAddBorrower(AddBorrowerSubmitRequest _Request)
        {
            AddBorrowerSubmitResponse _Response = new();
            
            _validate.SubmitAddBorrower(_Request);
            
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.InsertAddBorrower(_Request);
            }
            return _Response;
        }
        //------------------------------------
        public async Task<AllBorrowersResponse> GetAllBorrowers(AllBorrowersRequest _Request)
        {
            AllBorrowersResponse _Response = new();

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.QueryGetAllBorrowers(_Request);
            }

            return _Response;
        }
        //------------------------------------
        public async Task<BorrowerNameSearchResponse> GetBorrowerNameSearch(BorrowerNameSearchRequest _Request)
        {
            BorrowerNameSearchResponse _Response = new();
            
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.QueryGetBorrowerNameSearch(_Request);
            }
            return _Response;
        }
        //------------------------------------
        public async Task<ViewBorrowerResponse> GetViewBorrower(ViewBorrowerRequest _Request)
        {
            ViewBorrowerResponse _Response = new();

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                var borrower = await _DAL.GetBorrowerById(_Request.BORROWER_ID);
                if (borrower != null)
                {
                    _Response = await _DAL.QueryGetViewBorrower(_Request);
                    if (_Response.ERROR_MESSAGES.Count == 0)
                    {
                        _Response.BORROWER_NAME = borrower.Name;
                    }

                    var borrowedBooks = await _DAL.QueryGetBooksByBorrower(_Request.BORROWER_ID);
                    if (borrowedBooks != null)
                    {
                        _Response.BOOK_BORROWS = borrowedBooks.Select(bb => new BookBorrowerDto
                        { 
                            ID = bb.ID,
                            BOOK_ID = bb.BOOK_ID,
                            BOOK_TITLE = bb.BOOK_TITLE,
                            BOOK_GENRE = bb.BOOK_GENRE,
                            BOOK_PUB_DATE = bb.BOOK_PUB_DATE
                        }).ToList();
                    }
                }
            }
            return _Response;
        }
        //------------------------------------
        public async Task<UpdateBorrowerResponse> GetUpdateBorrower(UpdateBorrowerRequest _Request)
        {
            UpdateBorrowerResponse _Response = new();

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.QueryUpdateBorrower(_Request);
                _Response.RESP_BORROWER_ID = _Request.REQ_BORROWER_ID;
                _Response.RESP_BORROWER_NAME = _Request.REQ_BORROWER_NAME;
            }

            return _Response;
        }
        //------------------------------------
        public async Task<UpdateBorrowerSubmitResponse> SubmitUpdateBorrower(UpdateBorrowerSubmitRequest _Request)
        {
            UpdateBorrowerSubmitResponse _Response = new();
            _validate.SubmitUpdateBorrower(ref _Request, ref _Response);
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.UpdateBorrower(_Request);
            }

            return _Response;
        }
        //------------------------------------
        public async Task<DeleteBorrowerResponse> GetDeleteBorrower(DeleteBorrowerRequest _Request)
        {
            DeleteBorrowerResponse _Response = new();
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.QueryDeleteBorrower(_Request);
                _Response.RESP_BORROWER_NAME = _Request.REQ_BORROWER_NAME;
                _Response.RESP_BORROWER_ID = _Request.REQ_BORROWER_ID;
            }
            return _Response;
        }
        //------------------------------------
        public async Task<DeleteBorrowerSubmitResponse> SubmitDeleteBorrower(DeleteBorrowerSubmitRequest _Request)
        {
            DeleteBorrowerSubmitResponse _Response = new();
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                await _DAL.SubmitDeleteBorrower(_Request);
                _Response.Successful = true;
                _Response.Message = "Borrower deleted successfully.";
            }
            else
            {
                _Response.Successful = false;
                _Response.Message = "Failed to delete Borrower";
            }
            return _Response;
        }
        //------------------------------------
    }
}
