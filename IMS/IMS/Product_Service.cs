
using IMS.Models;
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
    public partial class Product_Service : Form
    {
        ApplicationDbContext context = new ApplicationDbContext();
        BindingSource source;
        public event Action DataUpdate;
        public Product_Service(Product product):this()
        {
          source.DataSource = product;
        }
        public Product_Service()
        {
            InitializeComponent();

            source = new BindingSource();
            Product product = new Product();
            source.DataSource = product;

            textBox1.DataBindings.Add("Text",source,"productId");
            textBox2.DataBindings.Add("Text", source, "Name");
            textBox3.DataBindings.Add("Text", source, "Description");
            textBox4.DataBindings.Add("Text", source, "SellingPrice");
            textBox5.DataBindings.Add("Text", source, "CategoryId");
            textBox6.DataBindings.Add("Text", source, "CurrentStock");



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Product model = source.Current as Product;
            if (model.ProductId == 0)
                AddProduct(model);

            else
                EditProduct(model);
            DataUpdate?.Invoke();
            
        }

        private void AddProduct (Product product) 
        {
            if (product is null)
                MessageBox.Show($"Model is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            var newproduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                SellingPrice = product.SellingPrice,
                CategoryId = product.CategoryId,

            };

            context.products.Add(newproduct);
            context.SaveChanges();

            MessageBox.Show($"Product is added", "Done", MessageBoxButtons.OK,MessageBoxIcon.Information);

        }

        private void EditProduct(Product product)
        {
            var editedproduct = context.products.Find(product.ProductId);
            if (editedproduct != null)
                MessageBox.Show($"Product is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (product is null)
                MessageBox.Show($"Model is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            editedproduct.Name = product.Name;
            editedproduct.Description = product.Description;
            editedproduct.CategoryId = product.CategoryId;
            editedproduct.SellingPrice = product.SellingPrice;

            context.SaveChanges ();

            MessageBox.Show($"Product is Updated", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
