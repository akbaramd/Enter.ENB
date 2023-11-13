using System.ComponentModel.DataAnnotations;

namespace Enter.ENB.Ddd.Application.Dtos;

/// <summary>
/// Simply implements <see cref="IPagedResultRequest"/>.
/// </summary>
[Serializable]
public class PagedResultRequestDto : LimitedResultRequestDto, IPagedResultRequest
{
    [Range(0, int.MaxValue)]
    public virtual int SkipCount { get; set; }
}

