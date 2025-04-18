namespace FibonacciApi.Helpers
{
    public static class CollectionExtensions
    {
        // Generic extension method to check for null or empty
        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
        {
            return source == null || !source.Any();
        }
    }
}
