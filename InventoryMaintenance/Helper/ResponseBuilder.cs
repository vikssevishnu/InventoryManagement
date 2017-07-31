using InventoryMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace InventoryMaintenance
{ 
    public static class ResponseBuilder
    {
       /// <summary>
        /// To build responses that needs to be sent to client applications
       /// </summary>
        /// <param name="code">Error Code </param>
        /// <param name="data">Data that needs to be sent to Client </param>
        /// <param name="errorMsg">Error messsage that needs to be sent </param>
       /// <param name="statusCode">HTTP Status Code</param>
       /// <returns></returns>
        public static Response BuildResponse(long code,object data,string errorMsg,HttpStatusCode statusCode)
        {
            var response = new Response()
            {
                StatusCode = statusCode,
                Code = code,
                Data = data,
                errorMessage = errorMsg
            };

            return response;
        }
    }
}