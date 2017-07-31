using InventoryManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InventoryClient
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
    }

    public class Response : HttpResponseMessage
    {
        public long Code { get; set; }
        public object Data { get; set; }
        public string errorMessage { get; set; }
    }
    /// <summary>
    /// Client To Test the web api code. Server should be hosted before executing the file
    /// </summary>
    class InventoryClientApp
    {
        static void Main(string[] args)
        {
            CreateProductTest_Success().Wait();
            UpdateTest_Success().Wait();
            SearchTest_Success().Wait();
        }        
        
        public static async Task CreateProductTest_Success()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:24247/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               

                var product = new Product() { Name = "Macbook", Qty = 1, Price = 45000 };
                HttpResponseMessage response = await client.PostAsJsonAsync("api/inventory/add", product);
                Response responseMessage = await response.Content.ReadAsAsync<Response>();

                Console.WriteLine(responseMessage.errorMessage);
            }
        }

        public static async Task UpdateTest_Success()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:24247/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var product = new Product() { Id = 4, Name = "Laptop", Qty = 1, Price = 45000 };
                HttpResponseMessage response = await client.PutAsJsonAsync("api/inventory/update", product);
                Response responseMessage = await response.Content.ReadAsAsync<Response>();

                Console.WriteLine(responseMessage.errorMessage);
            }
        }

        public static async Task SearchTest_Success()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:24247/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/inventory/search/?ProductId=4&Quantity=1");
                Response responseMessage = await response.Content.ReadAsAsync<Response>();

                Console.WriteLine(responseMessage.errorMessage);
            }
        }
    }
}
