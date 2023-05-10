using TADA.Dto.Review;
using TADA.Repository;
using TADA.Repository.Implement;

namespace TADA.Service.Implement
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IOrderRepository orderRepository;
        public ReviewService(IReviewRepository reviewRepository, IOrderRepository orderRepository)
        {
            this.reviewRepository = reviewRepository;
            this.orderRepository = orderRepository;
        }

        public int GetNumberOfStar(int bookId, int star)
        {
            return reviewRepository.GetNumberOfStar(bookId, star);
        }

        public List<ReviewDto> GetReviewsByBookId(int bookId)
        {
            return reviewRepository.GetReviewsByBookId(bookId);
        }
        public int AddReview(ReviewDto reviewDto)
        {
            return reviewRepository.AddReview(reviewDto);
        }
        public List<ReviewDto> GetReviewsByOrderId(int orderId)
        {
            return reviewRepository.GetReviewsByBookId(orderId);
        }
        public List<int> GetBookReviewInOrder(int orderId)
        {
            var listReview = reviewRepository.GetReviewsByOrderId(orderId);
            List<int> books = new List<int>();
            foreach(var review in listReview)
            {
                books.Add(review.BookId);
            }
            return books;
        }
        public bool OrderIsReviewed(int orderId)
        {
            if (reviewRepository.GetReviewsByOrderId(orderId).Count() == orderRepository.GetOrderDetailsByOrderId(orderId).Count())
                return true;
            return false;
        }
        public ReviewDto GetReview(int orderId, int bookId)
        {
            return reviewRepository.GetReview(orderId, bookId);
        }
    }
}
