using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkoutService.Shared
{
    public class PagedResult<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public bool HasPrevious => Page > 1;
        public bool HasNext => Page < TotalPages;

        public PagedResult(IEnumerable<T> items, int totalCount, int page, int pageSize)
        {
            if (page < 1) throw new ArgumentException("Page must be greater than 0", nameof(page));
            if (pageSize < 1) throw new ArgumentException("Page size must be greater than 0", nameof(pageSize));

            var list = items.ToList();
            Items = list.AsReadOnly();
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
            TotalPages = totalCount > 0 ? (int)Math.Ceiling(totalCount / (double)pageSize) : 0;
        }

        public static PagedResult<T> Create(IQueryable<T> source, int page, int pageSize)
        {
            var totalCount = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            return new PagedResult<T>(items, totalCount, page, pageSize);
        }
    }
}
