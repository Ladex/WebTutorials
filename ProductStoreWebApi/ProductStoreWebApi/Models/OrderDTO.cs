﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductStoreWebApi.Models
{
    public class OrderDTO
    {
        public class Detail
        {
            public int ProductID { get; set; }
            public string Product { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
        public IEnumerable<Detail> Details { get; set; }
    }
}