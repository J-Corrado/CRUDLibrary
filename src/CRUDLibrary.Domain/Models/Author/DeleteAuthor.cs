using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.Domain.Models
{
    public class DeleteAuthorRequest : RequestModel
    {
        public string AUTHOR_ID { get; set; }
    }

    public class DeleteAuthorResponse : ResponseModel
    {
        public string AUTHOR_ID { get; set; } = string.Empty;
        public string AUTHOR_NAME { get; set; } = string.Empty;
        public string? AUTHOR_BORN { get; set; } = string.Empty;
        public string? AUTHOR_DIED { get; set; } = string.Empty;
    }

    public class DeleteAuthorSubmitRequest : RequestModel
    {
        public string AUTHOR_ID { get; set; } = string.Empty;

    }

    public class DeleteAuthorSubmitResponse : ResponseModel
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
    }
    
}
