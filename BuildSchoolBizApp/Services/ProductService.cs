using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildSchoolBizApp.ViewModels;
using BuildSchoolBizApp.Services;
using BizDataLibrary.Model;
using BizDataLibrary.Repositories;

namespace BuildSchoolBizApp.Services
{
    public class ProductService
    {
        public OperationResult Create(ProductViewModel input)
        {
            var result = new OperationResult();
            try
            {
                BizModel context = new BizModel();
                BizRepository<Product> repository = new BizRepository<Product>(context);
                Product entity = new Product() { PartNo = input.PartNo, PartName = input.PartName };
                repository.Create(entity);
                context.SaveChanges();
                result.IsSuccessful = true;
            }
            catch(Exception ex)
            {
                result.IsSuccessful = false;
                result.exception = ex;
            }

            return result;
        }

        public ProductListViewModel GetProducts()
        {
            var result = new ProductListViewModel();
            result.Item = new List<ProductViewModel>();
            BizModel context = new BizModel();
            BizRepository<Product> repository = new BizRepository<Product>(context);
            foreach(var item in repository.GetAll().OrderBy(x=>x.PartNo))
            {
                var p = new ProductViewModel()
                {
                    PartNo = item.PartNo,
                    PartName = item.PartName
                };
                result.Item.Add(p);
            }
            return result;
        }
    }
}
