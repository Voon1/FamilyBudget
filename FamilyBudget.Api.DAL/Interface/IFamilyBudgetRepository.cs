using FamilyBudget.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget.Api.DAL
{
    public interface IFamilyBudgetRepository
    {
        public Task<IEnumerable<BudgetCategoryType>> BudgetCategoryTypeGet(int? budgetCategoryTypeId);
        public Task<int> BudgetCategoryTypeInsert(string BudgetCategoryTypeName);
    }
}
