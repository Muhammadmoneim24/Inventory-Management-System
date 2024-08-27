using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Dtos
{
    public class CategoryWiseReportDto
    {
        public string Category { get; set; }
        public int TotalStock { get; set; }
        public int TotalProducts { get; set; }
        public int TotalSales { get; set; }
    }

}
