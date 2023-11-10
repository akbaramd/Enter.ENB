namespace Enter.ENB.DDD.Application.Dtos;

public interface IListResult<T>
{
    /// <summary>
    /// List of items.
    /// </summary>
    IReadOnlyList<T> Items { get; set; }
}