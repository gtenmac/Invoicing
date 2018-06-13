using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuildSchoolBizApp.Services;
using BuildSchoolBizApp.ViewModels;
using BizDataLibrary.Model;
using BizDataLibrary.Repositories;

namespace BuildSchoolBizApp
{
    public partial class AddSellingForm : Form
    {
        public AddSellingForm()
        {
            InitializeComponent();
        }

        private void AddSellingForm_Load(object sender, EventArgs e)
        {
            BindProductsListBox();
            BindSalesManListBox();
        }

        private void BindProductsListBox()
        {
            var service = new ProductService();
            var viewModel = service.GetProducts();
            listBox1.DataSource = viewModel.Item;
            listBox1.DisplayMember = "PartName";
            listBox1.ValueMember = "PartNo";
        }

        private void BindSalesManListBox()
        {
            var service = new SalesManService();
            var viewModel = service.GetSalesMen();
            listBox2.DataSource = viewModel.Item;
            listBox2.DisplayMember = "Name";
            listBox2.ValueMember = "JobNumber";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var viewModel = new SellingViewModel();
            viewModel.PartNo = (string)listBox1.SelectedValue;
            viewModel.SalesJobNumber = (int)listBox2.SelectedValue;
            viewModel.Quantity = (int)numericUpDown1.Value;
            viewModel.UnitPrice = (int)numericUpDown2.Value;
            viewModel.SellingDay = dateTimePicker1.Value;
            var service = new SellingService();
            if(viewModel.Quantity <= GetProductStock(viewModel.PartNo))
            {
                var result = service.Create(viewModel);
                if(result.IsSuccessful)
                {
                    MessageBox.Show("新增出貨成功");
                }
                else
                {
                    var path = result.exception;
                    MessageBox.Show($"發生錯誤，請參考{path}");
                }
            }
            
        }

        private int GetProductStock(string PartNo)
        {
            var service = new ProcurementService();
            return service.GetStocks(PartNo);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
