using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model;

namespace DataAccessLayer.Interface
{
    public interface IUserInterface
    {
        List<RM_UserDetailModel> ListAllUser();
        int registerUser(RM_UserDetailModel model);
        bool signIn(string email,string password);
        string[] userRole(string user_email);
        int GetUserId(string user_email);
    }
}
