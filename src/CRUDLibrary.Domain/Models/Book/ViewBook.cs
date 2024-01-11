using CRUDLibrary.Data.LIB_DB.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CRUDLibrary.Domain.Models;

public class ViewBookRequest : RequestModel
{
    public int BOOK_ID { get; set; }
}

public class ViewBookResponse : ResponseModel
{
    public int BOOK_ID { get; set; }
    
    public string BOOK_TITLE { get; set; }
    public string? BOOK_PUB_DATE { get; set; } = string.Empty;
    public BookGenre? BOOK_GENRE { get; set; }

    public IList<AuthorBookDto> BOOK_AUTHORS { get; set; } = new List<AuthorBookDto>();
    public IList<BookBorrowerDto> BOOK_BORROWERS { get; set; } = new List<BookBorrowerDto>();

}
