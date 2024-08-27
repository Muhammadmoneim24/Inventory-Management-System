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
    public partial class Transaction_Service : Form
    {
        ApplicationDbContext context = new ApplicationDbContext();
        BindingSource source;

        public event Action DataUpdated;

        public Transaction_Service(Transaction transaction):this()
        {
            
            source.DataSource = transaction;
        }
        public Transaction_Service()
        {
            InitializeComponent();
            source = new BindingSource();
            Transaction transaction = new Transaction();
            source.DataSource = transaction;

            textBox1.DataBindings.Add("Text", source, "TransactionId");
            textBox2.DataBindings.Add("Text", source, "Date");
            textBox3.DataBindings.Add("Text", source, "Quantity");
            textBox6.DataBindings.Add("Text", source, "TransactionType");
            textBox4.DataBindings.Add("Text", source, "Notes");
            textBox5.DataBindings.Add("Text", source, "ProductId");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Transaction transaction = source.Current as Transaction;
            if (transaction.TransactionId ==0)
                AddTransaction(transaction);
            else
                EditTransaction(transaction);

            DataUpdated?.Invoke();

        }    

        private void AddTransaction(Transaction transaction)
        {
            if (transaction is null)
                MessageBox.Show($"Model is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            var newtrans = new Transaction
            {
                Date = DateTime.Now,
                Quantity = transaction.Quantity,
                TransactionType = transaction.TransactionType,
                Notes = transaction.Notes,
                ProductId = transaction.ProductId,
            };

            context.transactions.Add(newtrans);
            context.SaveChanges();

            MessageBox.Show($"Transaction is added", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void EditTransaction(Transaction transaction)
        {
            var editedtransaction = context.transactions.Find(transaction.TransactionId);
            if (editedtransaction != null)
                MessageBox.Show($"transaction is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (transaction is null)
                MessageBox.Show($"Model is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            editedtransaction.Date = DateTime.Now;
            editedtransaction.Quantity = transaction.Quantity;
            editedtransaction.Notes = transaction.Notes;

            context.SaveChanges();

            MessageBox.Show($"transaction is Updated", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


    }
}
