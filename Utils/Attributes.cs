using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Utils
{
    public sealed class DomainPropertyAttribute : Attribute
    {
        public string[] Properties { get; set; }
        public DomainPropertyAttribute(params string[] properties)
        {
            Properties = properties;
        }

    }
    
    public sealed class DomainEntityAttribute : Attribute
    {
        public Type Type { get; set; }
        public DomainEntityAttribute(Type _type)
        {
            Type = _type;
        }
    }
}