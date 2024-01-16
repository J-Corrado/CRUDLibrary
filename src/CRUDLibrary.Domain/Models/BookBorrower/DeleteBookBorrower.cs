using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.Domain.Models
{
    public class DeleteBookBorrowerRequest : RequestModel
    {
        public string BOOK_BORROWER_ID { get; set; } = string.Empty;
        public int BOOK_ID { get; set; }
        public int BORROWER_ID { get; set; }
    }

    public class DeleteBookBorrowerResponse : ResponseModel
    {
        public int BOOK_BORROWER_ID { get; set; }
        
        public int BOOK_ID { get; set; }
        public string BOOK_TITLE { get; set; } = string.Empty;
        
        public int BORROWER_ID { get; set; }
        public string BORROWER_NAME { get; set; } = string.Empty;
    }

    public class DeleteBookBorrowerSubmitRequest : RequestModel
    {
        public string BOOK_BORROWER_ID { get; set; } = string.Empty;
    }

    public class DeleteBookBorrowerSubmitResponse : ResponseModel
    {
        public int BOOK_ID { get; set; }
        public int BORROWER_ID { get; set; }
    }
}
