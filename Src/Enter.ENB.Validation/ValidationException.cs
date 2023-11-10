using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using Enter.ENB.Core;
using Enter.ENB.Core.Exceptions;
using Enter.ENB.Logging;
using Microsoft.Extensions.Logging;

namespace Enter.ENB.Validation;

[Serializable]
  public class EntValidationException : 
    EntException,
    IHasLogLevel,
    IHasValidationErrors,
    IExceptionWithSelfLogging
  {
    /// <summary>
    /// Detailed list of validation errors for this exception.
    /// </summary>
    public IList<ValidationResult> ValidationErrors { get; }

    /// <summary>
    /// Exception severity.
    /// Default: Warn.
    /// </summary>
    public LogLevel LogLevel { get; set; }

    /// <summary>Constructor.</summary>
    public EntValidationException()
    {
      this.ValidationErrors = (IList<ValidationResult>) new List<ValidationResult>();
      this.LogLevel = LogLevel.Warning;
    }

    /// <summary>Constructor for serializing.</summary>
    public EntValidationException(SerializationInfo serializationInfo, StreamingContext context)
      : base(serializationInfo, context)
    {
      this.ValidationErrors = (IList<ValidationResult>) new List<ValidationResult>();
      this.LogLevel = LogLevel.Warning;
    }

    /// <summary>Constructor.</summary>
    /// <param name="message">Exception message</param>
    public EntValidationException(string message)
      : base(message)
    {
      this.ValidationErrors = (IList<ValidationResult>) new List<ValidationResult>();
      this.LogLevel = LogLevel.Warning;
    }

    /// <summary>Constructor.</summary>
    /// <param name="validationErrors">Validation errors</param>
    public EntValidationException(IList<ValidationResult> validationErrors)
    {
      this.ValidationErrors = validationErrors;
      this.LogLevel = LogLevel.Warning;
    }

    /// <summary>Constructor.</summary>
    /// <param name="message">Exception message</param>
    /// <param name="validationErrors">Validation errors</param>
    public EntValidationException(string message, IList<ValidationResult> validationErrors)
      : base(message)
    {
      this.ValidationErrors = validationErrors;
      this.LogLevel = LogLevel.Warning;
    }

    /// <summary>Constructor.</summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public EntValidationException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.ValidationErrors = (IList<ValidationResult>) new List<ValidationResult>();
      this.LogLevel = LogLevel.Warning;
    }

    public void Log(ILogger logger)
    {
      if (this.ValidationErrors.IsNullOrEmpty<ValidationResult>())
        return;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("There are " + this.ValidationErrors.Count.ToString() + " validation errors:");
      foreach (ValidationResult validationError in (IEnumerable<ValidationResult>) this.ValidationErrors)
      {
        string str = "";
        if (validationError.MemberNames != null && validationError.MemberNames.Any<string>())
          str = " (" + string.Join(", ", validationError.MemberNames) + ")";
        stringBuilder.AppendLine(validationError.ErrorMessage + str);
      }
      logger.LogWithLevel(this.LogLevel, stringBuilder.ToString());
    }
  }