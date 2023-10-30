using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget.Common.Dapper
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableTypeAttribute : Attribute
    {
        public String SqlTypeName { get; set; }
    }
}
