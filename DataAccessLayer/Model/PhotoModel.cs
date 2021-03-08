using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class PhotoModel
    {
        public int useralbumphoto_id { get; set; }
        public string useralbumphoto_photo { get; set; }
        public int user_albumid { get; set; }
        public string useralbumphoto_artist { get; set; }
        public string useralbumphoto_name { get; set; }
        public int useralbumphoto_prise { get; set; }
        public string album_categary { get; set; }
        public string user_email { get; set; }
        public int quantity { get; set; }
        public int sellquantity { get; set; }
        public int cart_itemquantity { get; set; }
    }
}
