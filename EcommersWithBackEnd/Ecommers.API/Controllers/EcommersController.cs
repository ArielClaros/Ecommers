using Ecommers.API.Models;
using Ecommers.API.Services;
using EcommersSApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class EcommersController : ControllerBase
{
    private readonly EcommersService _ecommersService;
    private readonly ILogger<EcommersController> _logger;
    private readonly IMessagesProducer _messageProducer;
    public static Product? ActualProduct;

    public EcommersController(ILogger<EcommersController> logger, IMessagesProducer messageProducer, EcommersService ecommersService)
    {
        _ecommersService = ecommersService;
        _logger = logger;
        _messageProducer = messageProducer;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product newProduct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        _messageProducer.SendingMessages<Product>(newProduct);

        await _ecommersService.CreateAsync(newProduct);

        return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
    }

    [HttpGet("products")]
    public async Task<List<Product>> Get() =>
        await _ecommersService.GetAsync();

    [HttpPut("{productId}")]
    public async Task<IActionResult> AddToCart(string productId)
    {
        var product = await _ecommersService.GetAsync(productId);

        if (product is null)
        {
            return NotFound();
        }

        if(product.Stock > 0){
            product.Stock--;
            product.CountInCart++;
            await _ecommersService.UpdateAsync(productId, product);
        } else {
            _messageProducer.SendingMessages<String>("Limite de stock excedido " + product);
            return BadRequest("El producto ya alcanzó el límite de stock.");
        }

        _messageProducer.SendingMessages<String>("Add to cart: " + product);
        return NoContent();
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> RemoveProduct(string productId)
    {
        await _ecommersService.RemoveAsync(productId);
        return Ok();
    }

    [HttpGet("cart")]
    public async Task<ActionResult<List<Product>>> GetProductsInCart(){
        _messageProducer.SendingMessages<String>("Productos en el carrito");
        return Ok(await _ecommersService.GetInCartAsync());
    }

    [HttpGet("pay")]
    public async Task<decimal> GetTotalPrice(){
        var products = await _ecommersService.GetInCartAsync();
        decimal totalPrice = 0;

        foreach(var i in products)
        {
            totalPrice += i.Price;
        }

        _messageProducer.SendingMessages<String>("Precio total" + totalPrice);
        return totalPrice;
    }
}