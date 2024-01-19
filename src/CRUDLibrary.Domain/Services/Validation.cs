using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CRUDLibrary.Domain.Interfaces;
using CRUDLibrary.Domain.Models;

namespace CRUDLibrary.Domain.Services;

public class Validation : IValidation
{
        public IDAL _DAL;
        public Validation(IDAL _dal)
        {
            _DAL = _dal;
        }

        public static ValidationResult NAME(string _name)
        {
            Regex rgx = new Regex(@"^[a-zA-Z0-9\s]*$");

            if (rgx.IsMatch(_name))
                return ValidationResult.Success;
            else
                return new ValidationResult("");
        }
        public static ValidationResult TITLE(string _title)
        {
            Regex rgx = new Regex(@"^[\w\s\p{P}]*$");

            if (rgx.IsMatch(_title))
                return ValidationResult.Success;
            else
                return new ValidationResult("");
        }
        public static ValidationResult ID(string _id)
        {
            Regex rgx = new Regex(@"^\d*$");

            if (rgx.IsMatch(_id) || _id == "0")
                return ValidationResult.Success;
            else
                return new ValidationResult("");
        }

        public static ValidationResult GENRE(string _genre)
        {
            Regex rgx = new Regex(@"^[a-zA-Z0-9]*$");
            if (rgx.IsMatch(_genre))
                return ValidationResult.Success;
            else
                return new ValidationResult("");
        }
        
        public static ValidationResult VAL_DATE(string _date)
        {
            Regex rgx = new Regex(@"^\d{4}\/\d{1,2}\/\d{1,2}$");

            if (string.IsNullOrWhiteSpace(_date) || DateOnly.TryParse(_date, out _))
                return ValidationResult.Success;
            else
                return new ValidationResult("");
        }
        
        public List<MessageListItem> ValidateObject(object Obj, string validateId = "")
        {
            ValidationContext context = new ValidationContext(Obj);
            List<ValidationResult> results = new List<ValidationResult>();
            List<MessageListItem> messageList = new List<MessageListItem>();

            Validator.TryValidateObject(Obj, context, results, true);

            foreach (var i in results)
                messageList.Add(new MessageListItem() { MESSAGE = i.ErrorMessage + validateId });
            return messageList;
        }

        public async Task<List<MessageListItem>> SubmitAddAuthor(AddAuthorSubmitRequest _req)
        {
            List<MessageListItem> _rtn = new();
            
            _rtn.AddRange(ValidateObject(_req));
            _rtn.AddRange(await _DAL.ValidateInsertAuthor(_req));

            return _rtn;
        }public async Task<List<MessageListItem>> SubmitUpdateAuthor(UpdateAuthorSubmitRequest _req)
        {
            List<MessageListItem> _rtn = new();
            
            _rtn.AddRange(ValidateObject(_req));
            _rtn.AddRange(await _DAL.ValidateUpdateAuthor(_req));

            return _rtn;
        }

        public async Task<List<MessageListItem>> SubmitAddBook(AddBookSubmitRequest _req)
        {
            List<MessageListItem> _rtn = new();

            _rtn.AddRange(ValidateObject(_req));
            _rtn.AddRange(await _DAL.ValidateInsertBook(_req));
            
            return _rtn;
        }

        public async Task<List<MessageListItem>> SubmitUpdateBook(UpdateBookSubmitRequest _req)
        {
            List<MessageListItem> _rtn = new();
            _rtn.AddRange(ValidateObject(_req));
            _rtn.AddRange(await _DAL.ValidateUpdateBook(_req));


            return _rtn;
        }

        public async Task<List<MessageListItem>> SubmitAddBorrower(AddBorrowerSubmitRequest _req)
        {
            List<MessageListItem> _rtn = new();
            _rtn.AddRange(ValidateObject(_req));
            _rtn.AddRange(await _DAL.ValidateInsertBorrower(_req));
            return _rtn;
        }

        public async Task<List<MessageListItem>> SubmitUpdateBorrower( UpdateBorrowerSubmitRequest _req)
        {
            List<MessageListItem> _rtn = new();
            _rtn.AddRange(ValidateObject(_req));
            _rtn.AddRange(await _DAL.ValidateUpdateBorrower(_req));

            return _rtn;
        }
}