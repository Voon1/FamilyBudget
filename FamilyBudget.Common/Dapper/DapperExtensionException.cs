using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget.Common.Dapper
{
    public class DapperExtensionException : System.ApplicationException
    {
        public DapperExtensionException(string message)
            : base(message)
        { }
    }
}
