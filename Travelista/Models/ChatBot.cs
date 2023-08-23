using System.ComponentModel.DataAnnotations.Schema;

namespace Travelista.Models
{
    public class ChatBot
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        [ForeignKey("hotel")]
        public int? Hotel_Id { get; set; }
        public Hotel hotel { get; set; }
    }
}
