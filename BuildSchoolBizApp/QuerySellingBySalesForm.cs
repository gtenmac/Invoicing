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

namespace BuildSchoolBizApp
{
    public partial class QuerySellingBySalesForm : Form
    {
        public QuerySellingBySalesForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var service = new SalesManService();
            var viewModel = service.GetSellingBySalesMan((int)listBox1.SelectedValue);
            dataGridView1.DataSource = viewModel.Item;
        }

        private void QuerySellingBySalesForm_Load(object sender, EventArgs e)
        {
            var service = new SalesManService();
            var viewModel = service.GetSalesMen();
            listBox1.DataSource = viewModel.Item;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "JobNumber";
        }
    }
}
