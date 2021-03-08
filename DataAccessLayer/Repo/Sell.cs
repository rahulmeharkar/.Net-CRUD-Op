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
    public class Sell : ISell
    {
        NpgsqlConnection con = null;
        NpgsqlCommand cmd = null;
        public Sell()
        {
            con = ConnectionString.Connection();
            cmd = new NpgsqlCommand();
        }
        public int AlbumAvgQuantity(int album_id)
        {
            int average = 0;
            string query = "SELECT SUM(quantity) AS avrg FROM public.rm_useralbumphoto WHERE user_albumid=@user_albumid";
            con.Open();
            cmd = new NpgsqlCommand(query,con);
            cmd.Parameters.AddWithValue("@user_albumid",album_id);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        average = Convert.ToInt32(dr["avrg"]);
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
            return average;
        }

        public int AlbumAvgSellQuantity(int album_id)
        {
            int average = 0;
            string query = "SELECT SUM(sellquantity) AS avrg FROM public.rm_useralbumphoto WHERE user_albumid=@user_albumid";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user_albumid", album_id);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        average = Convert.ToInt32(dr["avrg"]);
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
            return average;
        }
    }
}
