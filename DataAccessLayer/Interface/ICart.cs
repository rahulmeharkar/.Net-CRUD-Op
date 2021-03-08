using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model;
namespace DataAccessLayer.Interface
{
    public interface ICart
    {
        int registerCartUser(int user_id);
        int addCart(int photo_id,int id,int quantity);
        int removeItem(int id,int photo_id);
        List<PhotoModel> cartItem(string user_email);
        //bool check(string user_email);
        int totalPrise(int user_id, int photo_id);
        int CheckOut(UserCheckoutModel model);
        int getCartId(int id);
        int IncreeaseCartItem(int user_id);
        int DecreesCartItem(int user_id,int photo_id);
        UserCheckoutModel OrderDetails(int user_id);
        int DeleteAllCartItem(int user_id);
        int updateprise(int photo_id,int user_id);
        PhotoModel detail(int photo_id,int user_id);
        bool CheckAlreadyPresent(int photo_id,int user_id);
        int updatecart(int photo_id, int id, int quantity);
    }
}
