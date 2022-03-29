using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Models;
using BookApi.Utils;

namespace BookApi.Interfaces
{
    [DomainEntity(typeof(FinanceItem))]
    public class FinanceItemInterface
    {
        [DomainPropertyAttribute(nameof(FinanceItem.FIN_ID))]
        public int id { get; set; }

        [DomainPropertyAttribute(nameof(FinanceItem.FIN_DCREATE))]
        public DateTime create_date { get; set; }

        [DomainPropertyAttribute(nameof(FinanceItem.FIN_VCATEGORY))]
        public string category { get; set; }

        [DomainPropertyAttribute(nameof(FinanceItem.FIN_VTITLE))]
        public string title { get; set; }

        [DomainPropertyAttribute(nameof(FinanceItem.FIN_DVALUE))]
        public double value { get; set; }

        [DomainPropertyAttribute(nameof(FinanceItem.FIN_BTYPE))]
        public bool type { get; set; }
    }
}