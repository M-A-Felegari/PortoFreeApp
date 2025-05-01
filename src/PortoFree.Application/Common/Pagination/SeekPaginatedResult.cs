namespace PortoFree.Application.Common.Pagination;

public record SeekPaginatedResult<T>(IReadOnlyList<T> Items, int? NextCursor, bool HasNextPage);