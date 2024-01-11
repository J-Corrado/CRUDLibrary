using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.Domain.Models
{
    public class DeleteAuthorBookRequest : RequestModel
    {
        public decimal AUTHOR_BOOK_ID { get; set; }

        public decimal AUTHOR_ID { get; set; }
        public decimal BOOK_ID { get; set; }
    }

    public class DeleteAuthorBookResponse : ResponseModel
    {
        public decimal AUTHOR_BOOK_ID { get; set; }

        public decimal AUTHOR_ID { get; set; }
        public string AUTHOR_NAME { get; set; } = string.Empty;
        
        public decimal BOOK_ID { get; set; }
        public string BOOK_TITLE { get; set; } = string.Empty;
    }
    public class DeleteAuthorBookSubmitRequest : RequestModel
    {
        public decimal AUTHOR_BOOK_ID { get; set; }

        public decimal AUTHOR_ID { get; set; }
        public decimal BOOK_ID { get; set; }
    }

    public class DeleteAuthorBookSubmitResponse : ResponseModel
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
    }
}
