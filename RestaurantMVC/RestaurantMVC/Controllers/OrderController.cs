using Microsoft.AspNetCore.Mvc;
using RestaurantMVC.Models;
using RestaurantMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantMVC.Entities;

namespace RestaurantMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IProductService productService; 
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService, IProductService productService)
        {
            this.orderService = orderService;
            this.productService = productService;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            //List<OrderDto> orderDtos = await orderService.Get(User);
            return RedirectToAction("Get");
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create()
        {
            OrderDto orderDto = new OrderDto()
            {
                isDone = false, Products = new List<OrderProducts>()
            };
            await orderService.Create(orderDto, User);

            List<OrderDto> orderDtos = await orderService.Get(User);
            return RedirectToAction("Index");
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await orderService.Delete(id, User);
            return RedirectToAction("Index");
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

        [HttpGet("addproduct/{orderId}")]
        public async Task<IActionResult> AddProduct([FromRoute] int orderId)
        {
            var products = await productService.Get();
            return View("addproduct", products);
        }
        [HttpPost("addproduct/{orderId}/{productId}")]
        public async Task<IActionResult> AddProduct([FromRoute] int orderId, [FromRoute] int productId)
        {
            await orderService.AddProduct(orderId, productId, User);
            return RedirectToAction($"addproduct", new {orderId});
        }

        [HttpGet("products/{orderId}")]
        public async Task<IActionResult> Products([FromRoute] int orderId)
        {
            var productDtos = await orderService.GetProducts(orderId, User);

            return View(productDtos);
        }
    }
}
