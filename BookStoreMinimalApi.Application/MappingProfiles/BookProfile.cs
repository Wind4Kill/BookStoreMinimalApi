using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreMinimalApi.Application.Books.DTOs.BookDTOs;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;

namespace BookStoreMinimalApi.Application.MappingProfiles
{
    public class BookProfile:Profile
    {
        public BookProfile()
        {
            CreateMap<ChangeBookDto, Book>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
            CreateMap<Book, GetBookDTO>().ForMember(dest => dest.BookId, opt => opt.MapFrom(b => b.BookId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(b => b.Title))
            .ForMember(dest => dest.Cost, opt => opt.MapFrom(b => b.Cost))
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(b => b.Author.Name))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(c => c.Categories));

            CreateMap<Book, GetBookByIdDTO>().ForMember(dest => dest.BookId,
            opt => opt.MapFrom(b => b.BookId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(b => b.Title))
            .ForMember(dest => dest.Cost, opt => opt.MapFrom(b => b.Cost))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(b => b.Description))
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(b => b.Author.Name))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(b => b.Categories))
            .ForMember(dest => dest.Reviews, opt => opt.MapFrom(b => b.Reviews));

            CreateMap<ChangeBookDto, Book>().ForMember(dest => dest.Title, opt => opt.MapFrom(dto => dto.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(dto => dto.Description))
            .ForMember(dest => dest.Cost, opt => opt.MapFrom(dto => dto.Cost));
        }
    }
}