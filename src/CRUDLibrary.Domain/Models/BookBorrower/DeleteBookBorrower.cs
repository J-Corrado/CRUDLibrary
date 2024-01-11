using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.Domain.Models
{
    public class DeleteBookBorrowerRequest : RequestModel
    {
        public decimal BOOK_BORROWER_ID { get; set; }
        public decimal BOOK_ID { get; set; }
        public decimal BORROWER_ID { get; set; }
    }

    public class DeleteBookBorrowerResponse : ResponseModel
    {
        public decimal BOOK_BORROWER_ID { get; set; }
        
        public decimal BOOK_ID { get; set; }
        public string BOOK_TITLE { get; set; } = string.Empty;
        
        public decimal BORROWER_ID { get; set; }
        public string BORROWER_NAME { get; set; } = string.Empty;
    }

    public class DeleteBookBorrowerSubmitRequest : RequestModel
    {
        public decimal BOOK_BORROWER_ID { get; set; }
        public decimal BOOK_ID { get; set; }
        public decimal BORROWER_ID { get; set; }
    }

    public class DeleteBookBorrowerSubmitResponse : ResponseModel
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
    }
}
