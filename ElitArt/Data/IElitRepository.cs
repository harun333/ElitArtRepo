using System.Collections.Generic;
using ElitArt.Data.Entities;

namespace ElitArt.Data
{
    public interface IElitRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderById(int id);
        bool SaveChanges();
        void AddEntity(object model);
    }
}