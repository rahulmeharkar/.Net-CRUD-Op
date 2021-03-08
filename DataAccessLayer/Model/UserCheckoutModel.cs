using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class UserCheckoutModel
    {
        public int checkout_id { get; set; }
        [Required]
        [Display(Name ="First Name:")]
        public string first_name { get; set; }
        [Required]
        [Display(Name = "Last Name:")]
        public string last_name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Address:")]
        public string address { get; set; }
        [Required]
        [Display(Name = "State:")]
        public string state { get; set; }
        [DataType(DataType.PostalCode)]
        [Required]
        [Display(Name = "Pin Code:")]
        public int postalcode { get; set; }
        [Required]
        [Display(Name = "Country:")]
        public string country { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile Number")]
        public string phone { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ship Date")]
        public DateTime ship_date { get; set; }
        public int total_prise { get; set; }
        public int user_id { get; set; }
        public int cart_id { get; set; }
        public int order_number { get; set; }
    }
}
