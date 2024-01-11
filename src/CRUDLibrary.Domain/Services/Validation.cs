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
            Regex rgx = new Regex(@"^[a-zA-Z0-9]*$");

            if (rgx.IsMatch(_name))
                return ValidationResult.Success;
            else
                return new ValidationResult("");
        }
        public static ValidationResult TITLE(string _title)
        {
            Regex rgx = new Regex(@"^[a-zA-Z0-9]*$");

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
        public static ValidationResult VAL_DATE(string _date)
        {
            Regex rgx = new Regex(@"^\d{1,2}\/\d{1,2}\/\d{4}$");

            if (rgx.IsMatch(_date) | _date.Trim() == "")
                return ValidationResult.Success;
            else
                return new ValidationResult("");
        }
        public static ValidationResult DROP_DOWN(string _drop_down)
        {
            // Dim rgx As New Regex("^\d{2}\/\d{2}\/\d{4}$")

            if (_drop_down.Trim() != "")
                return ValidationResult.Success;
            else
                return new ValidationResult("");
        }
        public static ValidationResult DROP_DOWN_REQ(string _drop_down)
        {
            // Dim rgx As New Regex("^\d{2}\/\d{2}\/\d{4}$")

            if (_drop_down.Trim() != "")
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
                messageList.Add(new MessageListItem() { MESSAGE = i.ErrorMessage + validateId, CDE = "VALIDATION" });
            return messageList;
        }

        public async Task<List<MessageListItem>> SubmitAddAuthor(AddAuthorSubmitRequest _req)
        {
            List<MessageListItem> _rtn = new();

            _rtn.AddRange(ValidateObject(_req));
            _rtn.AddRange(await _DAL.ValidateInsertAuthor(_req));

            return _rtn;
        }
        public void SubmitUpdateAuthor(ref UpdateAuthorSubmitRequest _req, ref UpdateAuthorSubmitResponse _resp)
        {
            _resp.ERROR_MESSAGES.AddRange(ValidateObject(_req));
        }

        public async Task<List<MessageListItem>> SubmitAddBook(AddBookSubmitRequest _req)
        {
            List<MessageListItem> _rtn = new();

            _rtn.AddRange(ValidateObject(_req));
            _rtn.AddRange(await _DAL.ValidateInsertBook(_req));
            
            return _rtn;
        }

        public void SubmitUpdateBook(ref UpdateBookSubmitRequest _req, ref UpdateBookSubmitResponse _resp)
        {
            _resp.ERROR_MESSAGES.AddRange(ValidateObject(_req));
        }

        public async Task<List<MessageListItem>> SubmitAddBorrower(AddBorrowerSubmitRequest _req)
        {
            List<MessageListItem> _rtn = new();
            _rtn.AddRange(ValidateObject(_req));
            _rtn.AddRange(await _DAL.ValidateInsertBorrower(_req));
            return _rtn;
        }

        public void SubmitUpdateBorrower(ref UpdateBorrowerSubmitRequest _req, ref UpdateBorrowerSubmitResponse _resp)
        {
            _resp.ERROR_MESSAGES.AddRange(ValidateObject(_req));
        }
}