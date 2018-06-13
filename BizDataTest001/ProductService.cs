using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizDataLibrary.Model;
using BizDataLibrary.Repositories;

namespace BizDataTest001
{
    public class ProductService
    {
        public void Create(Product entity)
        {
            BizModel context = new BizModel();
            BizRepository<Product> repository = new BizRepository<Product>(context);
            repository.Create(entity);
            context.SaveChanges();
        }

        public Product GetByPartNo(string partNo)
        {
            BizModel context = new BizModel();
            BizRepository<Product> repository = new BizRepository<Product>(context);
            var result = repository.GetAll().FirstOrDefault(x => x.PartNo == partNo);
            return result;
        }

        public List<Product> GetAll()
        {
            BizModel context = new BizModel();
            BizRepository<Product> repository = new BizRepository<Product>(context);
            var result = repository.GetAll().ToList();
            return result;
        }
    }
}
