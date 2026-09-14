using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStoreMinimalApi.Application.Interfaces.Abstractions;

namespace BookStoreMinimalApi.Data.Services
{
      public class JwtTokenProvider : ITokenProvider
      {
            public string GenerateToken(ClaimsPrincipal user)
            {
                 
            }
      }
}