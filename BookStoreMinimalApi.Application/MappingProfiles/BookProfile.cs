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
        }
    }
}