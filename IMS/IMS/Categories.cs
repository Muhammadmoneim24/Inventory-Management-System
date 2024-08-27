
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
    public partial class Categories : Form
    {
        ApplicationDbContext context = new ApplicationDbContext();
        BindingSource source;
        public Categories()
        {
            InitializeComponent();
            this.Shown += Categories_Shown;
            source = new BindingSource();
        }

        private void Categories_Shown(object? sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button1.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var categories = context.categories.Select(e => new { e.CategoryId, e.Name, e.Description }).ToList();
            dataGridView1.DataSource = categories;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Category_Service categoriesService = new Category_Service();
            categoriesService.DataUpdated += CategoriesService_DataUpdated;
            categoriesService.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Category category = dataGridView1.CurrentRow.DataBoundItem as Category;

            Category_Service categoriesService = new Category_Service(category);
            categoriesService.DataUpdated += CategoriesService_DataUpdated;
            categoriesService.Show();
        }

        private void CategoriesService_DataUpdated()
        {
            button1.PerformClick();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            var result = MessageBox.Show("Confirm Delete!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Category category = dataGridView1.CurrentRow.DataBoundItem as Category;
                if (category == null)
                {
                    MessageBox.Show("No category selected. Please select a category to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var categoTodelete = context.categories.Find(category.CategoryId);
                context.categories.Remove(categoTodelete);
                context.SaveChanges();
                source.DataSource = new Category();
                button1.PerformClick();

            }
        }


        
    }
}
