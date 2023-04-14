﻿using TADA.Dto;

namespace TADA.Repository
{
    public interface IReviewRepository
    {
        List<ReviewDto> GetReviewsByBookId(int bookId);
        double GetAverageRating(int bookId);
        int GetNumberOfStar(int bookId, int star);
    }
}
