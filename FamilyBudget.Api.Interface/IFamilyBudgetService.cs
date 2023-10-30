
using FamilyBudget.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FamilyBudget.Api.Interface
{
    public interface IFamilyBudgetService
    {
        Task<IEnumerable<BudgetCategoryType>> BudgetCategoryTypeGet(int? budgetCategoryTypeId);
        Task<int> BudgetCategoryTypeInsert(string budgetCategoryTypeName);
    }
}
