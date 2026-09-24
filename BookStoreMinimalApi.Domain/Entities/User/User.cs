using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace BookStoreMinimalApi.Domain.Entities.User
{
    public class User : IdentityUser
    {
        public User() : base() { }
        public User(string userName) : base(userName) { }
        public ICollection<Review> Reviews { get; set; } = null!;

        public ICollection<RefreshToken> RefreshTokens { get; set; } = null!;
    }
}