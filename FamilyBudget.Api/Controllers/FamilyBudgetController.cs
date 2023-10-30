using FamilyBudget.Api.Interface;
using FamilyBudget.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("BudgetCategoriesTypeGet", Name = "BudgetCategoryTypeGet")]
        public async Task<IEnumerable<BudgetCategoryType>> BudgetCategoryTypeGet(int? budgetCategoryTypeId, string? budgetCategoryTypeName)
        {
            return await _Service.BudgetCategoryTypeGet(budgetCategoryTypeId, budgetCategoryTypeName);
        }


        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("BudgetCategoriesTypeInsert", Name = "BudgetCategoryTypeInsert")]
        public async Task<int> BudgetCategoryTypeInsert(string budgetCategoryTypeName)
        {
            return await _Service.BudgetCategoryTypeInsert(budgetCategoryTypeName);
        }
    }
}
