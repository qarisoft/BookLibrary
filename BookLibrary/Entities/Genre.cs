using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Entities;

public class Genre
{
    public int Id { get; set; }

    [Required]
    [MaxLength(40)]
    public required string GenreName { get; set; }
    public List<Book> Books { get; set; } = [];

    // public Genre(List<Book> books)
    // {
    //     Books = books;
    // }
}
