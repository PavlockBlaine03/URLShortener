using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URLShortener.Models;

namespace URLShortener.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class UrlShortenerController : ControllerBase
    {
        private readonly UrlShortenerContext _context;

        public UrlShortenerController(UrlShortenerContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> ShortenUrl([FromBody] string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl)) return BadRequest("Invalid URL");

            var shortUrl = GenerateShortUrl();
            var urlMapping = new UrlMapping { OriginalUrl = originalUrl, ShortUrl = shortUrl };

            _context.UrlMappings.Add(urlMapping);
            await _context.SaveChangesAsync();

            return Ok(new { ShortUrl = shortUrl });
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> RedirectToOriginalUrl(string shortUrl)
        {
            var urlMapping = await _context.UrlMappings
                .FirstOrDefaultAsync(x => x.ShortUrl == shortUrl);

            if (urlMapping == null) return NotFound();

            return Redirect(urlMapping.OriginalUrl);
        }

        private string GenerateShortUrl()
        {
            // Simple unique short URL generation logic
            return Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}
