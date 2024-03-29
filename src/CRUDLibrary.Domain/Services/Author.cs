﻿
using CRUDLibrary.Domain.Interfaces;
using CRUDLibrary.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDLibrary.Domain.Services
{
    public class Author : IAuthor
    {
        public IValidation _validate;
        public IDAL _DAL;
        public Author(IValidation validate, IDAL DAL)
        {
            _validate = validate;
            _DAL = DAL;
        }
        
        //------------------------------------
        public async Task<AddAuthorResponse> GetAddAuthor(AddAuthorRequest _Request)
        {
            AddAuthorResponse _Response = new();

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response.RESP_AUTHOR_NAME = _Request.REQ_AUTHOR_NAME;
            }

            return _Response;
        }
        //------------------------------------
        public async Task<AddAuthorSubmitResponse> SubmitAddAuthor(AddAuthorSubmitRequest _Request)
        {
            AddAuthorSubmitResponse _Response = new();
            
            _Response.ERROR_MESSAGES.AddRange(await _validate.SubmitAddAuthor(_Request));

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.InsertAddAuthor(_Request);
            }
            
            return _Response;
        }
        //------------------------------------
        public async Task<AllAuthorsResponse> GetAllAuthors(AllAuthorsRequest _Request)
        {
            AllAuthorsResponse _Response = new();

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.QueryGetAllAuthors(_Request);
            }
            
            return _Response;
        }
        //------------------------------------
        public async Task<UpdateAuthorResponse> GetUpdateAuthor(UpdateAuthorRequest _Request)
        {
            UpdateAuthorResponse _Response = new();

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.QueryUpdateAuthor(_Request);
                _Response.RESP_AUTHOR_ID = _Request.REQ_AUTHOR_ID;
                _Response.RESP_AUTHOR_NAME = _Request.REQ_AUTHOR_NAME;
            }
            
            return _Response;
        }
        //------------------------------------
        public async Task<UpdateAuthorSubmitResponse> SubmitUpdateAuthor(UpdateAuthorSubmitRequest _Request)
        {
            UpdateAuthorSubmitResponse _Response = new();

            _Response.ERROR_MESSAGES.AddRange(await _validate.SubmitUpdateAuthor(_Request));

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.UpdateAuthor(_Request);
            }

            return _Response;
        }
        //------------------------------------
        public async Task<AuthorNameSearchResponse> GetAuthorNameSearch(AuthorNameSearchRequest _Request)
        {
            AuthorNameSearchResponse _Response = new();

            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.QueryGetAuthorNameSearch(_Request);
            }

            return _Response;
        }
        //------------------------------------
        public async Task<ViewAuthorResponse> GetViewAuthor(ViewAuthorRequest _Request)
        {
            ViewAuthorResponse _Response = new ViewAuthorResponse();
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                var author = await _DAL.GetAuthorById(_Request.AUTHOR_ID);
                if (author.Name != null)
                {
                    _Response = await _DAL.QueryGetViewAuthor(_Request);
                    if (_Response.ERROR_MESSAGES.Count() == 0)
                    {
                        _Response.AUTHOR_NAME = author.Name;
                        _Response.AUTHOR_BORN = author.DateOfBirth.ToString();
                        _Response.AUTHOR_DIED = author.DateOfDeath.ToString();
                    }

                    var authoredBooks = await _DAL.QueryGetAuthoredBooks(_Request.AUTHOR_ID);
                    _Response.AUTHORED_BOOKS = authoredBooks.Select(ab => new AuthorBookDto
                    {
                        ID = ab.ID,
                        BOOK_ID = ab.BOOK_ID,
                        BOOK_TITLE = ab.BOOK_TITLE,
                        BOOK_PUB_DATE = ab.BOOK_PUB_DATE,
                        BOOK_GENRE = ab.BOOK_GENRE
                    }).ToList();
                }
                else
                {
                    _Response.ERROR_MESSAGES.Add(new MessageListItem(){MESSAGE = "Author could not be found!"});
                }
            }
            return _Response;
        }
        //------------------------------------
        public async Task<DeleteAuthorResponse> GetDeleteAuthor(DeleteAuthorRequest _Request)
        {
            DeleteAuthorResponse _Response = new();
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.QueryDeleteAuthor(_Request);
            }

            return _Response;
        }
        //------------------------------------
        public async Task<DeleteAuthorSubmitResponse> SubmitDeleteAuthor(DeleteAuthorSubmitRequest _Request)
        {
            DeleteAuthorSubmitResponse _Response = new();
            if (_Response.ERROR_MESSAGES.Count == 0)
            {
                _Response = await _DAL.SubmitDeleteAuthor(_Request);
                _Response.Successful = true;
                _Response.Message = "Author deleted successfully.";
            }
            else
            {
                _Response.Successful = false;
                _Response.Message = "Failed to delete author.";
            }
            return _Response;
        }
        //------------------------------------
    }
}

