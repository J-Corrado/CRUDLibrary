using CRUDLibrary.Domain.Models;

namespace CRUDLibrary.Domain.Interfaces;

public interface IValidation
{
    Task<List<MessageListItem>> SubmitAddAuthor(AddAuthorSubmitRequest _req);
    Task<List<MessageListItem>> SubmitUpdateAuthor(UpdateAuthorSubmitRequest _req);
    
    Task<List<MessageListItem>> SubmitAddBook(AddBookSubmitRequest _req);
    Task<List<MessageListItem>> SubmitUpdateBook(UpdateBookSubmitRequest _req);
    
    Task<List<MessageListItem>> SubmitAddBorrower(AddBorrowerSubmitRequest _req);
    Task<List<MessageListItem>> SubmitUpdateBorrower(UpdateBorrowerSubmitRequest _req);
}