using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travelista.Models
{
    public class Recommendation
    {
        public int ID { get; set; }
        [ForeignKey("City")]
        [Display(Name ="City")]
        public int City_Id { get; set; }
        public City? City { get; set; }
      public string? Description { get; set; }
        [Display(Name ="Image")]
        
    public string? Image_Url { get; set; }



    }
}
