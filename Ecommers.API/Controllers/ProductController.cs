using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommers.API.Models;
using Ecommers.API.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMessagesProducer _messageProducer;
    private static readonly List<Product> _products = new List<Product>();

    public ProductController(ILogger<ProductController> logger, IMessagesProducer messageProducer)
    {
        _logger = logger;
        _messageProducer = messageProducer;
    }

    [HttpPost]
    public IActionResult CreateProduct(Product newProduct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (_products.Any(p => p.Id == newProduct.Id))
        {
            return Conflict("El producto ya existe.");
        }

        _products.Add(newProduct);

        _messageProducer.SendingMessages<Product>(newProduct);

        return Ok();
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok(_products);
    }

    [HttpGet("{productId}")]
    public IActionResult GetProductById(int productId)
    {
        var product = _products.FirstOrDefault(p => p.Id == productId);

        if (product == null)
        {
            return NotFound("Producto no encontrado.");
        }

        return Ok(product);
    }

    [HttpPut("{productId}")]
    public IActionResult UpdateProduct(int productId, Product updatedProduct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var existingProduct = _products.FirstOrDefault(p => p.Id == productId);

        if (existingProduct == null)
        {
            return NotFound("Producto no encontrado.");
        }

        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;

        _messageProducer.SendingMessages<Product>(existingProduct);

        return Ok();
    }

    [HttpDelete("{productId}")]
    public IActionResult RemoveProduct(int productId)
    {
        var productToRemove = _products.FirstOrDefault(p => p.Id == productId);

        if (productToRemove == null)
        {
            return NotFound("Producto no encontrado.");
        }

        _products.Remove(productToRemove);

        _messageProducer.SendingMessages<Product>(productToRemove);

        return Ok();
    }

    public static List<Product> products(){
        return _products;
    }
}