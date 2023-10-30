using FamilyBudget.Api.DAL;
using FamilyBudget.Api.Interface;
using FamilyBudget.Domain.Entities;
using System.Collections.Generic;

namespace FamilyBudget.Api.BLL
{
    public class FamilyBudgetService : IFamilyBudgetService
    {
        private readonly IFamilyBudgetRepository _familyBudgetRepository;

        public FamilyBudgetService(IFamilyBudgetRepository familyBudgetRepository)
        {
            _familyBudgetRepository = familyBudgetRepository;
        }

        public async Task<IEnumerable<BudgetCategoryType>> BudgetCategoryTypeGet(int? budgetCategoryTypeId, string? budgetCategoryTypeName)
        {
            var type = _familyBudgetRepository.BudgetCategoryTypeGet(budgetCategoryTypeId, budgetCategoryTypeName);
            return await type;
        }

        public async Task<int> BudgetCategoryTypeInsert(string budgetCategoryTypeName)
        {
            var type = _familyBudgetRepository.BudgetCategoryTypeInsert(budgetCategoryTypeName);
            return await type;
        }
    }
}