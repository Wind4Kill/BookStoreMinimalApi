using System.ComponentModel.DataAnnotations;
using BookStoreMinimalApi.Domain.Entities;
using AutoMapper;

namespace BookStoreMinimalApi.Domain.DTOs.CategoryDTOs
{
    [AutoMap(typeof(Category))]
    public class CategoryDTO
    {
        [Required]
        public string CategoryName { get; set; } = null!;
    }
}