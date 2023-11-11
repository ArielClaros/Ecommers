using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommers.API.Models;
using Ecommers.API.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly IMessagesProducer _messageProducer;
    private readonly ILogger<BookingController> _logger;
    private static readonly List<Booking> _bookings = new List<Booking>();
    private static readonly List<Product> _cart = new List<Product>();

    private static readonly List<Product> _products = ProductController.products();

    public BookingController(ILogger<BookingController> logger, IMessagesProducer messageProducer)
    {
        _logger = logger;
        _messageProducer = messageProducer;
    }

    [HttpPost("AddToCart")]
    public IActionResult AddToCart(int productId, string userName)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == productId);

        if (existingProduct == null)
        {
            return NotFound("Producto no encontrado.");
        }

        var existingProductInCart = _cart.FirstOrDefault(p => p.Id == productId);

        if (existingProduct.Stock == 0)
        {
            return BadRequest("El producto ya alcanzó el limite se stock.");
        }

        _cart.Add(existingProduct);
        existingProduct.Stock --;

        _messageProducer.SendingMessages<Product>(existingProduct);

        return Ok();
    }
}
