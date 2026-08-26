using System;
using System.ComponentModel.DataAnnotations;
using BookStoreMinimalApi.Domain.FiltrationEntities;

namespace BookStoreMinimalApi.Endpoints;

public record Filters(string? FilterOptions, string? OrderOptions,
string? FilterValue, int? PageNum);
