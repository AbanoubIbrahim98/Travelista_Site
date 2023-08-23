using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travelista.Models
{
    public class Room
    {
        public int ID { get; set; }
        [ForeignKey("hotel")]
        public int? Hotel_Id { get; set; }
        public Hotel? hotel { get; set; }
        [Display(Name ="Room Type")]
        public string Room_Type { get; set; }
        [Display(Name = "Price Per Night")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "Please enter a valid price number.")]
        public decimal Price_Per_Night { get; set; }
        public bool Status { get; set; }
        [Display(Name = "Bed Type")]
        public string? Bad_Type { get; set; }
        [RegularExpression("^[1-3]{1}$", ErrorMessage = "Please enter a valid Occupancy number.")]

        public int? Occupancy { get; set; }
    }
}
