using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget.Common.Dapper
{    /// <summary>
    /// Dapper query parameter which DOES NOT add a new parameter to the query
    /// </summary>
    internal sealed class IgnoredQueryParameter : SqlMapper.ICustomQueryParameter
    {
        public void AddParameter(IDbCommand command, String name)
        {
            //NOOP
        }
    }
}