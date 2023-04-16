using TADA.Dto.Review;
using TADA.Repository;

namespace TADA.Service.Implement
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository reviewRepository;
        public ReviewService(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public int GetNumberOfStar(int bookId, int star)
        {
            return reviewRepository.GetNumberOfStar(bookId, star);
        }

        public List<ReviewDto> GetReviewsByBookId(int bookId)
        {
            return reviewRepository.GetReviewsByBookId(bookId);
        }
    }
}
