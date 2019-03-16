using AutoMapper;
using ElitArt.Data;
using ElitArt.Data.Entities;
using ElitArt.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElitArt.Controllers
{
    [Route("/api/orders/{orderid}/items")]
    public class OrderItemsController : Controller
    {
        private readonly IElitRepository _repository;
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(IElitRepository repository,
            ILogger<OrderItemsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = _repository.GetOrderById(orderId);
            if (order != null) return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderViewModel>>(order.Items));
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId,int id)
        {
            var order = _repository.GetOrderById(orderId);
            if (order != null) {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                if (item != null) { 
                     return Ok(_mapper.Map<OrderItem, OrderViewModel>(item));
                }
            }
        
            return NotFound();
        }



    }
}
