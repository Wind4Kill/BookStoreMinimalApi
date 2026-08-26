
namespace BookStoreMinimalApi.Endpoints;

public record Filters(string? FilterOptions, string? OrderOptions,
string? FilterValue, int? PageNum);
