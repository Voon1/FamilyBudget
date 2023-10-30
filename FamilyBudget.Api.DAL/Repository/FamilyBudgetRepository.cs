using Dapper;
using FamilyBudget.Api.DAL.Context;
using FamilyBudget.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget.Api.DAL
{

    /*
     TODO:
        1. Impelment rest of the methods
        2. Maybe some generic class for calling SP's? Nice to have if got time fffff
     
     */

    public class FamilyBudgetRepository : IFamilyBudgetRepository
    {
        private readonly FamilyBudgetDbContext _context;

        public FamilyBudgetRepository(FamilyBudgetDbContext context)
        {
            _context = context;
        }

       
        /// <summary>
        /// Method return CategoryTypes or categpry type if provided budgetCategoryTypeId
        /// </summary>
        /// <param name="budgetCategoryTypeId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BudgetCategoryType>> BudgetCategoryTypeGet(int? budgetCategoryTypeId = null, string budgetCategoryTypeName = null)
        {
            using (var conn = _context.CreateConnection())
            {
                var spName = "pr_BudgetCategoryTypeGet";
                var spParameters = new DynamicParameters();

                if (budgetCategoryTypeId.HasValue)
                {
                    spParameters.Add("BudgetCategoryTypeId", budgetCategoryTypeId.Value);
                }

                if(!string.IsNullOrEmpty(budgetCategoryTypeName))
                {
                    spParameters.Add("BudgetCategoryTypeName", budgetCategoryTypeName);
                }

                var budgetCategoryType = await conn.QueryAsync<BudgetCategoryType>(spName, spParameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
                return budgetCategoryType;
            }
        }


        /// <summary>
        /// Method return CategoryTypes or categpry type if provided budgetCategoryTypeId
        /// </summary>
        /// <param name="budgetCategoryTypeId"></param>
        /// <returns></returns>
        public async Task<int> BudgetCategoryTypeInsert(string budgetCategoryTypeName)
        {
            using (var conn = _context.CreateConnection())
            {
                var spName = "pr_BudgetCategoryTypeInsert";
                var spParameters = new DynamicParameters();
                spParameters.Add("BudgetCategoryTypeName", budgetCategoryTypeName);

                var affectedRows = await conn.QueryFirstAsync<int>(spName, spParameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
                return affectedRows;
            }
        }
    }
}
