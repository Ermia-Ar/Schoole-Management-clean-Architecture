namespace Core.Application.Wrapper
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
            where T : class
        {
            if (source == null)
            {
                throw new Exception("Empty");
            }
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            int count = source.Count();
            if (count == 0)
                return PaginatedResult<T>.Success(new List<T>(), count, pageNumber, pageSize);

            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            await Task.CompletedTask;
            return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
        }
    }
}
