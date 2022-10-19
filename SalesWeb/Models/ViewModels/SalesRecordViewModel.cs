using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb.Models.ViewModels
{
    public class SalesRecordViewModel
    {
        public SalesRecord SalesRecord { get; set; }
        public ICollection<Seller> Sellers { get; set; }
    }
}
