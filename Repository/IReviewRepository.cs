using TADA.Dto.Review;

namespace TADA.Repository
{
    public interface IReviewRepository
    {
        List<ReviewDto> GetReviewsByBookId(int bookId);
        int GetNumberOfStar(int bookId, int star);
        int AddReview(ReviewDto reviewDto);
    }
}
