using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookLibrary.Models;

public class BookDto
{

    public int Id { get; set; }

    [Required]
    [MaxLength(40)]
    public string? BookName { get; set; }

    [Required]
    [MaxLength(40)]
    public string? AuthorName { get; set; }
    [Required]
    public double Price { get; set; }
    public string? Image { get; set; }
    [Required]
    public int GenreId { get; set; }
    public required string Genre { get; set; }
    // public IFormFile? ImageFile { get; set; }
    // public IEnumerable<SelectListItem>? GenreList { get; set; }
}
