using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public float SellingPrice { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Transaction> Transactions { get; set; }  = new List<Transaction>();

        public int CurrentStock
        {
            get
            {
                return Transactions.Where(t => t.TransactionType == "Purchase").Sum(t => t.Quantity)
                     - Transactions.Where(t => t.TransactionType == "Sale").Sum(t => t.Quantity);
            }
        }


    }
}
