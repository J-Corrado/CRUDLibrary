using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDLibrary.Data.LIB_DB.Enum;

namespace CRUDLibrary.Domain.Models
{
    public class DeleteBookRequest : RequestModel
    {
        public string BOOK_ID { get; set; }
    }

    public class DeleteBookResponse : ResponseModel
    {
        public int BOOK_ID { get; set; }
        public string BOOK_TITLE { get; set; } = string.Empty;
        public string? BOOK_PUB_DATE { get; set; } = string.Empty;
        public BookGenre? BOOK_GENRE { get; set; }
    }
    
    public class DeleteBookSubmitRequest : RequestModel
    {
        public string BOOK_ID { get; set; } = string.Empty;
    }
    public class DeleteBookSubmitResponse : ResponseModel
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
    }
}
