using Business.Concrete;
using DataAccess.Concrete.EntityFrameWork;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductManager _productManager = new ProductManager(new EfProductDal());

            var result = _productManager.GetProductDetails();

            if (result.Success)
            {
                foreach (var item in result.Data)
                {

                    Console.WriteLine($"{item.ProductName}, {item.ProductCategory}");

                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }
    }
}
