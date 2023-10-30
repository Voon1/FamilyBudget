using Dapper;
using FamilyBudget.Api.DAL.Context;
using FamilyBudget.Api.DAL.Interface;
using FamilyBudget.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget.Api.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FamilyBudgetDbContext _context;

        public UserRepository(FamilyBudgetDbContext context)
        {
            _context = context;
        }
     
        /// <summary>
        /// SImple method to get an user. TODO SALT
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public  User UserGet(string userName, string userPassword)
        {
            using (var conn = _context.CreateConnection())
            {
                var spName = "pr_UserGet";
                var spParameters = new DynamicParameters();

                    spParameters.Add("UserName", userName);
                    spParameters.Add("UserPassword", userPassword);

                var user =  conn.QueryFirstOrDefault<User>(spName, spParameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
                return user;
            }
        }

        public async Task<int> UserInsert(string userName, string userPassword)
        {
            using (var conn = _context.CreateConnection())
            {
                var spName = "pr_UserInsert";
                var spParameters = new DynamicParameters();
                spParameters.Add("UserName", userName);
                spParameters.Add("UserPassword", userPassword);

                var affectedRows = await conn.QueryFirstAsync<int>(spName, spParameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
                return affectedRows;
            }
        }



    }
}
