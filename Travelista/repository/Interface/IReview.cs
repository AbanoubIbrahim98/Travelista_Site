using Travelista.Models;

namespace Travelista.repository.Interface
{
    public interface IReview
    {
        void Create(string user_id, string comment, int hotel_id);
        void Update(int? id, string comment);
        void Delete(int? id);

    }
}
