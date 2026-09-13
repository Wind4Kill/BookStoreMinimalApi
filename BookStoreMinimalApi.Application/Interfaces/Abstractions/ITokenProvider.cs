using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStoreMinimalApi.Application.Interfaces.Abstractions
{
    public interface ITokenProvider
    {
        public string GenerateToken(ClaimsPrincipal user);
    }
}