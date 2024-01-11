using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDLibrary.Data.LIB_DB
{
    public partial class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfDeath { get; set; }

        public ICollection<AuthorBook>? AuthorBooks { get; set; }
    }
}
