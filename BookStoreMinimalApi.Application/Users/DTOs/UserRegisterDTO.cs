using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMinimalApi.Application.Users.DTOs
{
    public class UserRegisterDTO
    {
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}