
using AutoMapper;
using BookStoreMinimalApi.Application.Categories.DTOs.CategoryDTOs;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Application.MappingProfiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>().ForMember(dest => dest.CategoryName,
            opt => opt.MapFrom(c => c.CategoryName));   
        }
    }
}