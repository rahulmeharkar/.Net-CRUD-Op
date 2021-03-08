using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using Npgsql;
using System.Data;

namespace DataAccessLayer.Repo
{
    public class UserRepo : IUserInterface
    {
        NpgsqlConnection con = null;
        NpgsqlCommand cmd = null;
        public UserRepo()
        {
            con = ConnectionString.Connection();
            cmd = new NpgsqlCommand();
        }
        public List<RM_UserDetailModel> ListAllUser()
        {
            con.Open();
            List<RM_UserDetailModel> userList = new List<RM_UserDetailModel>();
            string query ="SELECT * FROM public.rm_userdetail";
            cmd = new NpgsqlCommand(query,con);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var user = new RM_UserDetailModel();
                        user.user_id = Convert.ToInt32(dr["user_id"]);
                        user.user_name = dr["user_name"].ToString();
                        user.user_email = dr["user_email"].ToString();
                        user.user_password = dr["user_password"].ToString();
                        userList.Add(user);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return userList;
        }

        public int registerUser(RM_UserDetailModel model)
        {
            con.Open();
            string query = "INSERT INTO public.rm_userdetail(user_name, user_email, user_password) VALUES(@user_name,@user_email,@user_password)";
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user_name", model.user_name);
            cmd.Parameters.AddWithValue("@user_email", model.user_email);
            cmd.Parameters.AddWithValue("@user_password", model.user_password);
            int i = 0;
            try
            {
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return i;
        }

        public bool signIn(string user_email,string user_password)
        {
            string query ="SELECT user_email,user_password FROM public.rm_userdetail where user_email=@user_email AND user_password=@user_password";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user_email", user_email);
            cmd.Parameters.AddWithValue("@user_password",user_password);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        public string[] userRole(string user_email)
        {
            string query = "SELECT  r_name from public.rm_userrole WHERE rid = (SELECT rid FROM public.rm_rolemap WHERE user_id=(SELECT user_id FROM public.rm_userdetail WHERE user_email=@user_email))";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user_email",user_email);
            UserRole rolename = new UserRole();
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                         rolename.r_name = dr["r_name"].ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            string role = rolename.ToString();
            return new[] { role };
        }

        public int GetUserId(string user_email)
        {
            int id = 0;
            string query = "SELECT  user_id FROM public.rm_userdetail WHERE user_email=@user_email";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user_email", user_email);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                         id = Convert.ToInt32(dr["user_id"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return id;
        }
    }
}
