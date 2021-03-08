using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model;
namespace DataAccessLayer.Interface
{
   public interface IPhoto
    {
        int AddPhoto(PhotoModel model);
        int DeletePhoto(int photo_id);
        int EditPhoto(PhotoModel model);
        List<PhotoModel> AllPhoto();
        PhotoModel PhotoDetail(int photo_id);
        List<PhotoModel> photolist();
        int DecreaseQuantity(int photo_id);
        int IncreaseQuantity(int photo_id);
        int totalPrise(string user_email);
        int UpdatesellQuantity(int quantity,int photo_id);
        PhotoViewModel PhotoDetailAdmin(int photo_id);
        List<UserCheckoutModel> ImageSell();
    }
}
