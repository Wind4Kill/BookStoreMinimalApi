using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Application.Categories.DTOs.CategoryDTOs
{
    public class CategoryDTO
    {
        [Required]
        public string CategoryName { get; set; } = null!;
    }
}