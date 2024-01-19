using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.Domain.Models
{
    public class DeleteAuthorBookRequest : RequestModel
    {
        public string AUTHOR_BOOK_ID { get; set; }

        public int AUTHOR_ID { get; set; }
        public int BOOK_ID { get; set; }
    }

    public class DeleteAuthorBookResponse : ResponseModel
    {
        public int AUTHOR_BOOK_ID { get; set; }

        public int AUTHOR_ID { get; set; }
        public string AUTHOR_NAME { get; set; } = string.Empty;
        public string? AUTHOR_BORN { get; set; } = string.Empty;
        public string? AUTHOR_DIED { get; set; } = string.Empty;
        
        public int BOOK_ID { get; set; }
        public string? BOOK_TITLE { get; set; } = string.Empty;
        public string? BOOK_PUB_DATE { get; set; } = string.Empty;
        public GenreDto? BOOK_GENRE { get; set; }
    }
    public class DeleteAuthorBookSubmitRequest : RequestModel
    {
        public string AUTHOR_BOOK_ID { get; set; } = string.Empty;
    }

    public class DeleteAuthorBookSubmitResponse : ResponseModel
    {
        public int AUTHOR_ID { get; set; }
        public int BOOK_ID { get; set; }
    }
}
