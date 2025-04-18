using FibonacciApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using FibonacciApi.Services;

namespace FibonacciApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FibonacciController : ControllerBase
    {
        private readonly IFibonacciService _fibonacciService;

        public FibonacciController(IFibonacciService fibonacciService)
        {
            _fibonacciService = fibonacciService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            int startIndex,
            int endIndex,
            bool useCache,
            int timeoutMs,
            long maxMemoryBytes)
        {
            try
            {
                var result = await _fibonacciService.GenerateAsync(startIndex, endIndex, useCache, timeoutMs, maxMemoryBytes);

                if (result.Subsequence.IsNullOrEmpty())
                {
                    return NotFound("No Fibonacci numbers found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // This shows you are handling errors
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
