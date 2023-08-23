using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travelista.Models
{
    public class City
    {
        public int ID { get; set; }
        [MinLength(4)]
        [Required]
        [RegularExpression("^[a-zA-Z\\s]*$", ErrorMessage = "Please enter a valid city name.")]
        public string Name { get; set; }
        [ForeignKey("country")]
        [Display(Name ="Country")]
        public int? Country_Id { get; set; }
        public Country? country { get; set; }
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "Please enter a valid population number.")]
        public int? Population { get; set; }
        [Display(Name ="Image")]
        public string? Image_Url { get; set; }
      
    }
}
