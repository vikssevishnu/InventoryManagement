using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryMaintenance.Controllers;
using InventoryMaintenance.Models;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using InventoryManagement.Utilities;
using System.IO;
using System.Configuration;
using System.Web.Http;


namespace InventoryManagement.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTest
    {
        private string dirPath = string.Empty;
        private string filePath = string.Empty;
                
        private string GetDirectoryPath()
        {
            if (dirPath == string.Empty)
                dirPath = Path.Combine(System.Environment.CurrentDirectory, ConfigurationManager.AppSettings["FolderName"]);

            DirectoryInfo dir = new DirectoryInfo(dirPath);
            if (!dir.Exists)
                dir = Directory.CreateDirectory(dirPath);

            return dirPath;
        }

        private string GetFilePath()
        {
            string filePath = Path.Combine(GetDirectoryPath(), ConfigurationManager.AppSettings["FileName"]);
            return filePath;
        }
        //To Remove existing content the file if any.
        private void Init()
        {
            if (string.IsNullOrEmpty(filePath))
                    filePath = GetFilePath();

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write("");
                }
            }
        }

        private InventoryController GetController()
        {
            InventoryController inventoryController = new InventoryController();
            inventoryController.Request = new HttpRequestMessage();
            inventoryController.Configuration = new HttpConfiguration();
            return inventoryController;
        }

        [TestMethod]
        public void AddInventoryTest()
        {
            Init();

            InventoryController inventoryController = GetController();            

            Product product = new Product()
            {
                Name = "Mobile",
                Qty = 5,
                Price = 25000
            };

            var response = inventoryController.AddInventory(product);
            Response responseMessage = response.Content.ReadAsAsync<Response>().Result;

            Assert.AreEqual(HttpStatusCode.Created, responseMessage.StatusCode);
        }

        [TestMethod]
        public void AddInventoryTest_WithSameProductName()
        {
            InventoryController inventoryController = GetController();            

            Product product = new Product()
            {
                Name = "Mobile",
                Qty = 5,
                Price = 25000
            };

            var response = inventoryController.AddInventory(product);
            Response responseMessage = response.Content.ReadAsAsync<Response>().Result;

            Assert.AreEqual(ErrorCode.ProductNameAlreadyExist, responseMessage.Code);
                       
        }

        [TestMethod]
        public void AddInventoryTest_WithInvalidPrice()
        {
            InventoryController inventoryController = GetController();            

            Product product = new Product()
            {
                Name = "Mobile",
                Qty = 5,
                Price = -25000
            };

            var response = inventoryController.AddInventory(product);
            Response responseMessage = response.Content.ReadAsAsync<Response>().Result;

            Assert.AreEqual(ErrorCode.InvalidProductPriceMsg, responseMessage.errorMessage);

        }

        [TestMethod]
        public void UpdateInventoryTest_IdNotExist()
        {
            InventoryController inventoryController = GetController();            

            Product product = new Product()
            {
                Id = 12,
                Name = "Mobile5",
                Qty = 5,
                Price = 45000
            };

            var response = inventoryController.UpdateInventory(product);
            Response responseMessage = response.Content.ReadAsAsync<Response>().Result;

            Assert.AreEqual(ErrorCode.IdNotExistMsg, responseMessage.errorMessage);
        }

        [TestMethod]
        public void UpdateInventoryTest_InvalidQty()
        {
            InventoryController inventoryController = GetController();            

            Product product = new Product()
            {
                Id = 12,
                Name = "Mobile5",
                Qty = -5,
                Price = 45000
            };

            var response = inventoryController.UpdateInventory(product);
            Response responseMessage = response.Content.ReadAsAsync<Response>().Result;

            Assert.AreEqual(ErrorCode.InvalidQuantity, responseMessage.Code);
        }

        [TestMethod]
        public void UpdateInventoryTest()
        {
            InventoryController inventoryController = GetController();            

            Product product = new Product()
            {
                Id = 1,
                Name = "Mobile",
                Qty = 5,
                Price = 45000
            };

            var response = inventoryController.UpdateInventory(product);
            Response responseMessage = response.Content.ReadAsAsync<Response>().Result;

            Assert.AreEqual(ErrorCode.SuccessMsg, responseMessage.errorMessage);
        }
        [TestMethod]
        public void SearchInventoryTest_WithInvalidId()
        {
            InventoryController inventoryController = GetController();            
            var response = inventoryController.FindProduct(ProductId: -10);
            Response responseMessage = response.Content.ReadAsAsync<Response>().Result;

            Assert.AreEqual(ErrorCode.IdAndNameNull, responseMessage.Code);
        }

        [TestMethod]
        public void SearchInventoryTest_WithInvalidCode()
        {
            InventoryController inventoryController = GetController();            
            var response = inventoryController.FindProduct(ProductName: "laptop");
            Response responseMessage = response.Content.ReadAsAsync<Response>().Result;

            Assert.AreEqual(ErrorCode.FilterCriteriaNotExist, responseMessage.Code);
        }

        [TestMethod]
        public void SearchInventoryTest_WithValidCode()
        {
            InventoryController inventoryController = GetController();            
            var response = inventoryController.FindProduct(ProductName: "mobile");
            Response responseMessage = response.Content.ReadAsAsync<Response>().Result;
            Product product = (Product)responseMessage.Data;

            Assert.AreEqual("mobile", product.Name.ToLowerInvariant());
        }               
    }
}
