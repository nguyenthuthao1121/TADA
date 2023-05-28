using Microsoft.Identity.Client;
using System.Data.SqlClient;
using System.Net;
using TADA.Dto.Order;
using TADA.Dto.Review;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository.Implement
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly TadaContext context;
        public ReviewRepository(TadaContext context)
        {
            this.context = context;
        }

        public int GetNumberOfStar(int bookId, int star)
        {
            var reviews = GetReviewsByBookId(bookId);
            return reviews.Where(p => p.Rating == star).Count();
        }

        public List<ReviewDto> GetReviewsByBookId(int bookId)
        {
            var reviews = context.Reviews.Join(context.Books, review => review.BookId, book => book.Id,
                (review, book) => new { reviews = review, books = book })
                .Join(context.Orders, reviewBook => reviewBook.reviews.OrderId, order => order.Id,
                (reviewBook, order) => new {reviewBook=reviewBook, order=order})
                .Join(context.Customers, reviewOrder=>reviewOrder.order.CustomerId, customer=>customer.Id,
                (reviewOrder,customer)=> new ReviewDto
                {
                    Id = reviewOrder.reviewBook.reviews.Id,
                    Comment = reviewOrder.reviewBook.reviews.Comment,
                    Rating = reviewOrder.reviewBook.reviews.Rating,
                    DateReview = reviewOrder.reviewBook.reviews.DateReview,
                    Image = reviewOrder.reviewBook.reviews.Image,
                    OrderId = reviewOrder.order.Id,
                    CustomerName = customer.Name,
                    BookId = Convert.ToInt32(reviewOrder.reviewBook.reviews.BookId)
                })
                .ToList().Where(review => review.BookId == bookId).ToList();
            return reviews;
        }
        public int AddReview(ReviewDto reviewDto)
        {
            try
            {
                context.Reviews.Add(new Review
                {
                    Comment = reviewDto.Comment,
                    Rating = reviewDto.Rating,
                    DateReview = reviewDto.DateReview,
                    Image = reviewDto.Image,
                    OrderId = reviewDto.OrderId,
                    BookId = reviewDto.BookId,
                });
                context.SaveChanges();
                return context.Reviews.Max(review => review.Id);
            }
            catch(SqlException)
            {
                return 0;
            }
        }
        public void UpdateReviewImg(int reviewId, string img)
        {
            try
            {
                var review = context.Reviews.Find(reviewId);
                review.Image= img;
                context.SaveChanges();
            }
            catch (SqlException)
            {
            }
        }
        public List<ReviewDto> GetReviewsByOrderId(int orderId)
        {
            var order=context.Orders.Find(orderId);
            var customer=context.Customers.Find(order.CustomerId);
            return context.Reviews.Where(review=>review .OrderId== orderId).Select(review=>new ReviewDto
            {
                Id = review.Id,
                Comment = review.Comment,
                Rating = review.Rating,
                DateReview = review.DateReview,
                Image = review.Image,
                OrderId = orderId,
                CustomerName = customer.Name,
                BookId = Convert.ToInt32(review.BookId)
            }).ToList();
        }
        public ReviewDto GetReview(int orderId, int bookId)
        {
            var order = context.Orders.Find(orderId);
            var customer = context.Customers.Find(order.CustomerId);
            return context.Reviews.Where(review => review.OrderId == orderId && review.BookId==bookId).Select(review => new ReviewDto
            {
                Id = review.Id,
                Comment = review.Comment,
                Rating = review.Rating,
                DateReview = review.DateReview,
                Image = review.Image,
                OrderId = orderId,
                CustomerName = customer.Name,
                BookId = Convert.ToInt32(review.BookId)
            }).FirstOrDefault();
        }
    }
}
