using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoControl;
namespace Demo
{
    public partial class Form1 : Form
    {
        Product product;
        public Form1()
        {
            InitializeComponent();
            product = new Product();
            product.ProductCode = "Code";
            product.ProductName = "Name";
        }

        private void btnToForm_Click(object sender, EventArgs e)
        {
            this.ToForm(product);
        }

        private void btnToEntity_Click(object sender, EventArgs e)
        {
            this.ToEntity(product);
            MessageBox.Show(product.ProductCode + " " + product.ProductName);
        }
    }
}
