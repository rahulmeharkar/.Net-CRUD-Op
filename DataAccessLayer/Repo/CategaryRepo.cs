using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using Npgsql;

namespace DataAccessLayer.Repo
{
    public class CategaryRepo : ICategory
    {
        NpgsqlConnection con = null;
        NpgsqlCommand cmd = null;
        public CategaryRepo()
        {
            con = ConnectionString.Connection();
            cmd = new NpgsqlCommand();
        }
        public int addCategory(CategaryModel cat)
        {
            int i = 0;
            string query = "INSERT INTO public.rm_photoalbum(album_categary) VALUES(@album_categary)";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.Add(new NpgsqlParameter("@album_categary", cat.album_categary));


            try
            {
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return i;
        }

        public CategaryModel CategoryDetails(int cid)
        {
            CategaryModel details = new CategaryModel();

            string query = "select * FROM public.rm_photoalbum where album_id=@album_id";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.Add(new NpgsqlParameter("@album_id", cid));
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        details.album_id = Convert.ToInt32(dr["cid"]);
                        details.album_categary = dr["album_categary"].ToString();
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
            return details;
        }

        public int deleteCategory(int cid)
        {

            int i = 0;
            string query = "DELETE FROM public.rm_photoalbum WHERE album_id = @album_id";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@album_id", cid);

            try
            {
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return i;
        }

        public int editCategory(CategaryModel cat)
        {
            int i = 0;
            string query = "UPDATE public.rm_photoalbum SET album_categary=@album_categary WHERE album_id=@album_id";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@album_id", cat.album_id);
            cmd.Parameters.AddWithValue("@album_categary", cat.album_categary);
           
            try
            {
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return i;
        }

        public List<CategaryModel> getAllCategory()
        {
            List<CategaryModel> categorys = new List<CategaryModel>();
            string query = "SELECT *FROM public.rm_photoalbum";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CategaryModel category = new CategaryModel();
                        category.album_id = Convert.ToInt32(dr["album_id"]);
                        category.album_categary = dr["album_categary"].ToString();
                        categorys.Add(category);
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
            return categorys;
        }
    }
}
