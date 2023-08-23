using Microsoft.EntityFrameworkCore;
using Travelista.Data;
using Travelista.Models;
using Travelista.repository.Interface;

namespace Travelista.repository.ClassImplmentation
{
    public class Recommend : IRecommendation
    {
        private readonly ApplicationDbContext _context;
        public Recommend(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Recommendation>> GetRecommendations(int? id)
        {
            List<Recommendation> recommendations = new List<Recommendation>();
            if (id != null)
            {
                recommendations = await _context.Recommendations.Where(r => r.City_Id == id).ToListAsync();
                return recommendations;
            }
            else
            {
                return null;
            }

        }


    }
}
