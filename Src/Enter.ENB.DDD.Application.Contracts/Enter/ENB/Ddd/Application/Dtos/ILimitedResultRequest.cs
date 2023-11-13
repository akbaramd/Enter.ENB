namespace Enter.ENB.Ddd.Application.Dtos;

public interface ILimitedResultRequest
{
    /// <summary>
    /// Maximum result count should be returned.
    /// This is generally used to limit result count on paging.
    /// </summary>
    int MaxResultCount { get; set; }
}