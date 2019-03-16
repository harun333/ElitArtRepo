using ElitArt.Data;
using ElitArt.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElitArt.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController:ControllerBase
    {
        private readonly IElitRepository _repository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController (IElitRepository repository,ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public ActionResult<List<Product>> Get()
        {
            try
            { 
                return _repository.GetAllProducts().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Faild to get products: {ex}");
                return BadRequest("Faild to get products");
            }
        }




    }
}
