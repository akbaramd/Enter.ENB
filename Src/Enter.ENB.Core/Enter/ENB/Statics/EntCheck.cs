using System.Diagnostics;
using System.Runtime.CompilerServices;
using Enter.ENB.Extensions;

namespace Enter.ENB.Statics;

[DebuggerStepThrough]
  public static class EntCheck
  {
    public static T NotNull<T>(T? value, string parameterName) => (object) value != null ? value : throw new ArgumentNullException(parameterName);

    public static T NotNull<T>(T? value, string parameterName, string message) => (object) value != null ? value : throw new ArgumentNullException(parameterName, message);

    public static string NotNull(string? value, string parameterName, int maxLength = 2147483647, int minLength = 0)
    {
      if (value == null)
        throw new ArgumentException(parameterName + " can not be null!", parameterName);
      if (value.Length > maxLength)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 2);
        interpolatedStringHandler.AppendFormatted(parameterName);
        interpolatedStringHandler.AppendLiteral(" length must be equal to or lower than ");
        interpolatedStringHandler.AppendFormatted<int>(maxLength);
        interpolatedStringHandler.AppendLiteral("!");
        throw new ArgumentException(interpolatedStringHandler.ToStringAndClear(), parameterName);
      }
      if (minLength > 0 && value.Length < minLength)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 2);
        interpolatedStringHandler.AppendFormatted(parameterName);
        interpolatedStringHandler.AppendLiteral(" length must be equal to or bigger than ");
        interpolatedStringHandler.AppendFormatted<int>(minLength);
        interpolatedStringHandler.AppendLiteral("!");
        throw new ArgumentException(interpolatedStringHandler.ToStringAndClear(), parameterName);
      }
      return value;
    }

    public static string NotNullOrWhiteSpace(
      string? value,
      string parameterName,
      int maxLength = 2147483647,
      int minLength = 0)
    {
      if (StringExtensions.IsNullOrWhiteSpace(value))
        throw new ArgumentException(parameterName + " can not be null, empty or white space!", parameterName);
      if (value.Length > maxLength)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 2);
        interpolatedStringHandler.AppendFormatted(parameterName);
        interpolatedStringHandler.AppendLiteral(" length must be equal to or lower than ");
        interpolatedStringHandler.AppendFormatted<int>(maxLength);
        interpolatedStringHandler.AppendLiteral("!");
        throw new ArgumentException(interpolatedStringHandler.ToStringAndClear(), parameterName);
      }
      if (minLength > 0 && value.Length < minLength)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 2);
        interpolatedStringHandler.AppendFormatted(parameterName);
        interpolatedStringHandler.AppendLiteral(" length must be equal to or bigger than ");
        interpolatedStringHandler.AppendFormatted<int>(minLength);
        interpolatedStringHandler.AppendLiteral("!");
        throw new ArgumentException(interpolatedStringHandler.ToStringAndClear(), parameterName);
      }
      return value;
    }

    public static string NotNullOrEmpty(
      string? value,
      string parameterName,
      int maxLength = 2147483647,
      int minLength = 0)
    {
      if (value.IsNullOrEmpty())
        throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);
      if (value.Length > maxLength)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 2);
        interpolatedStringHandler.AppendFormatted(parameterName);
        interpolatedStringHandler.AppendLiteral(" length must be equal to or lower than ");
        interpolatedStringHandler.AppendFormatted<int>(maxLength);
        interpolatedStringHandler.AppendLiteral("!");
        throw new ArgumentException(interpolatedStringHandler.ToStringAndClear(), parameterName);
      }
      if (minLength > 0 && value.Length < minLength)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 2);
        interpolatedStringHandler.AppendFormatted(parameterName);
        interpolatedStringHandler.AppendLiteral(" length must be equal to or bigger than ");
        interpolatedStringHandler.AppendFormatted<int>(minLength);
        interpolatedStringHandler.AppendLiteral("!");
        throw new ArgumentException(interpolatedStringHandler.ToStringAndClear(), parameterName);
      }
      return value;
    }

    public static ICollection<T> NotNullOrEmpty<T>(ICollection<T>? value, string parameterName) => value != null && value.Count > 0 ? value : throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);

    public static Type AssignableTo<TBaseType>(Type type, string parameterName)
    {
      EntCheck.NotNull<Type>(type, parameterName);
      if (!type.IsAssignableTo<TBaseType>())
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 3);
        interpolatedStringHandler.AppendFormatted(parameterName);
        interpolatedStringHandler.AppendLiteral(" (type of ");
        interpolatedStringHandler.AppendFormatted(type.AssemblyQualifiedName);
        interpolatedStringHandler.AppendLiteral(") should be assignable to the ");
        interpolatedStringHandler.AppendFormatted(typeof (TBaseType).GetFullNameWithAssemblyName());
        interpolatedStringHandler.AppendLiteral("!");
        throw new ArgumentException(interpolatedStringHandler.ToStringAndClear());
      }
      return type;
    }

    public static string? Length(string? value, string parameterName, int maxLength, int minLength = 0)
    {
      if (minLength > 0)
      {
        if (string.IsNullOrEmpty(value))
          throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);
        if (value.Length < minLength)
        {
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 2);
          interpolatedStringHandler.AppendFormatted(parameterName);
          interpolatedStringHandler.AppendLiteral(" length must be equal to or bigger than ");
          interpolatedStringHandler.AppendFormatted<int>(minLength);
          interpolatedStringHandler.AppendLiteral("!");
          throw new ArgumentException(interpolatedStringHandler.ToStringAndClear(), parameterName);
        }
      }
      if (value != null && value.Length > maxLength)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 2);
        interpolatedStringHandler.AppendFormatted(parameterName);
        interpolatedStringHandler.AppendLiteral(" length must be equal to or lower than ");
        interpolatedStringHandler.AppendFormatted<int>(maxLength);
        interpolatedStringHandler.AppendLiteral("!");
        throw new ArgumentException(interpolatedStringHandler.ToStringAndClear(), parameterName);
      }
      return value;
    }

    public static short Positive(short value, string parameterName)
    {
      if (value == (short) 0)
        throw new ArgumentException(parameterName + " is equal to zero");
      return value >= (short) 0 ? value : throw new ArgumentException(parameterName + " is less than zero");
    }

    public static int Positive(int value, string parameterName)
    {
      if (value == 0)
        throw new ArgumentException(parameterName + " is equal to zero");
      return value >= 0 ? value : throw new ArgumentException(parameterName + " is less than zero");
    }

    public static long Positive(long value, string parameterName)
    {
      if (value == 0L)
        throw new ArgumentException(parameterName + " is equal to zero");
      return value >= 0L ? value : throw new ArgumentException(parameterName + " is less than zero");
    }

    public static float Positive(float value, string parameterName)
    {
      if ((double) value == 0.0)
        throw new ArgumentException(parameterName + " is equal to zero");
      return (double) value >= 0.0 ? value : throw new ArgumentException(parameterName + " is less than zero");
    }

    public static double Positive(double value, string parameterName)
    {
      if (value == 0.0)
        throw new ArgumentException(parameterName + " is equal to zero");
      return value >= 0.0 ? value : throw new ArgumentException(parameterName + " is less than zero");
    }

    public static Decimal Positive(Decimal value, string parameterName)
    {
      if (value == 0M)
        throw new ArgumentException(parameterName + " is equal to zero");
      return !(value < 0M) ? value : throw new ArgumentException(parameterName + " is less than zero");
    }

    public static short Range(
      short value,
      string parameterName,
      short minimumValue,
      short maximumValue = 32767)
    {
      if ((int) value < (int) minimumValue || (int) value > (int) maximumValue)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 3);
        interpolatedStringHandler.AppendFormatted(parameterName);
        interpolatedStringHandler.AppendLiteral(" is out of range min: ");
        interpolatedStringHandler.AppendFormatted<short>(minimumValue);
        interpolatedStringHandler.AppendLiteral(" - max: ");
        interpolatedStringHandler.AppendFormatted<short>(maximumValue);
        throw new ArgumentException(interpolatedStringHandler.ToStringAndClear());
      }
      return value;
    }

    public static int Range(int value, string parameterName, int minimumValue, int maximumValue = 2147483647)
    {
      if (value < minimumValue || value > maximumValue)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 3);
        interpolatedStringHandler.AppendFormatted(parameterName);
        interpolatedStringHandler.AppendLiteral(" is out of range min: ");
        interpolatedStringHandler.AppendFormatted<int>(minimumValue);
        interpolatedStringHandler.AppendLiteral(" - max: ");
        interpolatedStringHandler.AppendFormatted<int>(maximumValue);
        throw new ArgumentException(interpolatedStringHandler.ToStringAndClear());
      }
      return value;
    }

    public static long Range(
      long value,
      string parameterName,
      long minimumValue,
      long maximumValue = 9223372036854775807)
    {
      if (value < minimumValue || value > maximumValue)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 3);
        interpolatedStringHandler.AppendFormatted(parameterName);
        interpolatedStringHandler.AppendLiteral(" is out of range min: ");
        interpolatedStringHandler.AppendFormatted<long>(minimumValue);
        interpolatedStringHandler.AppendLiteral(" - max: ");
        interpolatedStringHandler.AppendFormatted<long>(maximumValue);
        throw new ArgumentException(interpolatedStringHandler.ToStringAndClear());
      }
      return value;
    }

    public static float Range(
      float value,
      string parameterName,
      float minimumValue,
      float maximumValue = 3.4028235E+38f)
    {
      if ((double) value < (double) minimumValue || (double) value > (double) maximumValue)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 3);
        interpolatedStringHandler.AppendFormatted(parameterName);
        interpolatedStringHandler.AppendLiteral(" is out of range min: ");
        interpolatedStringHandler.AppendFormatted<float>(minimumValue);
        interpolatedStringHandler.AppendLiteral(" - max: ");
        interpolatedStringHandler.AppendFormatted<float>(maximumValue);
        throw new ArgumentException(interpolatedStringHandler.ToStringAndClear());
      }
      return value;
    }

    public static double Range(
      double value,
      string parameterName,
      double minimumValue,
      double maximumValue = 1.7976931348623157E+308)
    {
      if (value < minimumValue || value > maximumValue)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 3);
        interpolatedStringHandler.AppendFormatted(parameterName);
        interpolatedStringHandler.AppendLiteral(" is out of range min: ");
        interpolatedStringHandler.AppendFormatted<double>(minimumValue);
        interpolatedStringHandler.AppendLiteral(" - max: ");
        interpolatedStringHandler.AppendFormatted<double>(maximumValue);
        throw new ArgumentException(interpolatedStringHandler.ToStringAndClear());
      }
      return value;
    }

    public static Decimal Range(
      Decimal value,
      string parameterName,
      Decimal minimumValue,
      Decimal maximumValue = 79228162514264337593543950335M)
    {
      if (value < minimumValue || value > maximumValue)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 3);
        interpolatedStringHandler.AppendFormatted(parameterName);
        interpolatedStringHandler.AppendLiteral(" is out of range min: ");
        interpolatedStringHandler.AppendFormatted<Decimal>(minimumValue);
        interpolatedStringHandler.AppendLiteral(" - max: ");
        interpolatedStringHandler.AppendFormatted<Decimal>(maximumValue);
        throw new ArgumentException(interpolatedStringHandler.ToStringAndClear());
      }
      return value;
    }

    public static T NotDefaultOrNull<T>(T? value, string parameterName) where T : struct
    {
      T obj1 = value.HasValue ? value.Value : throw new ArgumentException(parameterName + " is null!", parameterName);
      obj1 = default (T);
      T obj2 = default;
      if (obj1.Equals((object) obj2))
        throw new ArgumentException(parameterName + " has a default value!", parameterName);
      return value.Value;
    }
  }