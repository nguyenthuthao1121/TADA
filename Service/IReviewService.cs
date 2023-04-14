using TADA.Dto.Review;

namespace TADA.Service
{
    public interface IReviewService
    {
        List<ReviewDto> GetReviewsByBookId(int bookId);
    }
}
