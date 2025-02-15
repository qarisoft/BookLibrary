using System;
using BookLibrary.Constants;
using BookLibrary.Entities;
using BookLibrary.Models;
using BookLibrary.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data;

public class DbSeeder
{
    public static async Task SeedDefaultData(IServiceProvider service)
    {
        try
        {
            var context = service.GetService<AppDbContext>();
            var authService = service.GetService<AuthService>();
            if (context is null)
            {
                return;

            }
            // await CreateProcedure_GetTopNSellingBooksByDate(context);
            // return;

            // this block will check if there are any pending migrations and apply them
            if ((await context.Database.GetPendingMigrationsAsync()).Count() > 0)
            {
                await context.Database.MigrateAsync();
            }

            if (!context.Users.Any())
            {
                var a = new UserDto { Username = "admin", Password = "password" };
                // await authService.RegisterAsync(a);
                if (!await context.Users.AnyAsync(u => u.Username == a.Username))
                {
                    var user = new User();
                    var hashedPassword = new PasswordHasher<User>()
                        .HashPassword(user, a.Password);

                    user.Username = a.Username;
                    user.PasswordHash = hashedPassword;
                    user.Role = "Admin";

                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                }

            }

            if (!context.Genres.Any())
            {
                await SeedGenreAsync(context);
            }

            if (!context.Books.Any())
            {
                await SeedBooksAsync(context);
                // update stock table
                await context.Database.ExecuteSqlRawAsync(@"
                     INSERT INTO Stocks(BookId,Quantity) 
                     SELECT 
                     b.Id,
                     10 
                     FROM Books b
                     WHERE NOT EXISTS (
                     SELECT * FROM [Stocks]
                     );
           ");
            }

            if (!context.orderStatuses.Any())
            {
                await SeedOrderStatusAsync(context);
            }

            // await CreateProcedure_GetTopNSellingBooksByDate(context);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    #region private methods

    private static async Task SeedGenreAsync(AppDbContext context)
    {
        var genres = new[]
         {
            new Genre { GenreName = "Romance" },
            new Genre { GenreName = "Action" },
            new Genre { GenreName = "Thriller" },
            new Genre { GenreName = "Crime" },
            new Genre { GenreName = "SelfHelp" },
            new Genre { GenreName = "Programming" }
        };

        await context.Genres.AddRangeAsync(genres);
        await context.SaveChangesAsync();
    }

    private static async Task SeedOrderStatusAsync(AppDbContext context)
    {
        var orderStatuses = new[]
        {
            new OrderStatus { StatusId = 1, StatusName = "Pending" },
            new OrderStatus { StatusId = 2, StatusName = "Shipped" },
            new OrderStatus { StatusId = 3, StatusName = "Delivered" },
            new OrderStatus { StatusId = 4, StatusName = "Cancelled" },
            new OrderStatus { StatusId = 5, StatusName = "Returned" },
            new OrderStatus { StatusId = 6, StatusName = "Refund" }
        };

        await context.orderStatuses.AddRangeAsync(orderStatuses);
        await context.SaveChangesAsync();
    }

    private static async Task SeedBooksAsync(AppDbContext context)
    {
        var books = new List<Book>
        {
            // Romance Books (GenreId = 1)
            new Book { BookName = "Pride and Prejudice", AuthorName = "Jane Austen", Price = 12.99, GenreId = 1 },
            new Book { BookName = "The Notebook", AuthorName = "Nicholas Sparks", Price = 11.99, GenreId = 1 },
            new Book { BookName = "Outlander", AuthorName = "Diana Gabaldon", Price = 14.99, GenreId = 1 },
            new Book { BookName = "Me Before You", AuthorName = "Jojo Moyes", Price = 10.99, GenreId = 1 },
            new Book { BookName = "The Fault in Our Stars", AuthorName = "John Green", Price = 9.99, GenreId = 1 },
            
            // Action Books (GenreId = 2)
            new Book { BookName = "The Bourne Identity", AuthorName = "Robert Ludlum", Price = 14.99, GenreId = 2 },
            new Book { BookName = "Die Hard", AuthorName = "Roderick Thorp", Price = 13.99, GenreId = 2 },
            new Book { BookName = "Jurassic Park", AuthorName = "Michael Crichton", Price = 15.99, GenreId = 2 },
            new Book { BookName = "The Da Vinci Code", AuthorName = "Dan Brown", Price = 12.99, GenreId = 2 },
            new Book { BookName = "The Hunger Games", AuthorName = "Suzanne Collins", Price = 11.99, GenreId = 2 },
            
            // Thriller Books (GenreId = 3)
            new Book { BookName = "Gone Girl", AuthorName = "Gillian Flynn", Price = 11.99, GenreId = 3 },
            new Book { BookName = "The Girl with the Dragon Tattoo", AuthorName = "Stieg Larsson", Price = 10.99, GenreId = 3 },
            new Book { BookName = "The Silence of the Lambs", AuthorName = "Thomas Harris", Price = 12.99, GenreId = 3 },
            new Book { BookName = "Before I Go to Sleep", AuthorName = "S.J. Watson", Price = 9.99, GenreId = 3 },
            new Book { BookName = "The Girl on the Train", AuthorName = "Paula Hawkins", Price = 13.99, GenreId = 3 },
            
            // Crime Books (GenreId = 4)
            new Book { BookName = "The Godfather", AuthorName = "Mario Puzo", Price = 13.99, GenreId = 4 },
            new Book { BookName = "The Girl with the Dragon Tattoo", AuthorName = "Stieg Larsson", Price = 12.99, GenreId = 4 },
            new Book { BookName = "The Cuckoo's Calling", AuthorName = "Robert Galbraith", Price = 14.99, GenreId = 4 },
            new Book { BookName = "In Cold Blood", AuthorName = "Truman Capote", Price = 11.99, GenreId = 4 },
            new Book { BookName = "The Silence of the Lambs", AuthorName = "Thomas Harris", Price = 15.99, GenreId = 4 },
            
            // SelfHelp Books (GenreId = 5)
            new Book { BookName = "The 7 Habits of Highly Effective People", AuthorName = "Stephen R. Covey", Price = 9.99, GenreId = 5 },
            new Book { BookName = "How to Win Friends and Influence People", AuthorName = "Dale Carnegie", Price = 8.99, GenreId = 5 },
            new Book { BookName = "Atomic Habits", AuthorName = "James Clear", Price = 10.99, GenreId = 5 },
            new Book { BookName = "The Subtle Art of Not Giving a F*ck", AuthorName = "Mark Manson", Price = 7.99, GenreId = 5 },
            new Book { BookName = "You Are a Badass", AuthorName = "Jen Sincero", Price = 11.99, GenreId = 5 },
            
            // Programming Books (GenreId = 6)
            new Book { BookName = "Clean Code", AuthorName = "Robert C. Martin", Price = 19.99, GenreId = 6 },
            new Book { BookName = "Design Patterns", AuthorName = "Erich Gamma", Price = 17.99, GenreId = 6 },
            new Book { BookName = "Code Complete", AuthorName = "Steve McConnell", Price = 21.99, GenreId = 6 },
            new Book { BookName = "The Pragmatic Programmer", AuthorName = "Andrew Hunt", Price = 18.99, GenreId = 6 },
            new Book { BookName = "Head First Design Patterns", AuthorName = "Eric Freeman", Price = 20.99, GenreId = 6 }
        };

        await context.Books.AddRangeAsync(books);
        await context.SaveChangesAsync();
    }

    private static async Task CreateProcedure_GetTopNSellingBooksByDate(AppDbContext context)
    {
        string sql = @"
 IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'Usp_GetTopNSellingBooksByDate' AND type = 'P')
BEGIN
    EXEC ('CREATE PROCEDURE [dbo].[Usp_GetTopNSellingBooksByDate]
    @startDate datetime,
    @endDate datetime
    AS
    BEGIN
        SET NOCOUNT ON;

        WITH UnitSold AS
        (
            SELECT od.BookId, SUM(od.Quantity) AS TotalUnitSold 
            FROM [Order] o 
            JOIN OrderDetail od ON o.Id = od.OrderId
            WHERE o.IsPaid = 1 AND o.IsDeleted = 0 AND o.CreateDate BETWEEN @startDate AND @endDate
            GROUP BY od.BookId
        )
        SELECT TOP 5 b.BookName, b.AuthorName, b.[Image], us.TotalUnitSold 
        FROM UnitSold us
        JOIN [Book] b ON us.BookId = b.Id
        ORDER BY us.TotalUnitSold DESC;
    END');
   END
  ";

        await context.Database.ExecuteSqlRawAsync(sql);
    }

    #endregion

}
