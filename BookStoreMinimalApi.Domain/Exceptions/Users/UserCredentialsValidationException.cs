using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMinimalApi.Domain.Exceptions.Users
{
    public class UserCredentialsValidationException:Exception
    {
        public UserCredentialsValidationException (string message):base(message) {}
    }
}