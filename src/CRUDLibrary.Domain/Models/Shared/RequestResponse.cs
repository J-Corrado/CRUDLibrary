using CRUDLibrary.Data.LIB_DB.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CRUDLibrary.Domain.Models;

public class RequestModel
{
    public int REQ_AUTHOR_ID { get; set; }
    public string REQ_AUTHOR_NAME { get; set; } = string.Empty;
    
    public int REQ_BOOK_ID { get; set; }
    public string REQ_BOOK_TITLE { get; set; } = string.Empty;
    
    public int REQ_BORROWER_ID { get; set; }
    public string REQ_BORROWER_NAME { get; set; } = string.Empty;

    public int REQ_AUTHOR_BOOK_ID { get; set; }
    public int REQ_BOOK_BORROWER_ID { get; set; }

}

public class ResponseModel
{
    public int RESP_AUTHOR_ID { get; set; }
    public string RESP_AUTHOR_NAME { get; set; } = string.Empty;

    public int RESP_BOOK_ID { get; set; }
    public string RESP_BOOK_TITLE { get; set; } = string.Empty;

    public int RESP_BORROWER_ID { get; set; }
    public string RESP_BORROWER_NAME { get; set; } = string.Empty;

    public int RESP_AUTHOR_BOOK_ID { get; set; }
    public int RESP_BOOK_BORROWER_ID { get; set; }

    public List<BookDto> BOOKS { get; set; } = new List<BookDto>();
    public List<AuthorDto> AUTHORS { get; set; } = new List<AuthorDto>();
    public List<BorrowerDto> BORROWERS { get; set; } = new List<BorrowerDto>();
    public IEnumerable<GenreDto> GENRES { get; set; } = new List<GenreDto>();
    
    
    public IEnumerable<AuthorBookDto> AUTHORED_BOOKS { get; set; } = new List<AuthorBookDto>();
    public IEnumerable<BookBorrowerDto> BOOK_BORROWERS { get; set; } = new List<BookBorrowerDto>();

    public List<MessageListItem> SUCCESS_MESSAGES { get; set; } = new List<MessageListItem>();
    public List<MessageListItem> ERROR_MESSAGES { get; set; } = new List<MessageListItem>();
    public List<MessageListItem> WARNING_MESSAGES { get; set; } = new List<MessageListItem>();

    public string DisplayErrorMessages()
    {
        string _msg = "";
        _msg = "<div class=\"alert alert-danger alert-dismissible show\">" +
               "<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button>" +
               "<div class=\"alert-icon\"><i class=\"bi bi-x-circle\"></i></div>" +
               "<div class=\"alert-text\">" +
               "<h5>Error</h5><ul>";

        for (var i = 0; i < ERROR_MESSAGES.Count; i++)
        {
            var tempmsg = ERROR_MESSAGES[i].MESSAGE;

            if (!tempmsg.Contains("#"))
            {
                _msg += "<li>" + tempmsg + "</li>";
                continue;
            }

            var label = "";
            var validate_id = "";
            var errorType = "";

            string[] array = tempmsg.Split('#');
            var array_count = array.Count();

            if (array_count > 0)
            {
                label = array[0];
            }

            if (array_count > 1)
            {
                errorType = array[1];
            }

            if (array_count > 2)
            {
                validate_id = array[2];
            }

            _msg += "<li>" + label + ((errorType != "") ? (" is " + errorType) : "") + "</li>";
        }

        _msg += "</ul></div></div>";

        return _msg;
    }
    public string DisplaySuccessMessages()
    {
        string _msg = "";
        _msg = "<div class=\"alert alert-success alert-dismissible show\">" +
               "<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button>" +
               "<div class=\"alert-icon\"><i class=\"bi bi-x-circle\"></i></div>" +
               "<div class=\"alert-text\">" +
               "<h5>Error</h5><ul>";

        for (var i = 0; i < SUCCESS_MESSAGES.Count; i++)
        {
            var tempmsg = SUCCESS_MESSAGES[i].MESSAGE;

            if (!tempmsg.Contains("#"))
            {
                _msg += "<li>" + tempmsg + "</li>";
                continue;
            }

            var label = "";
            var validate_id = "";
            var errorType = "";

            string[] array = tempmsg.Split('#');
            var array_count = array.Count();

            if (array_count > 0)
            {
                label = array[0];
            }

            if (array_count > 1)
            {
                errorType = array[1];
            }

            if (array_count > 2)
            {
                validate_id = array[2];
            }

            _msg += "<li>" + label + ((errorType != "") ? (" is " + errorType) : "") + "</li>";
        }

        _msg += "</ul></div></div>";

        return _msg;
    }

    public string DisplayWarningMessages()
    {
        string _msg = "";
        _msg = "<div class=\"alert alert-warning alert-dismissible show\">" +
               "<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button>" +
               "<div class=\"alert-icon\"><i class=\"bi bi-x-circle\"></i></div>" +
               "<div class=\"alert-text\">" +
               "<h5>Warning</h5><ul>";

        for (var i = 0; i < WARNING_MESSAGES.Count; i++)
        {
            var tempmsg = WARNING_MESSAGES[i].MESSAGE;

            if (!tempmsg.Contains("#"))
            {
                _msg += "<li>" + tempmsg + "</li>";
                continue;
            }

            var label = "";
            var validate_id = "";
            var errorType = "";

            string[] array = tempmsg.Split('#');
            var array_count = array.Count();

            if (array_count > 0)
            {
                label = array[0];
            }

            if (array_count > 1)
            {
                errorType = array[1];
            }

            if (array_count > 2)
            {
                validate_id = array[2];
            }

            _msg += "<li>" + label + ((errorType != "") ? (" is " + errorType) : "") + "</li>";
        }

        _msg += "</ul></div></div>";

        return _msg;
    }
}

public class MessageListItem
        {
            public string MESSAGE { get; set; } = string.Empty;
            public string CDE { get; set; } = string.Empty;
        }

