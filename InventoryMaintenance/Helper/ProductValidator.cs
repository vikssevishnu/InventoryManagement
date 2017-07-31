using InventoryMaintenance.Models;
using InventoryManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryMaintenance.Helper
{
    public static class ProductValidator
    {
        public static bool Validate(this Product product)
        {
            bool returnFlag = false;

            if (string.IsNullOrEmpty(product.Name))
                throw new ModuleException(ErrorCode.ProductNameNull, ErrorCode.ProductNameNullMsg);
            
            if(product.Price <=0)
                throw new ModuleException(ErrorCode.InvalidProductPrice, ErrorCode.InvalidProductPriceMsg);

            if (product.Qty <= 0)
                throw new ModuleException(ErrorCode.InvalidQuantity, ErrorCode.InvalidQuantityMsg);

            returnFlag = true;

            return returnFlag;

        }
    }
}