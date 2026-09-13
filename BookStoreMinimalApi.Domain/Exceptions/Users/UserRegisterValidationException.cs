using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMinimalApi.Domain.Exceptions.Users
{
    public class UserRegisterValidationException:Exception
    {
        public UserRegisterValidationException(string message):base(message) {}
    }
}