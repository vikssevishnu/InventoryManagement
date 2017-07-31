using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace InventoryMaintenance.Models
{
    public class Response:HttpResponseMessage
    {
        public long Code { get; set; }
        public object Data { get; set; }
        public string errorMessage { get; set; }
    }
}