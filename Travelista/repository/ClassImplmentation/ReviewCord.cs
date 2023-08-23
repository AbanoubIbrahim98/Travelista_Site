using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Travelista.Data;
using Travelista.Models;
using Travelista.repository.Interface;

namespace Travelista.repository.ClassImplmentation
{
    public class ReviewCord : IReview
    {
        ApplicationDbContext DbContext;
        SignInManager<IdentityUser> signInManager;


        public ReviewCord(ApplicationDbContext dbContext, SignInManager<IdentityUser> sign)
        {
            DbContext = dbContext;
            signInManager = sign;


        }



        public void Create(string user_id, string comment, int hotalId)
        {


            var Review = new Review();
            Review.User_id = user_id;
            Review.Hotel_id = hotalId;
            Review.Comment = comment;
            DbContext.Reviews.Add(Review);
            DbContext.SaveChanges();




        }

        public void Delete(int? id)
        {
            if (id != null)
            {
                var review = DbContext.Reviews.Find(id);
                DbContext.Reviews.Remove(review);
            }

        }


        public void Update(int? id, string comment)
        {
            if (id != null)
            {
                var review = DbContext.Reviews.Find(id);
                if (review != null)
                {
                    review.Comment = comment;
                    DbContext.SaveChanges();
                }
            }
        }
    }
}
