namespace Enter.ENB.Ddd.Application.Dtos;

[Serializable]
public class PagedResultDto<T> : ListResultDto<T>, IPagedResult<T>
{
    /// <inheritdoc />
    public long TotalCount { get; set; } //TODO: Can be a long value..?

    /// <summary>
    /// Creates a new <see cref="PagedResultDto{T}"/> object.
    /// </summary>
    public PagedResultDto()
    {

    }

    /// <summary>
    /// Creates a new <see cref="PagedResultDto{T}"/> object.
    /// </summary>
    /// <param name="totalCount">Total count of Items</param>
    /// <param name="items">List of items in current page</param>
    public PagedResultDto(long totalCount, IReadOnlyList<T> items)
        : base(items)
    {
        TotalCount = totalCount;
    }
}

