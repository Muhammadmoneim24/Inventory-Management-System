using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public DateTime Date { get; set; }

        public int Quantity { get; set; }

        public string TransactionType { get; set; }
        public string? Notes { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
