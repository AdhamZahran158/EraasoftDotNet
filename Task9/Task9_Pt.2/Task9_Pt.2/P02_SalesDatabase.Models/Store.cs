using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9_Pt._2.P02_SalesDatabase.Models
{
    internal class Store
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public ICollection<Sale> ?Sales { get; set; } = new List<Sale>();
    }
}
