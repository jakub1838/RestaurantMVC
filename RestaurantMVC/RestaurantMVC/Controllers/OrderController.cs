using Microsoft.AspNetCore.Mvc;
using RestaurantMVC.Models;
using RestaurantMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] OrderDto orderDto)
        {
            await orderService.Create(orderDto, User);
            return View("Index");
        }
        [HttpGet("create")]
        public IActionResult CreateForm()
        {
            return View();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await orderService.Delete(id, User);
            return View("Index");
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            OrderDto orderDto = await orderService.Get(id, User);
            return View(orderDto);
        }
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            List<OrderDto> orderDtos = await orderService.Get(User);
            return View(orderDtos);
        }
        [HttpGet("addproduct/{orderId}/{productId}")]
        public async Task<IActionResult> AddProduct([FromRoute] int orderId, [FromRoute] int productId)
        {
            await orderService.AddProduct(orderId, productId, User);
            return View();
        }
        [HttpGet("edit/{dto}")]
        public async Task<IActionResult> Edit([FromForm] OrderDto dto)
        {
            await orderService.Edit(dto, User);
            return View();
        }
    }
}
