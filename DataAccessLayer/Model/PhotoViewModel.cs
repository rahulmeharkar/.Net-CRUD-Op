using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class PhotoViewModel
    {
        public int useralbumphoto_id { get; set; }
        public string useralbumphoto_photo { get; set; }
        public int user_albumid { get; set; }
        public string useralbumphoto_artist { get; set; }
        public string useralbumphoto_name { get; set; }
        public int useralbumphoto_prise { get; set; }
    }
}
