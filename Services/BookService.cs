using Microsoft.Extensions.Caching.Memory;

public class BookService : IBookService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<BookService> _logger;
    private readonly IMemoryCache _cache;

    public BookService(IHttpClientFactory httpClientFactory, ILogger<BookService> logger, IMemoryCache cache)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _cache = cache;
    }
    // método SearchByAuthorAsync vendrá después
}