using TADA.Dto;

namespace TADA.Service
{
    public interface IReviewService
    {
        List<ReviewDto> GetReviewsByBookId(int bookId);
    }
}
