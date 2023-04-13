using TADA.Dto;

namespace TADA.Repository
{
    public interface IReviewRepository
    {
        List<ReviewDto> GetReviewsByBookId(int bookId);
    }
}
