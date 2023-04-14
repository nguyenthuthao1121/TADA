using TADA.Dto;

namespace TADA.Service
{
    public interface IReviewService
    {
        List<ReviewDto> GetReviewsByBookId(int bookId);
        int GetNumberOfStar(int bookId, int star);
        double GetAverageRating(int bookId);
    }
}
