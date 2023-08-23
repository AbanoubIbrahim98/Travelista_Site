using System.ComponentModel.DataAnnotations.Schema;

namespace Travelista.Models
{
    public class Images
    {
        public int ID { get; set; }
        [ForeignKey("hotel")]
        public int? Hotel_Id { get; set; }
        public Hotel? hotel { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
        public string? Image5 { get; set; }



    }
}
