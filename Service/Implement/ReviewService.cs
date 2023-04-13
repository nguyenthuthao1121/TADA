using TADA.Dto;
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
        public List<ReviewDto> GetReviewsByBookId(int bookId)
        {
            return reviewRepository.GetReviewsByBookId(bookId);
        }

    }
}
