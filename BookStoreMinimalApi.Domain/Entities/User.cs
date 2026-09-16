using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookStoreMinimalApi.Domain.Entities
{
    public class User : IdentityUser
    {
        public User() : base() { }
        public User(string userName) : base(userName) { }
        public ICollection<Review> Reviews { get; set; } = null!;
    }
}