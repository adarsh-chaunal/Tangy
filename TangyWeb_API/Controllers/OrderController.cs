using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tangy_Business.Repository;
using Tangy_Business.Repository.IRepository;
using Tangy_Models;

namespace TangyWeb_API.Controllers;


[Route("api/[controller]/[action]")] // we can also route to action methods, but this is just for understanding. In practical project you should go with only one approach, either to route to controllers only of route to action methods
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _orderRepository.GetAll());
    }

    [HttpGet("{orderHeaderId}")]
    public async Task<IActionResult> Get(int? orderHeaderId)
    {
        if (orderHeaderId == null || orderHeaderId == 0) 
        {
            return BadRequest(new ErrorModelDTO()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "Invalid Id"
            });
        }
        var orderHeader = await _orderRepository.Get(orderHeaderId.Value); // .Value is required because "productId" is a nullable field, if it was not a nullable than .Value was not required
        if (orderHeader == null)
        {
            return BadRequest(new ErrorModelDTO()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = "Product Not found"
            });
        }
        return Ok(orderHeader);
    }
}
