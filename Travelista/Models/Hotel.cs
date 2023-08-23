using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travelista.Models
{
    public class Hotel
    {
        public int ID { get; set; }
        [RegularExpression("^[a-zA-Z\\s\\-\\'\\&\\.]+$", ErrorMessage = "Please enter a valid hotel name.")]
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
        [RegularExpression("^[a-zA-Z0-9\\s\\-\\'\\&\\.\\,\\!\\?]+$", ErrorMessage = "Please enter a valid description.")]
        public string? Description { get; set; }
        [RegularExpression("^[a-zA-Z0-9\\s\\-\\'\\#\\.]+$", ErrorMessage = "Please enter a valid address.")]
        public string Address { get; set; }
        [ForeignKey("city")]
        [Display(Name ="City")]
        public int City_Id { get; set; }
        public City? city { get; set; } = null;
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "Please enter a valid Start Rating number.")]
        [Display(Name ="Start Rating")]

        public decimal? Star_Rating { get; set; }
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "Please enter a valid price number.")]
        [Display(Name = "Price Per Night")]
        public decimal? Price_Per_Night { get; set; }
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "Please enter a valid phone number.")]
        public string? PhoneNumber { get; set; }
        public List<Review>? reviews { get; set; }
        public List<Room>? Rooms { get; set; }
       public Images? images { get; set; }
        
        [Display(Name = "The number of rooms is  Single")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "Please enter a valid  Single room number.")]
        public int? SingleRoom_Number { get; set; }
        
        [Display(Name = "The number of rooms is Double")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "Please enter a valid Double room number.")]
        public int? DoubleRoom_Number { get; set; }

        [Display(Name = "The number of rooms is Triple")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "Please enter a valid Triple room number.")]
        public int? TripleRoom_Number { get; set; }
       

        
    }
}
