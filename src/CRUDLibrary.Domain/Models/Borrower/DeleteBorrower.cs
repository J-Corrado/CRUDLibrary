using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.Domain.Models
{
    public class DeleteBorrowerRequest : RequestModel
    {
        public int BORROWER_ID { get; set; }
    }

    public class DeleteBorrowerResponse : ResponseModel
    {
        public int BORROWER_ID { get; set; }
        public string BORROWER_NAME { get; set; } = string.Empty;

    }
    
    public class DeleteBorrowerSubmitRequest : RequestModel
    {
        public int BORROWER_ID { get; set; }
    }

    public class DeleteBorrowerSubmitResponse : ResponseModel
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
    }

}
