using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model;
using DataAccessLayer.Interface;
using Npgsql;
using System.Data;

namespace DataAccessLayer.Repo
{
    public class CartRepo : ICart
    {
        NpgsqlConnection con = null;
        NpgsqlCommand cmd = null;
        public CartRepo()
        {
            con = ConnectionString.Connection();
            cmd = new NpgsqlCommand();
        }
        public int addCart(int photo_id,int id,int quantity)
        {
            string query = "INSERT INTO public.rm_cart_item(user_id,useralbumphoto_id,cart_itemquantity) VALUES(@user_id,@useralbumphoto_id,@cart_itemquantity)";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user_id", id);
            cmd.Parameters.AddWithValue("@useralbumphoto_id",photo_id);
            cmd.Parameters.AddWithValue("@cart_itemquantity", quantity);
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
        public List<PhotoModel> cartItem(string user_email)
        {
            List<PhotoModel> photo = new List<PhotoModel>();
            string query = "SELECT rm_useralbumphoto.useralbumphoto_id,rm_useralbumphoto.useralbumphoto_photo,rm_useralbumphoto.useralbumphoto_name,rm_useralbumphoto.useralbumphoto_prise,rm_cart_item.cart_itemquantity FROM public.rm_useralbumphoto FULL OUTER JOIN public.rm_cart_item ON rm_useralbumphoto.useralbumphoto_id=rm_cart_item.useralbumphoto_id  WHERE user_id=(select user_id from public.rm_userdetail where user_email=@user_email)";
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
                        PhotoModel mod = new PhotoModel();
                        mod.useralbumphoto_id = Convert.ToInt32(dr["useralbumphoto_id"]);
                        mod.useralbumphoto_photo = dr["useralbumphoto_photo"].ToString();
                        mod.useralbumphoto_name = dr["useralbumphoto_name"].ToString();
                        mod.useralbumphoto_prise = Convert.ToInt32(dr["useralbumphoto_prise"]);
                        mod.cart_itemquantity = Convert.ToInt32(dr["cart_itemquantity"]);
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

        public int removeItem(int id,int photo_id)
        {
            int i = 0;
            string query = "DELETE FROM public.rm_cart_item WHERE useralbumphoto_id = @useralbumphoto_id AND user_id=@user_id";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@useralbumphoto_id", photo_id);
            cmd.Parameters.AddWithValue("@user_id", id);
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

        public int totalPrise(int user_id,int photo_id)
        {
            PhotoModel mod =new PhotoModel();
            string query = "select sum(rm_useralbumphoto.useralbumphoto_prise*rm_cart_item.cart_itemquantity) as total_prise from rm_useralbumphoto left outer join rm_cart_item on rm_useralbumphoto.useralbumphoto_id=rm_cart_item.useralbumphoto_id where user_id=@user_id";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.Parameters.AddWithValue("@useralbumphoto_id", photo_id);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                      mod.useralbumphoto_prise = Convert.ToInt32(dr["total_prise"]);
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
            return mod.useralbumphoto_prise;
        }

        public int CheckOut(UserCheckoutModel model)
        {
            string query = "INSERT INTO public.rm_userchekout(first_name,last_name,address,state,postalcode,country,phone,email,ship_date,total_prise,user_id,order_number) VALUES(@first_name,@last_name,@address,@state,@postalcode,@country,@phone,@email,@ship_date,@total_prise,@user_id,@order_number)";
            con.Open();
            DateTime date = Convert.ToDateTime(model.ship_date);
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@first_name", model.first_name);
            cmd.Parameters.AddWithValue("@last_name", model.last_name);
            cmd.Parameters.AddWithValue("@address", model.address);
            cmd.Parameters.AddWithValue("@state", model.state);
            cmd.Parameters.AddWithValue("@postalcode", model.postalcode);
            cmd.Parameters.AddWithValue("@country", model.country);
            cmd.Parameters.AddWithValue("@phone", model.phone);
            cmd.Parameters.AddWithValue("@email", model.email);
            cmd.Parameters.AddWithValue("@ship_date", date);
            cmd.Parameters.AddWithValue("@total_prise", model.total_prise);
            cmd.Parameters.AddWithValue("@user_id", model.user_id);
            //cmd.Parameters.AddWithValue("@cart_id", model.cart_id);
            cmd.Parameters.AddWithValue("@order_number", model.order_number);
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

        public int registerCartUser(int user_id)
        {
            string query = "INSERT INTO public.rm_cart(user_id) VALUES(@user_id)";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user_id", user_id);
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

        public int getCartId(int id)
        {
            CartModel mod = new CartModel();
            string query = "SELECT cart_id FROM public.rm_cart WHERE user_id=@user_id";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user_id", id);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                      mod.cart_id = Convert.ToInt32(dr["cart_id"]);
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
            return mod.cart_id;
        }

        public int IncreeaseCartItem(int user_id)
        {
            string query = "UPDATE public.rm_cart SET item_quantity=item_quantity+1 WHERE user_id=@user_id";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user_id", user_id);
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

        public int DecreesCartItem(int user_id,int photo_id)
        {
            string query = "UPDATE public.rm_cart_item SET cart_itemquantity=cart_itemquantity-1 WHERE useralbumphoto_id=@useralbumphoto_id AND user_id=@user_id";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@useralbumphoto_id", photo_id);
            cmd.Parameters.AddWithValue("@user_id", user_id);
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

        public UserCheckoutModel OrderDetails(int user_id)
        {
            string query = "SELECT order_number,total_prise FROM public.rm_userchekout WHERE user_id=@user_id";
            con.Open();
            cmd =new  NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user_id",user_id);
            UserCheckoutModel model = new UserCheckoutModel();
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        model.order_number = Convert.ToInt32(dr["order_number"]);
                        model.total_prise = Convert.ToInt32(dr["total_prise"]);
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
            return model;
        }

        public int DeleteAllCartItem(int user_id)
        {
            int i = 0;
            string query = "DELETE FROM public.rm_cart_item WHERE user_id = @user_id";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user_id", user_id);
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

        public int updateprise(int photo_id,int user_id)
        {
            string query = "UPDATE public.rm_cart_item SET cart_itemquantity=cart_itemquantity+1 WHERE useralbumphoto_id=@useralbumphoto_id AND user_id=@user_id";
            con.Open();
            cmd = new NpgsqlCommand(query,con);
            cmd.Parameters.AddWithValue("@useralbumphoto_id", photo_id);
            cmd.Parameters.AddWithValue("@user_id", user_id);
            int i = 0;
            try
            {
                i = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return i;
        }

        public PhotoModel detail(int photo_id, int user_id)
        {
            PhotoModel mod = new PhotoModel();
            string query = "select * from public.rm_cart_item where useralbumphoto_id=@useralbumphoto_id and user_id=@user_id";
            con.Open();
            cmd = new NpgsqlCommand(query,con);
            cmd.Parameters.AddWithValue("@useralbumphoto_id", photo_id);
            cmd.Parameters.AddWithValue("@user_id", user_id);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        mod.cart_itemquantity = Convert.ToInt32(dr["cart_itemquantity"]);
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

        public bool CheckAlreadyPresent(int photo_id,int user_id)
        {
            bool check= false;
            string query = "select useralbumphoto_id,user_id from rm_cart_item where useralbumphoto_id=@useralbumphoto_id and user_id=@user_id";
            con.Open();
            cmd = new NpgsqlCommand(query,con);
            cmd.Parameters.AddWithValue("@useralbumphoto_id",photo_id);
            cmd.Parameters.AddWithValue("@user_id", user_id);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    check = true;
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
            return check;
        }

        public int updatecart(int photo_id, int id, int quantity)
        {
            string query = "UPDATE public.rm_cart_item SET cart_itemquantity=cart_itemquantity+@cart_itemquantity WHERE useralbumphoto_id=@useralbumphoto_id AND user_id=@user_id";
            con.Open();
            cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@useralbumphoto_id", photo_id);
            cmd.Parameters.AddWithValue("@user_id", id);
            cmd.Parameters.AddWithValue("@cart_itemquantity", quantity);
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
    }
}
