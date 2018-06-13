using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuildSchoolBizApp.ViewModels;
using BuildSchoolBizApp.Services;

namespace BuildSchoolBizApp
{
    public partial class AddSalesManForm : Form
    {
        public AddSalesManForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("姓名不能為空白");
            }
            else
            {
                SalesManViewModel sales = new SalesManViewModel() { Name = textBox1.Text };
                SalesManService service = new SalesManService();
                var result = service.Create(sales);
                if(result.IsSuccessful)
                {
                    MessageBox.Show("業務員加入成功");
                }
                else
                {
                    var path = result.WriteLog();
                    MessageBox.Show($"發生錯誤，請參考{path}");
                }
            }

            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
