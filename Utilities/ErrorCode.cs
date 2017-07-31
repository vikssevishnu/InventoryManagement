using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Utilities
{
    public sealed class ErrorCode
    {
        //Error Codes
        public const long Success = 0;
        public const long UnknownFailure = -1;
        public const long ProductObjectNull = 10001;
        public const long ItemAlreadyAvailable = 10002;
        public const long IdNotExist = 10003;
        public const long InvalidQuantity = 10004;
        public const long ProductNameNull = 10005;
        public const long ProductNameAlreadyExist = 10006;
        public const long InvalidProductPrice = 10007;
        public const long FilterCriteriaNotExist = 10008;
        public const long IdAndNameNull = 10009;

        //Error Message
        public const string SuccessMsg = "Success";
        public const string UnknownFailureMsg = "Internal Server Error";
        public const string ProductObjectNullMsg = "Product Object cannot be null or empty.";
        public const string ItemAlreadyAvailableMsg = "Provided Item Already Exist.";
        public const string IdNotExistMsg = "Provided Product Id does not exist.";
        public const string InvalidQuantityMsg = "Provide a valid quantity.";
        public const string ProductNameNullMsg = "Product Name cannot be null or Empty";
        public const string ProductNameAlreadyExistMsg = "Duplicate Product Name : ";
        public const string InvalidProductPriceMsg = "Product Price cannnot be less than or equal to 0.";
        public const string FilterCriteriaNotExistMsg = "No Products available with the provided filter criteria";
        public const string IdAndNameNullMsg = "Both Product Id and Name cannot be null or empty";
    }
}
