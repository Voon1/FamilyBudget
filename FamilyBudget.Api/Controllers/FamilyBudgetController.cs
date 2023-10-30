using FamilyBudget.Api.Interface;
using FamilyBudget.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FamilyBudget.Api.Controllers
{
    //[ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyBudgetController //: BaseController<FamilyBudgetController>
    {
        private readonly IFamilyBudgetService _Service;
        public FamilyBudgetController(ILogger<FamilyBudgetController> logger, IFamilyBudgetService Service) //: base(logger)
        {
            this._Service = Service;
        }

        [HttpGet("BudgetCategoriesTypeGet", Name = "BudgetCategoryTypeGet")]
        public async Task<IEnumerable<BudgetCategoryType>> BudgetCategoryTypeGet(int? budgetCategoryTypeId)
        {
            return await _Service.BudgetCategoryTypeGet(budgetCategoryTypeId);
        }

        [HttpPost("BudgetCategoriesTypeInsert", Name = "BudgetCategoryTypeInsert")]
        public async Task<int> BudgetCategoryTypeInsert(string budgetCategoryTypeName)
        {
            return await _Service.BudgetCategoryTypeInsert(budgetCategoryTypeName);
        }
    }
}
