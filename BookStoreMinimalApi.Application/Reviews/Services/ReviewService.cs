using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreMinimalApi.Application.Exceptions;
using BookStoreMinimalApi.Application.Interfaces.Repositories;
using BookStoreMinimalApi.Application.Interfaces.Services;
using BookStoreMinimalApi.Application.Reviews.DTOs;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.DTOs;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Application.Reviews.Services
{
    public class ReviewService : IReviewService
    {
        readonly IMapper _mapper;

        readonly IBookRepository _bookRepository;
        readonly IReviewRepository _reviewRepository;

        public ReviewService(IMapper mapper, IReviewRepository reviewRepository, IBookRepository bookRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _bookRepository = bookRepository;
        }
        public async Task<ReviewDto> AddReview(int bookId, ReviewDto reviewDto, CancellationToken cancellationToken)
        {
            Book? requestedBook = await _bookRepository.GetBookById(bookId, cancellationToken);
            if(requestedBook is null)
            {
                throw new EntityNotFoundException("Requested books wasn't found.");
            }
            Review createdReview = _mapper.Map<Review>(reviewDto);
            requestedBook.Reviews!.Add(createdReview);
            requestedBook.EvaluateRating();
            await _bookRepository.UpdateBook(cancellationToken);
            ReviewDto mappedReview = _mapper.Map<ReviewDto>(createdReview);
            return mappedReview;
           
        }
    }
}