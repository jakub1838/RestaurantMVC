using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantMVC.Models;
using RestaurantMVC.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IAccountService accountService;

        public ProductController(IProductService productService, IAccountService accountService)
        {
            this.productService = productService;
            this.accountService = accountService;
        }

        // GET: ProductController
        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] ProductDto productDto)
        {
            await productService.Create(productDto, User);
            return View("Index");
        }

        [HttpGet("create")]
        public IActionResult CreateForm()
        {
            return View();
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromForm] ProductDto productDto)
        {
            await productService.Edit(productDto, User);
            return View("Index");
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await productService.Delete(id, User);
            return View("Index");
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            ProductDto productDto =  await productService.Get(id);
            return View(productDto);
        }
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            List<ProductDto> producDtos = await productService.Get();
            ViewBag.RoleId = accountService.GetUser(User).RoleId;

            return View(producDtos);
        }
        [HttpGet("search/{searchPhrase}")]
        public async Task<IActionResult> Search(string searchPhrase)
        {
            List<ProductDto> producDtos = await productService.Search(searchPhrase);
            return View(producDtos);
        }

    }
}
