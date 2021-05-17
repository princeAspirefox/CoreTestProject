using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityInCore.Models;
using IdentityInCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityInCore.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class UserDetailController : Controller
    {
       
        private readonly IUserDeatilsService userDeatilsService;

        /// <summary>
        /// initializing userDeatilsService by injecting object through constructor
        /// </summary>
        /// <param name="userDeatilsService"></param>
        public UserDetailController(IUserDeatilsService userDeatilsService)
        {
            
            this.userDeatilsService = userDeatilsService;
        }

        /// <summary>
        /// fn of returntype  ActionResult
        /// </summary>
        /// <returns> IEnumerable list of UserDetailList</returns>
        public ActionResult UserDetailList()
        {
            IEnumerable<UserDetails> UserList = userDeatilsService.GetUserDetails();
           
            return View(UserList);
        }



        /// <summary>
        /// fn to return Create UserDetails view to update record 
        /// else{ return view to create new UserDetails }
        /// </summary>
        /// <param name="id"> id of entity</param>
        /// <returns></returns>
        [Route("UserDetail/Create/{id?}")]
        public ActionResult Create(int id=0)
        {
            if (id != 0)
            {
                var UserList = userDeatilsService.GetUserDetail(id);
                return View(UserList);
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// fn to Create new UserDetails  
        /// else{ update  UserDetails }
        /// </summary>
        /// <param name="id"> id of entity</param>
        /// <returns></returns>
        [Route("UserDetail/Create/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserDetails model)
        {
            if (model.Id == 0)
            {
                UserDetails details = new UserDetails { Name = model.Name, Mobile = model.Mobile, State = model.State };
                userDeatilsService.InsertUserDetail(details);
                
                return RedirectToAction("UserDetailList");
            }
            else
            {
                UserDetails user = userDeatilsService.GetUserDetail(model.Id);
                user.Name = model.Name;
                user.Mobile = model.Mobile;
                user.State = model.State;
                userDeatilsService.UpdateUserDetail(user);

                return RedirectToAction("UserDetailList");
            }
           
        }


        /// <summary>
        /// fn to delete delete UserDetails using id
        /// </summary>
        /// <param name="id"> </param>
        /// <returns></returns>


        
        [Route("UserDetail/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            UserDetails user = userDeatilsService.GetUserDetail(id);
            return View(user);
        }

        [Route("UserDetail/Delete/{id:int}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            UserDetails user = userDeatilsService.GetUserDetail(id);
            userDeatilsService.DeleteUserDetail(user);
            
            return RedirectToAction("UserDetailList");
        }
    }
}
