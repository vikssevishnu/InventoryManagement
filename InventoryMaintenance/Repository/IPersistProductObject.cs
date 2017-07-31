using InventoryMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMaintenance.Repository
{
    interface IPersistProductObject
    {
        bool Save(Product products, out int productId);
        bool Update(Product product);
        List<Product> GetAll();
        Product FindBy(long Id = 0, string name = "", int qty = 0);
    }
}
