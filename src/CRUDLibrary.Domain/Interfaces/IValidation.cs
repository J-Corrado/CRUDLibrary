using CRUDLibrary.Domain.Models;

namespace CRUDLibrary.Domain.Interfaces;

public interface IValidation
{
    Task<List<MessageListItem>> SubmitAddAuthor(AddAuthorSubmitRequest _req);
    void SubmitUpdateAuthor(ref UpdateAuthorSubmitRequest _req, ref UpdateAuthorSubmitResponse _resp);
    
    Task<List<MessageListItem>> SubmitAddBook(AddBookSubmitRequest _req);
    void SubmitUpdateBook(ref UpdateBookSubmitRequest _req, ref UpdateBookSubmitResponse _resp);
    
    Task<List<MessageListItem>> SubmitAddBorrower(AddBorrowerSubmitRequest _req);
    void SubmitUpdateBorrower(ref UpdateBorrowerSubmitRequest _req, ref UpdateBorrowerSubmitResponse _resp);
}