using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMinimalApi.Domain.Entities.User
{
    public class RefreshToken
    {
        public string RefreshTokenId { get; set; } = null!;

        public string Token { get; set; } = null!;

        public DateTime ExpirationUtc { get; set; }

        public string UserId { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}