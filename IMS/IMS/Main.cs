using IMS.Dtos;
using IMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IMS
{
    public partial class Main : Form
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public Main()
        {
            InitializeComponent();
            this.Shown += Main_Shown;
        }

        private void Main_Shown(object? sender, EventArgs e)
        {
            // Display category-wise report when the form is shown
            dataGridView1.DataSource = GenerateCategoryWiseReport();
            dataGridView1.Refresh();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Products products = new Products();
            products.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Categories categories = new Categories();
            categories.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Transactions transactions = new Transactions();
            transactions.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Refresh the report data when a cell is clicked
            dataGridView1.DataSource = GenerateCategoryWiseReport();
            dataGridView1.Refresh();
        }

        private List<CategoryWiseReportDto> GenerateCategoryWiseReport()
        {
            // Fetch the data into memory
            var products = context.products
                                  .Include(p => p.Category)
                                  .Include(p => p.Transactions)
                                  .AsNoTracking()
                                  .ToList();

            // Perform the grouping and calculations in-memory
            var reportData = products
                .GroupBy(p => p.Category.Name)
                .Select(g => new CategoryWiseReportDto
                {
                    Category = g.Key,
                    TotalStock = g.Sum(p => p.CurrentStock),
                    TotalProducts = g.Count(),
                    TotalSales = g.Sum(p => p.Transactions
                                        .Where(t => t.TransactionType == "Sale")
                                        .Sum(t => t.Quantity))
                })
                .ToList();

            return reportData;
        }
    }
}
