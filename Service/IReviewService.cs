using TADA.Dto.Review;

namespace TADA.Service
{
    public interface IReviewService
    {
        List<ReviewDto> GetReviewsByBookId(int bookId);
        int GetNumberOfStar(int bookId, int star);
        int AddReview(ReviewDto reviewDto);
        List<ReviewDto> GetReviewsByOrderId(int orderId);
        List<int> GetBookReviewInOrder(int orderId);
        bool OrderIsReviewed(int orderId);
        ReviewDto GetReview(int orderId, int bookId);
    }
}
