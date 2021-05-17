using IdentityInCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityInCore.Services
{
    public interface IUserDeatilsService
    {
        IQueryable<UserDetails> GetUserDetails();
        UserDetails GetUserDetail(int id);
        void InsertUserDetail(UserDetails user);
        void UpdateUserDetail(UserDetails user);
        void DeleteUserDetail(UserDetails user);
    }
}
