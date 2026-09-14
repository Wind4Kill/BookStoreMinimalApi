using AutoMapper;
using BookStoreMinimalApi.Application.Reviews.DTOs;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Application.MappingProfiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDto>().ForMember(dest => dest.Description, opt => opt.MapFrom(r => r.Description))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(r => r.Rating));
        }
    }
}