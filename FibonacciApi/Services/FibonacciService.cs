using System.Collections.Concurrent;

namespace FibonacciApi.Services
{
    public class FibonacciResult
    {
        public List<long> Subsequence { get; set; } = new();
        public bool TimeoutOccurred { get; set; }
        public bool MemoryLimitExceeded { get; set; }
    }

    public interface IFibonacciService
    {
        Task<FibonacciResult> GenerateAsync(int start, int end, bool useCache, int timeoutMs, long maxMemoryBytes);
    }

    public class FibonacciService : IFibonacciService
    {
        private static ConcurrentDictionary<int, long> _cache = new();

        public async Task<FibonacciResult> GenerateAsync(int start, int end, bool useCache, int timeoutMs, long maxMemoryBytes)
        {
            var result = new FibonacciResult();
            var tempResults = new ConcurrentDictionary<int, long>();
            var cts = new CancellationTokenSource();
            var timeoutToken = cts.Token;

            var timeoutTask = Task.Delay(timeoutMs, timeoutToken);
            var tasks = new List<Task>();

            for (int i = start; i <= end; i++)
            {
                var index = i;
                var task = Task.Run(async () =>
                {
                    long fib;
                    if (useCache && _cache.TryGetValue(index, out fib)) { }
                    else
                    {
                        fib = ComputeFibonacci(index);
                        await Task.Delay(500); // artificial delay

                        if (useCache)
                            _cache.TryAdd(index, fib);
                    }

                    if (GC.GetTotalMemory(false) >= maxMemoryBytes)
                        result.MemoryLimitExceeded = true;

                    tempResults.TryAdd(index, fib);
                });

                tasks.Add(task);
            }

            var completed = await Task.WhenAny(Task.WhenAll(tasks), timeoutTask);

            if (completed == timeoutTask)
            {
                result.TimeoutOccurred = true;
                cts.Cancel();
            }

            result.Subsequence = tempResults
                .OrderBy(kv => kv.Key)
                .Select(kv => kv.Value)
                .ToList();

            return result;
        }

        private long ComputeFibonacci(int n)
        {
            if (n <= 1) return n;
            long a = 0, b = 1;
            for (int i = 2; i <= n; i++)
            {
                long temp = a + b;
                a = b;
                b = temp;
            }
            return b;
        }
    }
}
