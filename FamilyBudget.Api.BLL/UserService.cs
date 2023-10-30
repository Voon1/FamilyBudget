using FamilyBudget.Api.DAL;
using FamilyBudget.Api.DAL.Interface;
using FamilyBudget.Api.Interface;
using FamilyBudget.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget.Api.BLL
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _iUserRepository;

        public UserService(IUserRepository userRepository)
        {
            _iUserRepository = userRepository;
        }
        public  User UserGet(string userName, string userPassword)
        {
            var user =  _iUserRepository.UserGet(userName, userPassword);
            return user;
        }

        public Task<int> UserInsert(string userName, string userPassword)
        {
            var user = _iUserRepository.UserInsert(userName, userPassword);
            return user;
        }

        //xDDD --to change
        public bool IsValidUser(User model)
        {
            if (model?.UserId > 0)
                return true;

            return false;
        }

        public User GetUserDetails()
        {
            return new User { UserName = "wt", UserPassword = "wt" };
        }
    }
}
