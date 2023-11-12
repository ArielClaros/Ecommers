using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommers.API.Models;
using Ecommers.API.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IMessagesProducer _messageProducer;
    private readonly ILogger<PaymentController> _logger;

    private static readonly List<Product> _products = BookingController.CartProducts();

    public PaymentController(ILogger<PaymentController> logger, IMessagesProducer messageProducer)
    {
        _logger = logger;
        _messageProducer = messageProducer;
    }

    [HttpGet("GetPay")]
    public IActionResult GetPay()
    {
        decimal totalPrice = CalculateTotalPrice();
        _messageProducer.SendingMessages<decimal>(totalPrice);
        return Ok(new { TotalPrice = totalPrice });
    }

    private decimal CalculateTotalPrice()
    {
        decimal totalPrice = 0;

        foreach (var product in _products)
        {
            totalPrice += product.Price;
        }
        
        return totalPrice;
    }
}
