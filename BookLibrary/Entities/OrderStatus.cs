using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Entities;

public class OrderStatus
{
    public int Id { get; set; }
    [Required]
    public int StatusId { get; set; }
    [Required, MaxLength(20)]
    public string? StatusName { get; set; }

}
