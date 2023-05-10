﻿using Microsoft.Identity.Client;
using System.Data.SqlClient;
using System.Net;
using TADA.Dto.Review;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository.Implement
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly TadaContext context;
        public ReviewRepository(TadaContext context)
        {
            this.context = context;
        }

        public int GetNumberOfStar(int bookId, int star)
        {
            var reviews = GetReviewsByBookId(bookId);
            return reviews.Where(p => p.Rating == star).Count();
        }

        public List<ReviewDto> GetReviewsByBookId(int bookId)
        {
            var reviews = context.Reviews.Join(context.Books, review => review.BookId, book => book.Id,
                (review, book) => new { reviews = review, books = book })
                .Join(context.Customers, reviewBook => reviewBook.reviews.CustomerId, customer => customer.Id,
                (reviewBook, customer) => new ReviewDto
                {
                    Id = reviewBook.reviews.Id,
                    Comment = reviewBook.reviews.Comment,
                    Rating = reviewBook.reviews.Rating,
                    DateReview = reviewBook.reviews.DateReview,
                    Image = reviewBook.reviews.Image,
                    CustomerId = reviewBook.reviews.CustomerId,
                    CustomerName = customer.Name,
                    BookId = Convert.ToInt32(reviewBook.reviews.BookId)
                }).ToList().Where(review => review.BookId == bookId).ToList();
            return reviews;
        }
        public int AddReview(ReviewDto reviewDto)
        {
            try
            {
                context.Reviews.Add(new Review
                {
                    Comment = reviewDto.Comment,
                    Rating = reviewDto.Rating,
                    DateReview = reviewDto.DateReview,
                    Image = reviewDto.Image,
                    CustomerId = reviewDto.CustomerId,
                    BookId = reviewDto.BookId,
                });
                context.SaveChanges();
                return context.Reviews.Max(review => review.Id);
            }
            catch(SqlException)
            {
                return 0;
            }
        }
    }
}
