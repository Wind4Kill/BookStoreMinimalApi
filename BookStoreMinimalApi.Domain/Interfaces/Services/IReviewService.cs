using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMinimalApi.Domain.Interfaces.Services
{
    public interface IReviewService
    {
        Task<int> AddReview();
    }
}