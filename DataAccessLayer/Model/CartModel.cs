using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class CartModel
    {
        public int cart_id { get; set; }
        public int user_id { get; set; }
        public int useralbumphoto_id { get; set; }
        public int album_id { get; set; }
        public string user_email { get; set; }
        public int item_quantity { get; set; }
        public int cart_itemquantity { get; set; }
        public int prise { get; set; }
    }
}
