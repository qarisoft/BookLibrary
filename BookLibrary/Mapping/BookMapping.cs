using System;
using BookLibrary.Entities;
using BookLibrary.Models;

namespace BookLibrary.Mapping;

public static class BookMapping
{
    public static BookDto ToDto(this Book book)
    {
        var book_ = new BookDto
        {
            Genre = book.Genre!.GenreName,
            AuthorName = book.AuthorName,
            BookName = book.BookName,
            GenreId = book.GenreId,
            Id = book.Id,
            Image = book.Image,
            Price = book.Price,
        };
        return book_;
    }

}
