using Microsoft.EntityFrameworkCore;

namespace Application.Framework;

public record struct RequestResponse<T>(T Payload, bool IsSuccess = true, HttpStatusCode HttpStatusCode = HttpStatusCode.OK);
public record struct ErrorResponse(Dictionary<string, string[]>? Errors, string Type, string Title, int Status, string Message, string TraceId);

public record struct PagingResponse(bool HasNextPage, bool HasPreviousPage, int PageSize, int CurrentPage, int TotalData, int TotalPages);
public struct PaginationResult<T>
{
    #region properties
    public PagingResponse Paging { get; }
    public IEnumerable<T> Items { get; }

    #endregion

    private PaginationResult(List<T> items, int currentPage, int pageSize, int totalData)
    {
        var notZeroPageSize = pageSize == 0 ? 1 : pageSize;
        var totalPages = (totalData / notZeroPageSize) + ((totalData % notZeroPageSize) == 0 ? 0 : 1);
        Items = items;
        Paging = new PagingResponse(HasNextPage: (currentPage * pageSize) < totalData,
                                    HasPreviousPage: pageSize > 1,
                                    PageSize: pageSize,
                                    CurrentPage: currentPage,
                                    TotalData: totalData,
                                    TotalPages: totalPages);
    }

    public static async ValueTask<PaginationResult<T>> CreateAsync(IQueryable<T>? query, int currentPage, int pageSize, CancellationToken cancellationToken = default)
    {
        if (query is null || currentPage == 0 || pageSize == 0) return default;

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return new(items, currentPage, pageSize, totalCount);
    }
}
