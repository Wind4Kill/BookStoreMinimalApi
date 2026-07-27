using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMinimalApi.Domain.Interfaces.Repositories
{
    public interface IValidator<T>
    {
        public Dictionary<string,string> Validate(T forValidation);
    }
}