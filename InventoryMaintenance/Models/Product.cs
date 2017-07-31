using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryMaintenance.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
    }
}