using TADA.Dto.Review;
using TADA.Model;

namespace TADA.Repository.Implement
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly TadaContext context;
        public ReviewRepository(TadaContext context)
        {
            this.context = context;
        }

        public double GetAverageRating(int bookId)
        {
            var reviews = GetReviewsByBookId(bookId);
            return reviews.Average(p => p.Rating);
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
                .Join(context.Customers, reviewBook => reviewBook.reviews.CustomerId, customer => customer.Id,
                (reviewBook, customer) => new ReviewDto
                {
                    Id = reviewBook.reviews.Id,
                    Comment = reviewBook.reviews.Comment,
                    Rating = reviewBook.reviews.Rating,
                    DateReview = reviewBook.reviews.DateReview,
                    Image = reviewBook.reviews.Image,
                    CustomerId = reviewBook.reviews.CustomerId,
                    CustomerName = customer.Name,
                    BookId = Convert.ToInt32(reviewBook.reviews.BookId)
                }).ToList().Where(review => review.BookId == bookId).ToList();
            return reviews;
        }
    }
}
