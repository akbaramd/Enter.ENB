using System.ComponentModel.DataAnnotations;

namespace Enter.ENB.Validation;

public interface IHasValidationErrors
{
    IList<ValidationResult> ValidationErrors { get; }
}
