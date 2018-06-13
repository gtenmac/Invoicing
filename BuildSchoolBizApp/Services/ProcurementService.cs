using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildSchoolBizApp.Services;
using BuildSchoolBizApp.ViewModels;
using BizDataLibrary.Model;
using BizDataLibrary.Repositories;

namespace BuildSchoolBizApp.Services
{
    public class ProcurementService
    {
        public OperationResult Create(ProcurementViewModel input)
        {
            var result = new OperationResult();
            try
            {
                BizModel context = new BizModel();
                BizRepository<Procurement> repository = new BizRepository<Procurement>(context);
                Procurement procurement = new Procurement()
                {
                    PartNo = input.PartNo,
                    PurchasingDay = input.PurchasingDay,
                    Quantity = input.Quantity,
                    InvetoryQuantity = input.InvetoryQuantity,
                    UintPrice = input.UnitPrice
                };
                repository.Create(procurement);
                context.SaveChanges();
                result.IsSuccessful = true;
                return result;
            }
            catch(Exception ex)
            {
                result.IsSuccessful = false;
                result.exception = ex;
            }

            return result;
        }

        public int GetStocks(string PartNo)
        {
            BizModel context = new BizModel();
            BizRepository<Procurement> repository = new BizRepository<Procurement>(context);
            var item = repository.GetAll().Where(x => x.PartNo == PartNo);
            var result = 0;
            if(item.Count() > 0)
            {
                result = item.Sum(x => x.InvetoryQuantity);
            }
            return result;
        }

        public ProcurementListQueryViewModel GetStockList()
        {
            var result = new ProcurementListQueryViewModel();
            BizModel context = new BizModel();
            BizRepository<Procurement> ProcurementRepo = new BizRepository<Procurement>(context);
            BizRepository<Product> ProductRepo = new BizRepository<Product>(context);
            var temp = from p in ProcurementRepo.GetAll()
                       join q in ProductRepo.GetAll()
                       on p.PartNo equals q.PartNo
                       select new
                       {
                           PartNo = p.PartNo,
                           PartName = q.PartName,
                           Quantity = p.Quantity
                       };
            var group = from t in temp
                        group t by new { t.PartNo, t.PartName } into g
                        select new ProcurementQueryViewModel
                        {
                            PartName = g.Key.PartName,
                            PartNo = g.Key.PartNo,
                            TotoalInvetoryQuantity = g.Sum(x=>x.Quantity)
                        };
            result.Items = group.ToList();
            return result;
        }
    }
}
