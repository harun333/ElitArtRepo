using ElitArt.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ElitArt.Data
{
    public class ElitRepository : IElitRepository
    {
        private readonly ElitContext _ctx;
        private readonly ILogger<ElitRepository> _logger;

        public ElitRepository (ElitContext ctx, ILogger<ElitRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
            SaveChanges();
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {

            if (includeItems) { 
                return _ctx.Orders
                    .Include(o=>o.Items)
                    .ThenInclude(i=>i.Product)
                    .ToList();
            }
            else
            {
                return _ctx.Orders
                      .ToList();

            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try {
                _logger.LogInformation("GetAllProducts was called");
            return _ctx.Products
                .OrderBy(p => p.Title)
                .ToList();
            }catch(Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public Order GetOrderById(int id)
        {
            return _ctx.Orders
              .Include(o => o.Items)
              .ThenInclude(i => i.Product)
              .Where(o=>o.Id==id)
              .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                .Where(p => p.Category==category)
                .ToList();
        }

        public bool SaveChanges()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
