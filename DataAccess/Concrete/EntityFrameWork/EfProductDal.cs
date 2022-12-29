using Core.DataAccess.EntityFrameWork;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositroryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (var db = new NorthwindContext())
            {
                var query = from product in db.Products
                            join category in db.Categories on product.CategoryId equals category.CategoryId
                            select new ProductDetailDto
                            {
                                ProductName = product.ProductName,
                                ProductCategory = category.CategoryName
                            };

                var result = query.ToList();

                return result;
            }
        }
    }
}
