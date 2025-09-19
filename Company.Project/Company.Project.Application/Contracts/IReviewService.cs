using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.DTO.DTO.Review;

namespace Company.Project.Application.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponseDto>> GetProductReviewsAsync(int productId);
        Task AddReviewAsync(int productId, ReviewDto dto, string userId);
        Task<bool> DeleteReviewAsync(int reviewId, string userId);
        Task<bool> UpdateReviewAsync(int reviewId, ReviewDto dto, string userId);
        Task<IEnumerable<ReviewResponseDto>> GetAllReviewsAsync(int skip, int take);
        Task<ReviewResponseDto?> GetReviewByIdAsync(int reviewId);
        Task<IEnumerable<ReviewResponseDto>> GetUserReviewsAsync(string userId);
    }
}
