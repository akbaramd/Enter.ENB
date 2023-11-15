using Enter.ENB.Localization;

namespace Enter.ENB.ExceptionHandling;

public interface ILocalizeErrorMessage
{
    string LocalizeMessage(LocalizationContext context);
}
