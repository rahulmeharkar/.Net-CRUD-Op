using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer.Model;
using DataAccessLayer.Repo;
using DataAccessLayer.Interface;

namespace ApiLayer.Controllers
{
    public class UserController : ApiController
    {
        IUserInterface userRepo = null;
        public UserController()
        {
            userRepo = new UserRepo();
        }
        [HttpGet]
        [Route("api/getuserlist")]
        public IHttpActionResult GetUserList()
        {
            try
            {
                List<RM_UserDetailModel> user = new List<RM_UserDetailModel>();
                user = userRepo.ListAllUser();
                return Ok(user);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("api/register")]
        public IHttpActionResult RegisterUser([FromBody]RM_UserDetailModel model)
        {
            RM_UserDetailModel detail = new RM_UserDetailModel();
            detail.user_name = model.user_name;
            detail.user_email = model.user_email;
            detail.user_password = model.user_password;
            int i = userRepo.registerUser(detail);
            if(i >= 1)
            {
                return Ok("Succesfully Register");
            }
            else
            {
                return BadRequest("Not a valid Detail/Request");
            }
        }
        
        [HttpGet]
        [Route("api/signin")]
        public IHttpActionResult SignIn(string user_email,string user_password)
        {
            bool check = userRepo.signIn(user_email, user_password);
            if(check == true)
            {
                return Ok("Valid User");
            }
            else
            {
                return BadRequest("Wrong User Name / Password");
            }
        }
    }
}
