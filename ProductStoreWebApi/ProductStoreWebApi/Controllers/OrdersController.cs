using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ProductStoreWebApi.Models;

namespace ProductStoreWebApi.Controllers
{
    public class OrdersController : ApiController
    {
        private OrdersContext db = new OrdersContext();

        // GET api/Orders
        public IEnumerable<Order> GetOrders()
        {
            return db.Orders.AsEnumerable();
        }

        // GET api/Orders/5
        public OrderDTO GetOrder(int id)
        {
            Order order = db.Orders.Include("OrderDetails.Product")
        .First(o => o.Id == id);
            if (order == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return new OrderDTO()
            {
                Details = from d in order.OrderDetails
                          select new OrderDTO.Detail()
                          {
                              ProductID = d.Product.Id,
                              Product = d.Product.Name,
                              Price = d.Product.Price,
                              Quantity = d.Quantity
                          }
            };
        }


        // POST api/Orders
        public HttpResponseMessage PostOrder(OrderDTO dto)
        {
            if (ModelState.IsValid)
            {
                var order = new Order()
                {
                    Customer = "Ladi",
                    OrderDetails = (from item in dto.Details
                                    select new OrderDetail()
                                        {
                                            ProductId = item.ProductID,
                                            Quantity = item.Quantity
                                        }).ToList()
                };
                db.Orders.Add(order);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, order);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = order.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}