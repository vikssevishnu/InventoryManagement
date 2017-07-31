using InventoryMaintenance.Models;
using InventoryMaintenance.Repository;
using InventoryManagement.Utilities;
using InventoryMaintenance.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryMaintenance.Controllers
{
    [RoutePrefix("api/inventory")]
    public class InventoryController : ApiController
    {
        //private static ConcurrentDictionary<int, Product> _products = new ConcurrentDictionary<int, Product>();
        private IProductRepository productRepository;

        public InventoryController()
        {
            productRepository = new ProductRepository();
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddInventory(Product product)
        {
            try
            {
                int productId = 0;

                if (product == null)
                    return Request.CreateResponse<Response>(ResponseBuilder.BuildResponse(ErrorCode.ProductObjectNull, "", ErrorCode.ProductObjectNullMsg, HttpStatusCode.InternalServerError));
                

                if (product.Validate())
                {
                    bool result = productRepository.SaveProductsToDB(product, out productId);
                    return Request.CreateResponse<Response>(ResponseBuilder.BuildResponse(ErrorCode.Success, productId, ErrorCode.SuccessMsg, HttpStatusCode.Created));
                }

                return Request.CreateResponse<Response>(ResponseBuilder.BuildResponse(ErrorCode.UnknownFailure, productId, ErrorCode.UnknownFailureMsg, HttpStatusCode.InternalServerError));
            }
            catch (ModuleException moduleException)
            {
                return Request.CreateResponse<Response>(ResponseBuilder.BuildResponse(moduleException.ErrorCode, string.Empty, moduleException.ErrorMessage, HttpStatusCode.BadRequest));
            }
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage UpdateInventory(Product product)
        {
            try
            {
                if (product == null)
                    return Request.CreateResponse<Response>(ResponseBuilder.BuildResponse(ErrorCode.ProductObjectNull, "", ErrorCode.ProductObjectNullMsg, HttpStatusCode.InternalServerError));

                if (product.Validate())
                {
                    bool result = productRepository.UpdateInDB(product);
                    return Request.CreateResponse<Response>(ResponseBuilder.BuildResponse(ErrorCode.Success, string.Empty, ErrorCode.SuccessMsg, HttpStatusCode.OK));
                }

                return Request.CreateResponse<Response>(ResponseBuilder.BuildResponse(ErrorCode.UnknownFailure, string.Empty, ErrorCode.UnknownFailureMsg, HttpStatusCode.InternalServerError));
            }
            catch (ModuleException moduleException)
            {
                return Request.CreateResponse<Response>(ResponseBuilder.BuildResponse(moduleException.ErrorCode, string.Empty, moduleException.ErrorMessage, HttpStatusCode.BadRequest));
            }
        }

        [HttpGet]         
        [Route("search")] 
        public HttpResponseMessage FindProduct([FromUri]long ProductId = 0,[FromUri] string ProductName = "",[FromUri] int Quantity = 0)
        {
            try
            {
                if (ProductId <= 0 && string.IsNullOrEmpty(ProductName))
                    return Request.CreateResponse<Response>(ResponseBuilder.BuildResponse(ErrorCode.IdAndNameNull, string.Empty, ErrorCode.IdAndNameNullMsg, HttpStatusCode.BadRequest));

                Product product = productRepository.FindInDB(ProductId, ProductName, Quantity);

                if (product == null)
                    return Request.CreateResponse<Response>(ResponseBuilder.BuildResponse(ErrorCode.FilterCriteriaNotExist, string.Empty, ErrorCode.FilterCriteriaNotExistMsg, HttpStatusCode.NotFound));

                return Request.CreateResponse<Response>(ResponseBuilder.BuildResponse(ErrorCode.Success, product, ErrorCode.SuccessMsg, HttpStatusCode.OK));
            }
            catch (ModuleException moduleException)
            {
                return Request.CreateResponse<Response>(ResponseBuilder.BuildResponse(moduleException.ErrorCode, string.Empty, moduleException.ErrorMessage, HttpStatusCode.BadRequest));
            }
        }
    }
}
