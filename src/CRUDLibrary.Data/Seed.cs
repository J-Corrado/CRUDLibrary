using CRUDLibrary.Data.LIB_DB.Enum;
using CRUDLibrary.Data.LIB_DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CRUDLibrary.Data
{
    public class Seed
    {
        public static async Task SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<LIBDBContext>();
                
                context.Database.EnsureCreated();

                if (!context.AuthorBooks.Any())
                {
                    var georgeOrwell = new Author()
                    {
                        Name = "George Orwell",
                        DateOfBirth = "1903-06-25",
                        DateOfDeath = "1950-01-21"
                    };
                    var hpLovecraft = new Author()
                    {
                        Name = "HP Lovecraft",
                        DateOfBirth = "1890-08-20",
                        DateOfDeath = "1937-03-15"
                    };
                    var jrrTolkien = new Author()
                    {
                        Name = "JRR Tolkien",
                        DateOfBirth = "1892-01-03",
                        DateOfDeath = "1973-09-02"
                    };

                    context.Authors.AddRange(new List<Author>()
                    {
                        georgeOrwell,
                        hpLovecraft,
                        jrrTolkien
                    });
                    await context.SaveChangesAsync();




                    var bor1 = new Borrower()
                    {
                        Name = "John Smith",
                    };
                    var bor2 = new Borrower()
                    {
                        Name = "Elliot Alderson",
                    };
                    var bor3 = new Borrower()
                    {
                        Name = "Abdul Alhazred",
                    };
                    var bor4 = new Borrower()
                    {
                        Name = "Steven Colbert",
                    };
                    context.Borrowers.AddRange(new List<Borrower>()
                    {
                        bor1,
                        bor2,
                        bor3,
                        bor4
                    });
                    await context.SaveChangesAsync();

                    var animalFarm = new Book()
                    {
                        Title = "Animal Farm",
                        PublicationDate = "1945-08-17",
                        Genre = BookGenre.Satire
                    };
                    var nineteenEightyFour = new Book()
                    {
                        Title = "Nineteen Eighty-Four",
                        PublicationDate = "1949-06-08",
                        Genre = BookGenre.Novel
                    };
                    var historyOfNecron = new Book()
                    {
                        Title = "History of the Necronomicon",
                        PublicationDate = "1938",
                        Genre = BookGenre.Horror

                    };
                    var mountainsOfMadness = new Book()
                    {
                        Title = "At the Mountains of Madness",
                        PublicationDate = "1936-02-01",
                        Genre = BookGenre.Horror
                    };
                    var silmarillion = new Book()
                    {
                        Title = "The Silmarillion",
                        PublicationDate = "1977-09-15",
                        Genre = BookGenre.Fantasy
                    };
                    context.Books.AddRange(new List<Book>()
                    {
                        animalFarm,
                        nineteenEightyFour,
                        historyOfNecron,
                        mountainsOfMadness,
                        silmarillion
                    });
                    await context.SaveChangesAsync();

                    context.BookBorrows.AddRange(new List<BookBorrower>()
                    {
                        new BookBorrower()
                        {
                            Book = animalFarm,
                            BorrowedDate = new DateTime(2023, 12, 20).ToString(),
                            ReturnedDate = DateTime.Now.ToString(),
                            IsReturned = true,
                            Borrower = bor1
                        },
                        new BookBorrower()
                        {
                            Book = nineteenEightyFour,
                            BorrowedDate = new DateTime(2023, 12, 01).ToString(),
                            ReturnedDate = new DateTime(2023, 12, 20).ToString(),
                            IsReturned = true,
                            Borrower = bor2
                        },
                        new BookBorrower()
                        {
                            Book = historyOfNecron,
                            BorrowedDate = new DateTime(2023, 06, 14).ToString(),
                            ReturnedDate = new DateTime(2023, 07, 05).ToString(),
                            IsReturned = true,
                            Borrower = bor3
                        },
                        new BookBorrower()
                        {
                            Book = mountainsOfMadness,
                            BorrowedDate = new DateTime(2023, 03, 24).ToString(),
                            ReturnedDate = new DateTime(2023, 04, 01).ToString(),
                            IsReturned = true,
                            Borrower = bor1
                        },
                        new BookBorrower()
                        {
                            Book = silmarillion,
                            BorrowedDate = new DateTime(2023, 09, 16).ToString(),
                            ReturnedDate = new DateTime(2023, 09, 26).ToString(),
                            IsReturned = true,
                            Borrower = bor4
                        }
                    });
                    await context.SaveChangesAsync();


                    context.AuthorBooks.AddRange(new List<AuthorBook>()
                    {
                        new AuthorBook()
                        {
                            Book = animalFarm,
                            Author = georgeOrwell
                        },
                        new AuthorBook()
                        {
                            Book = nineteenEightyFour,
                            Author = georgeOrwell
                        },
                        new AuthorBook()
                        {
                            Book = historyOfNecron,
                            Author = hpLovecraft
                        },
                        new AuthorBook()
                        {
                            Book = mountainsOfMadness,
                            Author = hpLovecraft
                        },
                        new AuthorBook()
                        {
                            Book = silmarillion,
                            Author = jrrTolkien
                        }
                    });
                    await context.SaveChangesAsync();
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
