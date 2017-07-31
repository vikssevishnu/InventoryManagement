using InventoryMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace InventoryMaintenance.Repository
{
    public class ProductRepository : IProductRepository
    {
        private IPersistProductObject productObject = null;

        public ProductRepository()
        {
            string repositoryType = ConfigurationManager.AppSettings["repositoryType"];

            switch (repositoryType.ToUpperInvariant())
            {
                case "FLATFILE":
                    productObject = new PerisistProductsFlatFile();
                    break;
                default:
                    productObject = new PerisistProductsFlatFile(); //default repository type is set as flat file.
                    break;
            }
        }

        public bool SaveProductsToDB(Product product, out int productId)
        {
            productId = 0;
            if (product != null)                            
                return productObject.Save(product,out productId);            

            return false;
        }

        public bool UpdateInDB(Product product)
        {
            if (product != null)
                return productObject.Update(product);

            return false;
        }

        public List<Product> GetAllProductsFromDB()
        {
            return productObject.GetAll();
        }

        public Product FindInDB(long Id = 0, string name = "", int qty = 0)
        {
            return productObject.FindBy(Id, name, qty);
        }
    }
}