using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStoreMinimalApi.Application.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BookStoreMinimalApi.Application.Interfaces.Abstractions.Authorization
{
    public interface ITokenProvider
    {
        public string GenerateToken(IdentityUser user);
    }
}