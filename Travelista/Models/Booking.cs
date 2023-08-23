using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travelista.Models
{
    public class Booking
    {
        public int ID { get; set; }
        [ForeignKey("user")]
        public string User_ID { get; set; }
        public IdentityUser? user { get; set; }
        [ForeignKey("room")]
        public int? Room_id { get; set; }
        public Room? room { get; set; }
        [Display(Name ="Check In Date")]
        public DateTime Check_In_Date { get; set; }
        [Display(Name = "Check Out Date")]

        public DateTime Check_Out_Date { get; set; }
        [Display(Name = "Number of guests")]
        public int Num_Guests { get; set; }
        [Display(Name = "Room Type")]

        public string Room_type { get; set; }
        [Display(Name = "Total Price")]

        public decimal Total_Price { get; set; }
    }
}
