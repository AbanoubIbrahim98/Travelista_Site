using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travelista.Models
{
    public class Review
    {
        public int ID { get; set; }
        public string Comment { get; set; }
        [ForeignKey("user")]
        public string? User_id { get; set; }
        public IdentityUser? user { get; set; }
        [ForeignKey("hotel")]
        public int? Hotel_id { get; set; }
        public Hotel? hotel { get; set; }
    }
}
