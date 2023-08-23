using Travelista.Models;

namespace Travelista.repository.Interface
{
    public interface IRecommendation
    {
        Task<List<Recommendation>> GetRecommendations(int? id);
    }
}
