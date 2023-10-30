using FamilyBudget.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget.Api.Interface
{
    public interface IUserService
    {
        public User UserGet(string userName, string userPassword);
        public Task<int> UserInsert(string userName, string userPassword);
        bool IsValidUser(User model);
        User GetUserDetails();
    }
}
