using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreMinimalApi.Application.Exceptions;
using BookStoreMinimalApi.Application.Interfaces.Abstractions;
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

        IUnitOfWork _unitOfWork;

        readonly IBookRepository _bookRepository;

        readonly IUserService _userService;

        readonly IReviewRepository _reviewRepository;

        public ReviewService(IMapper mapper, IUnitOfWork unitOfWork, IReviewRepository reviewRepository, IBookRepository bookRepository, IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _reviewRepository = reviewRepository;
            _bookRepository = bookRepository;
            _userService = userService;
        }
        public async Task<ReviewDto> AddReview(int bookId, ReviewDto reviewDto, ClaimsPrincipal claims, CancellationToken cancellationToken)
        {
            Book? requestedBook = await _bookRepository.GetBookById(bookId, cancellationToken);
            if (requestedBook is null)
            {
                throw new EntityNotFoundException("Requested books wasn't found.");
            }
            Review createdReview = _mapper.Map<Review>(reviewDto);
            requestedBook.Reviews!.Add(createdReview);
            requestedBook.EvaluateRating();

            await _userService.AddReviewToUser(claims, createdReview);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            ReviewDto mappedReview = _mapper.Map<ReviewDto>(createdReview);
            return mappedReview;

        }
    }
}