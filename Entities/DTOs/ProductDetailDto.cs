﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.DTOs
{
    public class ProductDetailDto :IDto
    {
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
    }
}
