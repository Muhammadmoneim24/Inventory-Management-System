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
    public partial class Transactions : Form
    {
        ApplicationDbContext context = new ApplicationDbContext();
        BindingSource source;
        public Transactions()
        {
            InitializeComponent();
            source = new BindingSource();
            this.Shown += Transactions_Shown;
        }

        private void Transactions_Shown(object? sender, EventArgs e)
        {
            button1.PerformClick();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button1.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.transactions.ToList();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Transaction_Service transaction_Service = new Transaction_Service();
            transaction_Service.DataUpdated += Transaction_Service_DataUpdated;
            transaction_Service.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Transaction transaction = dataGridView1.CurrentRow.DataBoundItem as Transaction;
            Transaction_Service transaction_Service = new Transaction_Service(transaction);
            transaction_Service.DataUpdated += Transaction_Service_DataUpdated;
            transaction_Service.Show();
        }
        private void Transaction_Service_DataUpdated()
        {
            button1.PerformClick();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Confirm Delete!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Transaction transaction = dataGridView1.CurrentRow.DataBoundItem as Transaction;
                if (transaction == null)
                {
                    MessageBox.Show("No transaction selected. Please select a transaction to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var transactionTodelete = context.transactions.Find(transaction.TransactionId);
                context.transactions.Remove(transactionTodelete);
                context.SaveChanges();
                source.DataSource = new Product();
                button1.PerformClick();

            }
        }
    }
}
