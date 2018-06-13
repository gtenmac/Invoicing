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
    public class SalesManService
    {
        public OperationResult Create(SalesManViewModel input)
        {
            var result = new OperationResult();
            try
            {
                BizModel context = new BizModel();
                BizRepository<SalesMan> repository = new BizRepository<SalesMan>(context);
                SalesMan sales = new SalesMan() { Name = input.Name };
                repository.Create(sales);
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

        public bool IsNameExsists(SalesManViewModel input)
        {
            BizModel context = new BizModel();
            BizRepository<SalesMan> repository = new BizRepository<SalesMan>(context);
            return repository.GetAll().Any(x => x.Name == input.Name);
        }

        public SalesManListViewModel GetSalesMen()
        {
            var result = new SalesManListViewModel();
            result.Item = new List<SalesManViewModel>();
            BizModel context = new BizModel();
            BizRepository<SalesMan> repository = new BizRepository<SalesMan>(context);
            foreach(var item in repository.GetAll().OrderBy(x=>x.JobNumber))
            {
                var SaleMan = new SalesManViewModel()
                {
                    JobNumber = item.JobNumber,
                    Name = item.Name
                };
                result.Item.Add(SaleMan);
            }
            return result;
        }

        public SellingListQueryViewModel GetSellingBySalesMan(int JobNumber)
        {
            var result = new SellingListQueryViewModel();
            result.Item = new List<SellingQueryViewModel>();
            BizModel context = new BizModel();
            BizRepository<Selling> SellingRepo = new BizRepository<Selling>(context);
            BizRepository<SalesMan> SalesManRepo = new BizRepository<SalesMan>(context);

            var temp = from p in SellingRepo.GetAll()
                       join s in SalesManRepo.GetAll()
                       on p.SalesJobNumber equals s.JobNumber
                       where s.JobNumber == JobNumber
                       select new SellingQueryViewModel
                       {
                           PartNo = p.PartNo,
                           Quantity = p.Quantity,
                           UnitPrice = p.UnitPrice,
                           SalesJobNumber = s.JobNumber,
                           SalesName = s.Name,
                           SellingDay = p.SellingDay,
                           SellingId = p.SellingId,
                           TotalPrice = p.UnitPrice * p.Quantity
                       };
            result.Item = temp.ToList();
            return result;
        }
    }
}
