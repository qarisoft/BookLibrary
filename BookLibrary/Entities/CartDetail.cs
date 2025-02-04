using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Entities;

public class CartDetail
{
    public int Id { get; set; }
    [Required]
    public int ShoppingCartId { get; set; }
    [Required]
    public int BookId { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public double UnitPrice { get; set; }
    public required Book Book { get; set; }
    public required ShoppingCart ShoppingCart { get; set; }

}
