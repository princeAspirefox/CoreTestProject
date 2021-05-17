using IdentityInCore.Data;
using IdentityInCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityInCore.Services
{
    public class UserDeatailsService:IUserDeatilsService
    {
        private readonly IRepositry<UserDetails> userRepository;

//-------------------getting userRepository
        public UserDeatailsService(IRepositry<UserDetails> UserRepository )
        {
            userRepository = UserRepository;
        }
//-------------------deleting user ---------------
        public void DeleteUserDetail(UserDetails user)
        {
            userRepository.Delete(user);
        }
//--------------------get user detail by id--------------
        public UserDetails GetUserDetail(int id)
        {
            return userRepository.GetById(id);
        }
//---------------------get user detail list-----------------
        public IQueryable<UserDetails> GetUserDetails()
        {
            return userRepository.Table;
        }
//---------------------inserting new user details ---------------
        public void InsertUserDetail(UserDetails user)
        {
            userRepository.Insert(user);
        }
//-----------------updating record of existing userdetails
        public void UpdateUserDetail(UserDetails user)
        {
            userRepository.Update(user);
        }
    }
}
