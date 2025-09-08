using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Application.Contracts;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.Review;

namespace Company.Project.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }



        public async Task<IEnumerable<ReviewResponseDto>> GetProductReviewsAsync(int productId)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetProductreviewsbyidAsync(productId);
            return reviews.Select(r => new ReviewResponseDto
            {
                Id = r.Id,
                Comment = r.Comment,
                Rating = r.Rating,
                UserName = r.User.UserName
            });
        }

        public async Task AddReviewAsync(int productId, ReviewDto dto, string userId)
        {
            var review = new Review
            {
                ProductId = productId,
                Comment = dto.Comment,
                Rating = dto.Rating,
                UserId = userId
            };

            await _unitOfWork.ReviewRepository.AddAsync(review);
            await _unitOfWork.ReviewRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteReviewAsync(int reviewId, string userId)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(reviewId);
            if (review == null || review.UserId != userId)
                return false;

            _unitOfWork.ReviewRepository.DeleteAsync(review);
            await _unitOfWork.ReviewRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateReviewAsync(int reviewId, ReviewDto dto, string userId)
        {
            var review =  _unitOfWork.ReviewRepository.GetByIdAsync(reviewId).Result;
            if (review == null || review.UserId != userId)
            {
                await Task.FromResult(false);
                return false;
            }
            review.Comment = dto.Comment;
            review.Rating = dto.Rating;
            await _unitOfWork.ReviewRepository.UpdateAsync(review);
            await _unitOfWork.Completeasync();
            await Task.FromResult(true);
            return true; 
        }
        public Task<IEnumerable<ReviewResponseDto>> GetAllReviewsAsync(int skip, int take)
        {
            var reviews =  _unitOfWork.ReviewRepository.GetAllAsync().Result.Skip(skip).Take(take);
            return Task.FromResult(reviews.Select(r => new ReviewResponseDto
            {
                Id = r.Id,
                Comment = r.Comment,
                Rating = r.Rating,
                UserName = r.User.UserName
            }));
        }
        public Task<ReviewResponseDto?> GetReviewByIdAsync(int reviewId)
        {
            var review =  _unitOfWork.ReviewRepository.GetByIdAsync(reviewId).Result;
            if (review == null)
                return Task.FromResult<ReviewResponseDto?>(null);
            return Task.FromResult<ReviewResponseDto?>(new ReviewResponseDto
            {
                Id = review.Id,
                Comment = review.Comment,
                Rating = review.Rating,
                UserName = review.User.UserName
            });
        }
    }
}
