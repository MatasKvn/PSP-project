namespace POS_System.Business.Dtos;

public record PagedResponse<T>(int TotalCount, int PageSize, int PageNum, IEnumerable<T> Results);
