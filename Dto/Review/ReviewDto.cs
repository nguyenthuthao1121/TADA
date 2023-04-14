namespace TADA.Dto.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime DateReview { get; set; }
        public string Image { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int BookId { get; set; }

    }
}
