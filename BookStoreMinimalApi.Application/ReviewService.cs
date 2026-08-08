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
        public async Task<ReviewDto> AddReview(int bookId, ReviewDto reviewDto, CancellationToken cancellationToken)
        {
            Review createdReview = _mapper.Map<Review>(reviewDto);
            createdReview.BookId = bookId;
            createdReview = await _reviewRepository.AddReview(createdReview, cancellationToken);
            ReviewDto mappedReview = _mapper.Map<ReviewDto>(createdReview);
            return mappedReview;
           
        }
    }
}