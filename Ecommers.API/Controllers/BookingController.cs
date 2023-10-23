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
    private readonly ILogger<BookingController> _logger;
    private readonly IMessagesProducer _messageProducer;

    public static readonly List<Booking> _bookings = new();
    public BookingController(ILogger<BookingController> logger, IMessagesProducer messageProducer)
    {
        _logger = logger;
        _messageProducer = messageProducer;
    }

    [HttpPost]
    public IActionResult CreatingBooking(Booking newBooking)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        _bookings.Add(newBooking);
        _messageProducer.SendingMessages<Booking>(newBooking);

        return Ok();
    }
}