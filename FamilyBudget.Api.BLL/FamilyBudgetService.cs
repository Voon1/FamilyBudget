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

        public async Task<IEnumerable<BudgetCategoryType>> GetBudgetCategoryType(int? budgetCategoryTypeId)
        {
            var type = _familyBudgetRepository.GetBudgetCategoryType(budgetCategoryTypeId);
            return await type;
        }
    }
}