
using IMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS
{
    public partial class Products : Form
    {
        ApplicationDbContext context = new ApplicationDbContext();
        BindingSource source;
        public Products()
        {
            InitializeComponent();
            source = new BindingSource();
            this.Shown += Products_Shown;
        }

        private void Products_Shown(object? sender, EventArgs e)
        {
            
            button1.PerformClick();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button1.PerformClick();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            dataGridView1.DataSource = GetData();
        }
        private List<Product> GetData()
        {
            var searchtxt = textBox1.Text;
            var products = context.products
                                  .Include(p => p.Transactions)  
                                  .AsQueryable()
                                  .AsNoTracking();

            if (!string.IsNullOrEmpty(searchtxt))
            {
                products = products.Where(e => e.Name.Contains(searchtxt) || e.Description.Contains(searchtxt));
            }

            return products.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var filtertxt = Convert.ToInt32(textBox2.Text);
            if (filtertxt == 0)
            {
                MessageBox.Show("Category Id Must be entered", "Error", MessageBoxButtons.OK);
            }
            dataGridView1.DataSource = context.products.Where(e => e.CategoryId == filtertxt).ToList();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Product_Service product = new Product_Service();
            product.DataUpdate += Product_Service_DataUpdate;
            product.Show();
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Product product = dataGridView1.CurrentRow.DataBoundItem as Product;
            Product_Service product_Service = new Product_Service(product);
            product_Service.DataUpdate += Product_Service_DataUpdate;
            product_Service.Show();
        }

        private void Product_Service_DataUpdate()
        {
            button1.PerformClick();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Confirm Delete!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Product product = dataGridView1.CurrentRow.DataBoundItem as Product;
                if (product == null)
                {
                    MessageBox.Show("No product selected. Please select a product to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var productTodelete = context.products.Find(product.ProductId);
                context.products.Remove(productTodelete);
                context.SaveChanges();
                source.DataSource = new Product();
                button1.PerformClick();

            }
        }

       
    }
}
