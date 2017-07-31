using InventoryMaintenance.Models;
using InventoryManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace InventoryMaintenance.Repository
{
    public class PerisistProductsFlatFile:IPersistProductObject
    {
        private string dirPath = string.Empty;
        private string filePath = string.Empty;
        public static List<Product> Products = null;

        private string GetDirectoryPath()
        {
            if (dirPath == string.Empty)
                dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["FolderName"]);            

            DirectoryInfo dir = new DirectoryInfo(dirPath);
            if (!dir.Exists)
                dir = Directory.CreateDirectory(dirPath);

            return dirPath;
        }

        private string GetFilePath()
        {
            filePath = Path.Combine(GetDirectoryPath(), ConfigurationManager.AppSettings["FileName"]);            
            return filePath;
        }

        public bool Save(Product product, out int productId)
        {
            var jsonSerializer = new JavaScriptSerializer();
            bool resultFlag = false;
            productId = 0;
            try
            {
                if (Products == null)
                    Products = GetAll();

                if (Products.Exists(X => X.Name.ToUpperInvariant() == product.Name.ToUpperInvariant()))
                    throw new ModuleException(ErrorCode.ProductNameAlreadyExist, ErrorCode.ProductNameAlreadyExistMsg + product.Name);

                product.Id = Products.Count() + 1;
                productId = product.Id;
                Products.Add(product);

                WriteToFile(Products);

                resultFlag = true;
            }
            catch (ModuleException moduleException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ModuleException(ErrorCode.UnknownFailure, e.StackTrace);
            }
            finally
            {
                jsonSerializer = null;
            }
            return resultFlag;
        }

        public bool Update(Product product)
        {
            var jsonSerializer = new JavaScriptSerializer();
            bool resultFlag = false;

            try
            {
                if (Products == null)
                    Products = GetAll();
                
                int index = Products.FindIndex(X=>X.Id == product.Id || X.Name.ToUpperInvariant() == product.Name.ToUpperInvariant());

                if(index <0)
                    throw new ModuleException(ErrorCode.IdNotExist, ErrorCode.IdNotExistMsg);

                Products[index] = product;

                WriteToFile(Products);

                resultFlag = true;
            }
            catch (ModuleException moduleException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ModuleException(ErrorCode.UnknownFailure, e.StackTrace);
            }

            return resultFlag;
        }

        public List<Product> GetAll()
        {
            var jsonSerializer = new JavaScriptSerializer();

            if (filePath == string.Empty)
                filePath = GetFilePath();

            if (Products == null)
                Products = new List<Product>();

            try
            {
                if (File.Exists(filePath))
                {
                    string readJson = System.IO.File.ReadAllText(filePath);
                    if (!string.IsNullOrEmpty(readJson))
                    {                       
                        if (readJson.Length > 0)
                            Products = jsonSerializer.Deserialize<List<Product>>(readJson);
                    }
                }
            }
            catch (ModuleException moduleException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ModuleException(ErrorCode.UnknownFailure, e.StackTrace);
            }
            finally
            {
                jsonSerializer = null;
            }
            return Products;
        }

        public Product FindBy(long Id = 0,string name = "" ,int qty = 0)
        {
            try
            {
                Product product = null;

                if (Products == null)
                {
                    Products = GetAll();
                    if (Products.Count == 0)
                        throw new ModuleException(ErrorCode.IdNotExist, ErrorCode.IdNotExistMsg);
                }
                bool filter = false;

                var query = from prod in Products
                            select prod;
                if (Id > 0)
                {
                    query = from prod in query
                            where prod.Id == Id
                            select prod;

                    filter = true;
                }

                if (!string.IsNullOrEmpty(name))
                {
                    query = from prod in query
                            where prod.Name.ToUpperInvariant() == name.ToUpperInvariant()
                            select prod;

                    filter = true;
                }

                if (qty > 0)
                {
                    query = from prod in query
                            where prod.Qty == qty
                            select prod;
                    filter = true;
                }

                if(filter)
                    product = query.ToList().FirstOrDefault();
                else
                    throw new ModuleException(ErrorCode.FilterCriteriaNotExist, ErrorCode.FilterCriteriaNotExistMsg);
                

                return product;
            }
            catch (ModuleException moduleException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ModuleException(ErrorCode.UnknownFailure, e.StackTrace);
            }
        }

        private void WriteToFile(List<Product> products)
        {
            var jsonSerializer = new JavaScriptSerializer();

             if (string.IsNullOrEmpty(filePath))
                    filePath = GetFilePath();

             using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
             {
                 using (StreamWriter streamWriter = new StreamWriter(fileStream))
                 {
                     string jsonString = jsonSerializer.Serialize(Products);
                     streamWriter.Write(jsonString);
                 }
             }
        }
    }
}