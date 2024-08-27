
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
    public partial class Category_Service : Form
    {
        ApplicationDbContext context = new ApplicationDbContext();
        BindingSource source;
        public event Action DataUpdated;

        public Category_Service(Category category):this ()
        {
             source.DataSource = category;
        }
        public Category_Service()
        {
            InitializeComponent();
            source = new BindingSource();
            Category model = new Category();
            source.DataSource = model;

            textBox1.DataBindings.Add("Text", source, "CategoryId");
            textBox2.DataBindings.Add("Text", source, "Name");
            textBox3.DataBindings.Add("Text", source, "Description");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Category category = source.Current as Category;
            if (category.CategoryId == 0)
                AddCategory(category);
            else
                EditCategory(category);
            DataUpdated?.Invoke();

        }

        private void AddCategory(Category category) 
        {
            if (category is null)
                MessageBox.Show("Model is Null","Error",MessageBoxButtons.OK);
            var newcat = new Category 
            {
                Name = category.Name,
                Description = category.Description,
            };

            context.categories.Add(newcat);
            context.SaveChanges();
            MessageBox.Show("Category is added", "Done", MessageBoxButtons.OK);
            source.ResetBindings(false);

        }
        private void EditCategory(Category category)
        {
            var categ = context.categories.Find(category.CategoryId);
            if (categ is null)
                MessageBox.Show("Category is not found", "Error", MessageBoxButtons.OK);

            if (category is null)
                MessageBox.Show("Model is Null", "Error", MessageBoxButtons.OK);
            categ.Name = category.Name;
            categ.Description = category.Description;

            context.SaveChanges();
            MessageBox.Show("Category is Updated", "Done", MessageBoxButtons.OK);
            source.ResetBindings(false);

        }

    }
}
