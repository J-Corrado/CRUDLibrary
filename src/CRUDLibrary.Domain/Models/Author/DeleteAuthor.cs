using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.Domain.Models
{
    public class DeleteAuthorRequest : RequestModel
    {
        public decimal AUTHOR_ID { get; set; }
    }

    public class DeleteAuthorResponse : ResponseModel
    {
        public decimal AUTHOR_ID { get; set; }
        public string AUTHOR_NAME { get; set; } = string.Empty;
        public DateTime? AUTHOR_BORN { get; set; }
        public DateTime? AUTHOR_DIED { get; set; }
    }

    public class DeleteAuthorSubmitRequest : RequestModel
    {
        public decimal AUTHOR_ID { get; set; }
        
    }

    public class DeleteAuthorSubmitResponse : ResponseModel
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
    }
    
}
