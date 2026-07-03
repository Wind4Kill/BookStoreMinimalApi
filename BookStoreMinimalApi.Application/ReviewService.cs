using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreMinimalApi.Domain.DTOs;
using BookStoreMinimalApi.Domain.Entities;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;
using BookStoreMinimalApi.Domain.Interfaces.Services;

namespace BookStoreMinimalApi.Application
{
    public class ReviewService : IReviewService
    {
        readonly IMapper _mapper;
        readonly IReviewRepository _reviewRepository;

        public ReviewService(IMapper mapper, IReviewRepository reviewRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }
        public async Task<int> AddReview(int bookId, ReviewDto reviewDto)
        {
            Review review = _mapper.Map<Review>(reviewDto);
            review.BookId = bookId;
            return await _reviewRepository.AddReview(review);
        }
    }
}