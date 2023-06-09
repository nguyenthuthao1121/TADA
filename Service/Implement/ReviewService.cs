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
            try
            {
                return reviewRepository.GetNumberOfStar(bookId, star);
            }
            catch(Exception)
            {
                return 0;
            }
        }

        public List<ReviewDto> GetReviewsByBookId(int bookId)
        {
            try
            {
                return reviewRepository.GetReviewsByBookId(bookId);
            }
            catch (Exception)
            {
                return new List<ReviewDto>();
            }
            
        }
        public int AddReview(ReviewDto reviewDto)
        {
            try
            {
                return reviewRepository.AddReview(reviewDto);
            }
            catch (Exception)
            {
                return 0;
            }
            
        }
        public void UpdateReviewImg(int reviewId, string img)
        {
            try
            {
                reviewRepository.UpdateReviewImg(reviewId, img);
            }
            catch (Exception)
            {
            }
            
        }
        public List<ReviewDto> GetReviewsByOrderId(int orderId)
        {
            try
            {
                return reviewRepository.GetReviewsByBookId(orderId);
            }
            catch (Exception)
            {
                return new List<ReviewDto>();
            }
            
        }
        public List<int> GetBookReviewInOrder(int orderId)
        {
            try
            {
                var listReview = reviewRepository.GetReviewsByOrderId(orderId);
                List<int> books = new List<int>();
                foreach (var review in listReview)
                {
                    books.Add(review.BookId);
                }
                return books;
            }
            catch (Exception)
            {
                return new List<int>();
            }
           
        }
        public bool OrderIsReviewed(int orderId)
        {
            try
            {
                if (reviewRepository.GetReviewsByOrderId(orderId).Count() == orderRepository.GetOrderDetailsByOrderId(orderId).Count())
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
        public ReviewDto GetReview(int orderId, int bookId)
        {
            try
            {
                return reviewRepository.GetReview(orderId, bookId);
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
