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
            CreateMap<ChangeBookDto, Book>().ForMember(b => b.Cost, opts => opts.Condition(dto => dto.Cost is not 0 && dto.Cost > 0))
            .ForMember(b => b.Description, opts => opts.Condition(dto => dto.Description is not null && dto.Description != "string"))
            .ForMember(b => b.Title, opts => opts.Condition(dto => dto.Title is not null && dto.Title != "string"));
        }
    }
}