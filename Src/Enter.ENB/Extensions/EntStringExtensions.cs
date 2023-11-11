using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Enter.ENB.Statics;

namespace Enter.ENB.Extensions;

public static class EntStringExtensions
  {
    /// <summary>
    /// Adds a char to end of given string if it does not ends with the char.
    /// </summary>
    public static string EnsureEndsWith(this string str, char c, StringComparison comparisonType = StringComparison.Ordinal)
    {
      EntCheck.NotNull<string>(str, nameof (str));
      return str.EndsWith(c.ToString(), comparisonType) ? str : str + c.ToString();
    }

    /// <summary>
    /// Adds a char to beginning of given string if it does not starts with the char.
    /// </summary>
    public static string EnsureStartsWith(this string str, char c, StringComparison comparisonType = StringComparison.Ordinal)
    {
      EntCheck.NotNull<string>(str, nameof (str));
      return str.StartsWith(c.ToString(), comparisonType) ? str : c.ToString() + str;
    }

    /// <summary>
    /// Indicates whether this string is null or an System.String.Empty string.
    /// </summary>
    public static bool IsNullOrEmpty(this string? str) => string.IsNullOrEmpty(str);

    /// <summary>
    /// indicates whether this string is null, empty, or consists only of white-space characters.
    /// </summary>
    public static bool IsNullOrWhiteSpace(this string? str) => string.IsNullOrWhiteSpace(str);

    /// <summary>
    /// Gets a substring of a string from beginning of the string.
    /// </summary>
    /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="str" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">Thrown if <paramref name="len" /> is bigger that string's length</exception>
    public static string Left(this string str, int len)
    {
      EntCheck.NotNull<string>(str, nameof (str));
      if (str.Length < len)
        throw new ArgumentException("len argument can not be bigger than given string's length!");
      return str.Substring(0, len);
    }

    /// <summary>
    /// Converts line endings in the string to <see cref="P:System.Environment.NewLine" />.
    /// </summary>
    public static string NormalizeLineEndings(this string str) => str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);

    /// <summary>Gets index of nth occurrence of a char in a string.</summary>
    /// <param name="str">source string to be searched</param>
    /// <param name="c">Char to search in <paramref name="str" /></param>
    /// <param name="n">Count of the occurrence</param>
    public static int NthIndexOf(this string str, char c, int n)
    {
      EntCheck.NotNull<string>(str, nameof (str));
      int num = 0;
      for (int index = 0; index < str.Length; ++index)
      {
        if ((int) str[index] == (int) c && ++num == n)
          return index;
      }
      return -1;
    }

    /// <summary>
    /// Removes first occurrence of the given postfixes from end of the given string.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="postFixes">one or more postfix.</param>
    /// <returns>Modified string or the same string if it has not any of given postfixes</returns>
    public static string RemovePostFix(this string str, params string[] postFixes) => str.RemovePostFix(StringComparison.Ordinal, postFixes);

    /// <summary>
    /// Removes first occurrence of the given postfixes from end of the given string.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="comparisonType">String comparison type</param>
    /// <param name="postFixes">one or more postfix.</param>
    /// <returns>Modified string or the same string if it has not any of given postfixes</returns>
    public static string RemovePostFix(
      this string str,
      StringComparison comparisonType,
      params string[] postFixes)
    {
      if (str.IsNullOrEmpty() || ((ICollection<string>) postFixes).IsNullOrEmpty<string>())
        return str;
      foreach (string postFix in postFixes)
      {
        if (str.EndsWith(postFix, comparisonType))
          return str.Left(str.Length - postFix.Length);
      }
      return str;
    }

    /// <summary>
    /// Removes first occurrence of the given prefixes from beginning of the given string.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="preFixes">one or more prefix.</param>
    /// <returns>Modified string or the same string if it has not any of given prefixes</returns>
    public static string RemovePreFix(this string str, params string[] preFixes) => str.RemovePreFix(StringComparison.Ordinal, preFixes);

    /// <summary>
    /// Removes first occurrence of the given prefixes from beginning of the given string.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="comparisonType">String comparison type</param>
    /// <param name="preFixes">one or more prefix.</param>
    /// <returns>Modified string or the same string if it has not any of given prefixes</returns>
    public static string RemovePreFix(
      this string str,
      StringComparison comparisonType,
      params string[] preFixes)
    {
      if (str.IsNullOrEmpty() || ((ICollection<string>) preFixes).IsNullOrEmpty<string>())
        return str;
      foreach (string preFix in preFixes)
      {
        if (str.StartsWith(preFix, comparisonType))
          return str.Right(str.Length - preFix.Length);
      }
      return str;
    }


    /// <summary>Gets a substring of a string from end of the string.</summary>
    /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="str" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">Thrown if <paramref name="len" /> is bigger that string's length</exception>
    public static string Right(this string str, int len)
    {
      EntCheck.NotNull<string>(str, nameof (str));
      if (str.Length < len)
        throw new ArgumentException("len argument can not be bigger than given string's length!");
      return str.Substring(str.Length - len, len);
    }

    /// <summary>
    /// Uses string.Split method to split given string by given separator.
    /// </summary>
    public static string[] Split(this string str, string separator) => str.Split(new string[1]
    {
      separator
    }, StringSplitOptions.None);

    /// <summary>
    /// Uses string.Split method to split given string by given separator.
    /// </summary>
    public static string[] Split(this string str, string separator, StringSplitOptions options) => str.Split(new string[1]
    {
      separator
    }, options);

    /// <summary>
    /// Uses string.Split method to split given string by <see cref="P:System.Environment.NewLine" />.
    /// </summary>
    public static string[] SplitToLines(this string str) => str.Split(Environment.NewLine);

    /// <summary>
    /// Uses string.Split method to split given string by <see cref="P:System.Environment.NewLine" />.
    /// </summary>
    public static string[] SplitToLines(this string str, StringSplitOptions options) => str.Split(Environment.NewLine, options);

    /// <summary>Converts PascalCase string to camelCase string.</summary>
    /// <param name="str">String to convert</param>
    /// <param name="useCurrentCulture">set true to use current culture. Otherwise, invariant culture will be used.</param>
    /// <param name="handleAbbreviations">set true to if you want to convert 'XYZ' to 'xyz'.</param>
    /// <returns>camelCase of the string</returns>
    public static string ToCamelCase(
      this string str,
      bool useCurrentCulture = false,
      bool handleAbbreviations = false)
    {
      if (string.IsNullOrWhiteSpace(str))
        return str;
      if (str.Length == 1)
        return !useCurrentCulture ? str.ToLowerInvariant() : str.ToLower();
      if (!handleAbbreviations || !IsAllUpperCase(str))
        return (useCurrentCulture ? char.ToLower(str[0]) : char.ToLowerInvariant(str[0])).ToString() + str.Substring(1);
      return !useCurrentCulture ? str.ToLowerInvariant() : str.ToLower();
    }

    /// <summary>
    /// Converts given PascalCase/camelCase string to sentence (by splitting words by space).
    /// Example: "ThisIsSampleSentence" is converted to "This is a sample sentence".
    /// </summary>
    /// <param name="str">String to convert.</param>
    /// <param name="useCurrentCulture">set true to use current culture. Otherwise, invariant culture will be used.</param>
    public static string ToSentenceCase(this string str, bool useCurrentCulture = false)
    {
      if (string.IsNullOrWhiteSpace(str))
        return str;
      return !useCurrentCulture ? Regex.Replace(str, "[a-z][A-Z]", (MatchEvaluator) (m =>
      {
        char lowerInvariant = m.Value[0];
        string str1 = lowerInvariant.ToString();
        lowerInvariant = char.ToLowerInvariant(m.Value[1]);
        string str2 = lowerInvariant.ToString();
        return str1 + " " + str2;
      })) : Regex.Replace(str, "[a-z][A-Z]", (MatchEvaluator) (m =>
      {
        char lower = m.Value[0];
        string str3 = lower.ToString();
        lower = char.ToLower(m.Value[1]);
        string str4 = lower.ToString();
        return str3 + " " + str4;
      }));
    }

    /// <summary>
    /// Converts given PascalCase/camelCase string to kebab-case.
    /// </summary>
    /// <param name="str">String to convert.</param>
    /// <param name="useCurrentCulture">set true to use current culture. Otherwise, invariant culture will be used.</param>
    public static string ToKebabCase(this string str, bool useCurrentCulture = false)
    {
      if (string.IsNullOrWhiteSpace(str))
        return str;
      str = str.ToCamelCase();
      return !useCurrentCulture ? Regex.Replace(str, "[a-z][A-Z]", (MatchEvaluator) (m =>
      {
        char lowerInvariant = m.Value[0];
        string str1 = lowerInvariant.ToString();
        lowerInvariant = char.ToLowerInvariant(m.Value[1]);
        string str2 = lowerInvariant.ToString();
        return str1 + "-" + str2;
      })) : Regex.Replace(str, "[a-z][A-Z]", (MatchEvaluator) (m =>
      {
        char lower = m.Value[0];
        string str3 = lower.ToString();
        lower = char.ToLower(m.Value[1]);
        string str4 = lower.ToString();
        return str3 + "-" + str4;
      }));
    }

    /// <summary>
    /// Converts given PascalCase/camelCase string to snake case.
    /// Example: "ThisIsSampleSentence" is converted to "this_is_a_sample_sentence".
    /// https://github.com/npgsql/npgsql/blob/dev/src/Npgsql/NameTranslation/NpgsqlSnakeCaseNameTranslator.cs#L51
    /// </summary>
    /// <param name="str">String to convert.</param>
    /// <returns></returns>
    public static string ToSnakeCase(this string str)
    {
      if (string.IsNullOrWhiteSpace(str))
        return str;
      StringBuilder stringBuilder = new StringBuilder(str.Length + Math.Min(2, str.Length / 5));
      UnicodeCategory? nullable1 = new UnicodeCategory?();
      for (int index = 0; index < str.Length; ++index)
      {
        char lower = str[index];
        if (lower == '_')
        {
          stringBuilder.Append('_');
          nullable1 = new UnicodeCategory?();
        }
        else
        {
          UnicodeCategory unicodeCategory1 = char.GetUnicodeCategory(lower);
          UnicodeCategory? nullable2;
          switch (unicodeCategory1)
          {
            case UnicodeCategory.UppercaseLetter:
            case UnicodeCategory.TitlecaseLetter:
              nullable2 = nullable1;
              UnicodeCategory unicodeCategory2 = UnicodeCategory.SpaceSeparator;
              if (!(nullable2.GetValueOrDefault() == unicodeCategory2 & nullable2.HasValue))
              {
                nullable2 = nullable1;
                UnicodeCategory unicodeCategory3 = UnicodeCategory.LowercaseLetter;
                if (!(nullable2.GetValueOrDefault() == unicodeCategory3 & nullable2.HasValue))
                {
                  nullable2 = nullable1;
                  UnicodeCategory unicodeCategory4 = UnicodeCategory.DecimalDigitNumber;
                  if (nullable2.GetValueOrDefault() == unicodeCategory4 & nullable2.HasValue || !nullable1.HasValue || index <= 0 || index + 1 >= str.Length || !char.IsLower(str[index + 1]))
                    goto label_10;
                }
              }
              stringBuilder.Append('_');
label_10:
              lower = char.ToLower(lower);
              break;
            case UnicodeCategory.LowercaseLetter:
            case UnicodeCategory.DecimalDigitNumber:
              nullable2 = nullable1;
              UnicodeCategory unicodeCategory5 = UnicodeCategory.SpaceSeparator;
              if (nullable2.GetValueOrDefault() == unicodeCategory5 & nullable2.HasValue)
              {
                stringBuilder.Append('_');
                break;
              }
              break;
            default:
              if (nullable1.HasValue)
              {
                nullable1 = new UnicodeCategory?(UnicodeCategory.SpaceSeparator);
                continue;
              }
              continue;
          }
          stringBuilder.Append(lower);
          nullable1 = new UnicodeCategory?(unicodeCategory1);
        }
      }
      return stringBuilder.ToString();
    }

    /// <summary>Converts string to enum value.</summary>
    /// <typeparam name="T">Type of enum</typeparam>
    /// <param name="value">String value to convert</param>
    /// <returns>Returns enum object</returns>
    public static T ToEnum<T>(this string value) where T : struct
    {
      EntCheck.NotNull<string>(value, nameof (value));
      return (T) Enum.Parse(typeof (T), value);
    }

    /// <summary>Converts string to enum value.</summary>
    /// <typeparam name="T">Type of enum</typeparam>
    /// <param name="value">String value to convert</param>
    /// <param name="ignoreCase">Ignore case</param>
    /// <returns>Returns enum object</returns>
    public static T ToEnum<T>(this string value, bool ignoreCase) where T : struct
    {
      EntCheck.NotNull<string>(value, nameof (value));
      return (T) Enum.Parse(typeof (T), value, ignoreCase);
    }

    public static string ToMd5(this string str)
    {
      using (MD5 md5 = MD5.Create())
      {
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        byte[] hash = md5.ComputeHash(bytes);
        StringBuilder stringBuilder = new StringBuilder();
        foreach (byte num in hash)
          stringBuilder.Append(num.ToString("X2"));
        return stringBuilder.ToString();
      }
    }

    /// <summary>Converts camelCase string to PascalCase string.</summary>
    /// <param name="str">String to convert</param>
    /// <param name="useCurrentCulture">set true to use current culture. Otherwise, invariant culture will be used.</param>
    /// <returns>PascalCase of the string</returns>
    public static string ToPascalCase(this string str, bool useCurrentCulture = false)
    {
      if (string.IsNullOrWhiteSpace(str))
        return str;
      if (str.Length != 1)
        return (useCurrentCulture ? char.ToUpper(str[0]) : char.ToUpperInvariant(str[0])).ToString() + str.Substring(1);
      return !useCurrentCulture ? str.ToUpperInvariant() : str.ToUpper();
    }

    /// <summary>
    /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
    /// </summary>
    public static string? Truncate(this string? str, int maxLength)
    {
      if (str == null)
        return (string) null;
      return str.Length <= maxLength ? str : str.Left(maxLength);
    }

    /// <summary>
    /// Gets a substring of a string from Ending of the string if it exceeds maximum length.
    /// </summary>
    public static string? TruncateFromBeginning(this string? str, int maxLength)
    {
      if (str == null)
        return (string) null;
      return str.Length <= maxLength ? str : str.Right(maxLength);
    }

    /// <summary>
    /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
    /// It adds a "..." postfix to end of the string if it's truncated.
    /// Returning string can not be longer than maxLength.
    /// </summary>
    /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="str" /> is null</exception>
    public static string? TruncateWithPostfix(this string? str, int maxLength) => str.TruncateWithPostfix(maxLength, "...");

    /// <summary>
    /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
    /// It adds given <paramref name="postfix" /> to end of the string if it's truncated.
    /// Returning string can not be longer than maxLength.
    /// </summary>
    /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="str" /> is null</exception>
    public static string? TruncateWithPostfix(this string? str, int maxLength, string postfix)
    {
      if (str == null)
        return (string) null;
      if (str == string.Empty || maxLength == 0)
        return string.Empty;
      if (str.Length <= maxLength)
        return str;
      return maxLength <= postfix.Length ? postfix.Left(maxLength) : str.Left(maxLength - postfix.Length) + postfix;
    }

    /// <summary>
    /// Converts given string to a byte array using <see cref="P:System.Text.Encoding.UTF8" /> encoding.
    /// </summary>
    public static byte[] GetBytes(this string str) => str.GetBytes(Encoding.UTF8);

    /// <summary>
    /// Converts given string to a byte array using the given <paramref name="encoding" />
    /// </summary>
    public static byte[] GetBytes(this string str, Encoding encoding)
    {
      EntCheck.NotNull<string>(str, nameof (str));
      EntCheck.NotNull<Encoding>(encoding, nameof (encoding));
      return encoding.GetBytes(str);
    }

    private static bool IsAllUpperCase(string input)
    {
      for (int index = 0; index < input.Length; ++index)
      {
        if (char.IsLetter(input[index]) && !char.IsUpper(input[index]))
          return false;
      }
      return true;
    }
  }