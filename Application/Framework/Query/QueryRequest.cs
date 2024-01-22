namespace Application.Framework;


//public Dictionary<string, object> Query { get; set; }
public record PaginationQuery(int PageSize, int CurrentPage, string? OrderBy, bool? IsAscending, string? OpenText, DateOnly? DateFrom, DateOnly? DateTo);