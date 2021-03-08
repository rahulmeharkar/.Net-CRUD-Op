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
    public class PhotoRepo : IPhoto
    {
        NpgsqlConnection con = null;
        NpgsqlCommand cmd = null;
        public PhotoRepo()
        {
            con = ConnectionString.Connection();
            cmd = new NpgsqlCommand();
        }
        public int AddPhoto(PhotoModel model)
        {
            model.sellquantity = 0;
            int i = 0;
            string query = "INSERT INTO public.rm_useralbumphoto(useralbumphoto_artist,user_albumid,useralbumphoto_photo,useralbumphoto_name,useralbumphoto_prise,quantity,sellquantity) VALUES(@useralbumphoto_artist,@user_albumid,@useralbumphoto_photo,@useralbumphoto_name,@useralbumphoto_prise,@quantity,@sellquantity)";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.Add(new NpgsqlParameter("@useralbumphoto_artist", model.useralbumphoto_artist));
            cmd.Parameters.Add(new NpgsqlParameter("@user_albumid", model.user_albumid));
            cmd.Parameters.Add(new NpgsqlParameter("@useralbumphoto_photo", model.useralbumphoto_photo));
            cmd.Parameters.Add(new NpgsqlParameter("@useralbumphoto_name", model.useralbumphoto_name));
            cmd.Parameters.Add(new NpgsqlParameter("@useralbumphoto_prise", model.useralbumphoto_prise));
            cmd.Parameters.Add(new NpgsqlParameter("@quantity", model.quantity));
            cmd.Parameters.Add(new NpgsqlParameter("@sellquantity", model.sellquantity));
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

        public int DeletePhoto(int photo_id)
        {
            int i = 0;
            string query = "DELETE FROM public.rm_useralbumphoto WHERE useralbumphoto_id = @useralbumphoto_id";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@useralbumphoto_id", photo_id);

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

        public int EditPhoto(PhotoModel model)
        {
            int i = 0;
            string query = "UPDATE public.rm_useralbumphoto SET useralbumphoto_name=@useralbumphoto_name,useralbumphoto_prise=@useralbumphoto_prise,user_albumid=@user_albumid,useralbumphoto_artist=@useralbumphoto_artist WHERE useralbumphoto_id=@useralbumphoto_id";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@useralbumphoto_name", model.useralbumphoto_name);
            cmd.Parameters.AddWithValue("@useralbumphoto_prise", model.useralbumphoto_prise);
            cmd.Parameters.AddWithValue("@useralbumphoto_id", model.useralbumphoto_id);
            //cmd.Parameters.AddWithValue("@quantity", model.quantity);
            cmd.Parameters.AddWithValue("@user_albumid", model.user_albumid);
            cmd.Parameters.AddWithValue("@useralbumphoto_artist", model.useralbumphoto_artist);
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

        public List<PhotoModel> AllPhoto()
        {

            List<PhotoModel> photo = new List<PhotoModel>();
            string query = String.Format("SELECT rm_useralbumphoto.useralbumphoto_id,rm_useralbumphoto.useralbumphoto_photo,rm_useralbumphoto.useralbumphoto_name,rm_useralbumphoto.useralbumphoto_prise,rm_useralbumphoto.useralbumphoto_artist,rm_photoalbum.album_categary,rm_useralbumphoto.quantity,rm_useralbumphoto.sellquantity FROM public.rm_useralbumphoto FULL OUTER JOIN public.rm_photoalbum ON rm_useralbumphoto.user_albumid=rm_photoalbum.album_id");
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PhotoModel mod = new PhotoModel();
                        mod.useralbumphoto_id = Convert.ToInt32(dr["useralbumphoto_id"]);
                        mod.useralbumphoto_photo = dr["useralbumphoto_photo"].ToString();
                        mod.useralbumphoto_name = dr["useralbumphoto_name"].ToString();
                        mod.useralbumphoto_prise = Convert.ToInt32(dr["useralbumphoto_prise"]);
                        mod.useralbumphoto_artist = dr["useralbumphoto_artist"].ToString();
                        mod.album_categary = dr["album_categary"].ToString();
                        mod.quantity = Convert.ToInt32(dr["quantity"]);
                        mod.sellquantity = Convert.ToInt32(dr["sellquantity"]);
                        photo.Add(mod);
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
            return photo;
        }

        public PhotoModel PhotoDetail(int photo_id)
        {
            PhotoModel model = new PhotoModel();
            string query = String.Format("Select * FROM rm_useralbumphoto WHERE useralbumphoto_id={0}", photo_id);
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.useralbumphoto_id = Convert.ToInt32(dr["useralbumphoto_id"]);
                        model.useralbumphoto_name = dr["useralbumphoto_name"].ToString();
                        model.useralbumphoto_photo = dr["useralbumphoto_photo"].ToString();
                        model.useralbumphoto_prise = Convert.ToInt32(dr["useralbumphoto_prise"]);
                        model.useralbumphoto_artist = dr["useralbumphoto_artist"].ToString();
                        model.user_albumid = Convert.ToInt32(dr["user_albumid"]);
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
            return model;
        }

        public List<PhotoModel> photolist()
        {

            List<PhotoModel> photo = new List<PhotoModel>();
            string query = String.Format("SELECT useralbumphoto_id,useralbumphoto_photo,useralbumphoto_name,useralbumphoto_prise FROM public.rm_useralbumphoto");
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PhotoModel mod = new PhotoModel();
                        mod.useralbumphoto_id = Convert.ToInt32(dr["useralbumphoto_id"]);
                        mod.useralbumphoto_photo = dr["useralbumphoto_photo"].ToString();
                        mod.useralbumphoto_name = dr["useralbumphoto_name"].ToString();
                        mod.useralbumphoto_prise = Convert.ToInt32(dr["useralbumphoto_prise"]);
                        photo.Add(mod);
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
            return photo;
        }

        public int DecreaseQuantity(int photo_id)
        {
            string query = "UPDATE public.rm_useralbumphoto SET quantity=quantity-1,sellquantity=sellquantity+1 WHERE useralbumphoto_id=@useralbumphoto_id";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@useralbumphoto_id", photo_id);
            int i = 0;
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

        public int IncreaseQuantity(int photo_id)
        {
            string query = "UPDATE public.rm_useralbumphoto SET quantity=quantity+1,sellquantity=sellquantity-1 WHERE useralbumphoto_id=@useralbumphoto_id";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@useralbumphoto_id", photo_id);
            int i = 0;
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

        public int totalPrise(string user_email)
        {
            int total_prise = 0;
            string query = "select sum(rm_useralbumphoto.useralbumphoto_prise * rm_cart_item.cart_itemquantity) as total_prise from rm_useralbumphoto left outer join rm_cart_item on rm_useralbumphoto.useralbumphoto_id = rm_cart_item.useralbumphoto_id where user_id =(SELECT user_id FROM public.rm_userdetail WHERE user_email=@user_email)";
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
                        total_prise = Convert.ToInt32(dr["total_prise"]);
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
            return total_prise;
        }

        public int UpdatesellQuantity(int quantity,int photo_id)
        {
            string query = "UPDATE public.rm_useralbumphoto SET sellquantity=sellquantity+@sellquantity WHERE useralbumphoto_id=@useralbumphoto_id";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@useralbumphoto_id", photo_id);
            cmd.Parameters.AddWithValue("@sellquantity", quantity);
            int i = 0;
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

        public PhotoViewModel PhotoDetailAdmin(int photo_id)
        {
            PhotoViewModel model = new PhotoViewModel();
            string query = String.Format("Select * FROM rm_useralbumphoto WHERE useralbumphoto_id={0}", photo_id);
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.useralbumphoto_id = Convert.ToInt32(dr["useralbumphoto_id"]);
                        model.useralbumphoto_name = dr["useralbumphoto_name"].ToString();
                        model.useralbumphoto_photo = dr["useralbumphoto_photo"].ToString();
                        model.useralbumphoto_prise = Convert.ToInt32(dr["useralbumphoto_prise"]);
                        model.useralbumphoto_artist = dr["useralbumphoto_artist"].ToString();
                        model.user_albumid = Convert.ToInt32(dr["user_albumid"]);
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
            return model;
        }

        public List<UserCheckoutModel> ImageSell()
        {
            List<UserCheckoutModel> mod = new List<UserCheckoutModel>();
            string query = "select total_prise,ship_date from rm_userchekout";
            con.Open();
            cmd = new NpgsqlCommand(query,con);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        UserCheckoutModel model = new UserCheckoutModel();
                        model.total_prise = Convert.ToInt32(dr["total_prise"]);
                        model.ship_date = Convert.ToDateTime(dr["ship_date"]);
                        mod.Add(model);
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
            return mod;
        }
    }
}
