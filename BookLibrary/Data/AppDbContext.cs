using BookLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<OrderStatus> orderStatuses { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     base.OnConfiguring(optionsBuilder);
        // }
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     string sql = @"
        //     IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'Usp_GetTopNSellingBooksByDate' AND type = 'P')
        //     BEGIN
        //         EXEC ('CREATE PROCEDURE [dbo].[Usp_GetTopNSellingBooksByDate]
        //         @startDate datetime,
        //         @endDate datetime
        //         AS
        //         BEGIN
        //             SET NOCOUNT ON;

        //             WITH UnitSold AS
        //             (
        //                 SELECT od.BookId, SUM(od.Quantity) AS TotalUnitSold 
        //                 FROM [Order] o 
        //                 JOIN OrderDetail od ON o.Id = od.OrderId
        //                 WHERE o.IsPaid = 1 AND o.IsDeleted = 0 AND o.CreateDate BETWEEN @startDate AND @endDate
        //                 GROUP BY od.BookId
        //             )
        //             SELECT TOP 5 b.BookName, b.AuthorName, b.[Image], us.TotalUnitSold 
        //             FROM UnitSold us
        //             JOIN [Book] b ON us.BookId = b.Id
        //             ORDER BY us.TotalUnitSold DESC;
        //         END');
        //     END
        //     ";
        //     // modelBuilder.HasDbFunction(sql);
        //     // Database.ExecuteSqlRaw(sql);
        // }
        // public void a(){
        //     // this.Database
        // }
    }
}


//{
//    "accessToken": "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic2FsYWgiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjRiN2FkNTNmLTY5ODYtNDQwNi03MWI0LTA4ZGQ0NGE2NTBjOCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IiIsImV4cCI6MTczODcxNjYyNSwiaXNzIjoiTXlBd2Vzb21lQXBwIiwiYXVkIjoiTXlBd2Vzb21lQXVkaWVuY2UifQ.vASGY3UcLnFrygQNsiDTBJKtsPNfkd0oYzLxoiQOP9TtH8ci9D16M3zLBj9jN1S8iQsGnTWY5vdhMy56vsBTvQ",
//  "refreshToken": "fQBcJwIR1aK8Tx78K3nRjKMJFiDiTUeSR4sTnJ9cVzc="
//}