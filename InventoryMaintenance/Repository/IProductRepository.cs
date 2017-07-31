using InventoryMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMaintenance.Repository
{
    interface IProductRepository
    {
        bool SaveProductsToDB(Product product, out int productId);
        bool UpdateInDB(Product product);
        List<Product> GetAllProductsFromDB();
        Product FindInDB(long Id = 0, string name = "", int qty = 0);
    }
}
