using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Application.Categories.DTOs.CategoryDTOs
{
    [AutoMap(typeof(Category))]
    public class CategoryDTO
    {
        public string CategoryName { get; set; } = null!;
    }
}