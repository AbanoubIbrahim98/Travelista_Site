using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Travelista.Data;
using Travelista.Models;
using Travelista.repository.Interface;

namespace Travelista.repository.ClassImplmentation
{
    public class RoomAvalible : IRoomToBooking
    {
        ApplicationDbContext DbContext;
        ClaimsPrincipal User;
        public RoomAvalible(ApplicationDbContext Context)
        {
            DbContext = Context;
        }



        public async Task<List<Room>> GetRoomsAvalible(int Hotel_id)
        {
            return await DbContext.Rooms.Where(r => r.Hotel_Id == Hotel_id && r.Status == false).ToListAsync();
        }

    }
}
