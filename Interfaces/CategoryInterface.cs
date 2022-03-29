using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Models;
using BookApi.Utils;

namespace BookApi.Interfaces
{
    [DomainEntity(typeof(Category))]
    public class CategoryInterface
    {
        [DomainPropertyAttribute(nameof(Category.CAT_ID))]
        public int id { get; set; }

        [DomainPropertyAttribute(nameof(Category.CAT_DCREATE))]
        public DateTime create_date { get; set; }

        [DomainPropertyAttribute(nameof(Category.CAT_VKEY))]
        public string key { get; set; }

        [DomainPropertyAttribute(nameof(Category.CAT_VNAME))]
        public string name { get; set; }

        [DomainPropertyAttribute(nameof(Category.CAT_VCOLOR))]
        public string color { get; set; }
    }
}