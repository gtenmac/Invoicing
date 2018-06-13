using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizDataLibrary.Model;

namespace BizDataTest001
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductService service = new ProductService();
            //List<Product> products = new List<Product>()
            //{
            //    new Product(){ PartNo = "A0001",PartName = "小號一字起子"},
            //    new Product(){ PartNo = "A0002",PartName = "大號一字起子"},
            //    new Product(){ PartNo = "B0001",PartName = "小號十字起子"},
            //    new Product(){ PartNo = "B0002",PartName = "大號十字起子"}
            //};

            //foreach (var p in products)
            //{
            //    service.Create(p);
            //}

            var result = service.GetAll();
            foreach(var i in result)
            {
                Console.WriteLine($"料號 {i.PartNo}: 品名:{i.PartName} ");
            }
            Console.WriteLine("==========================");
            var product = service.GetByPartNo("B0001");
            Console.WriteLine($"料號 {product.PartNo}: 品名:{product.PartName} ");
            Console.ReadLine();
        }
    }
}
