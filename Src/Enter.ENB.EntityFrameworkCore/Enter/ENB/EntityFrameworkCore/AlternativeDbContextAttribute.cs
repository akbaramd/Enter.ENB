namespace Enter.ENB.EntityFrameworkCore;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AlternativeDbContextAttribute : Attribute
{
    public AlternativeDbContextAttribute(Type replacedDbContextTypes)
    {
        ReplacedDbContextTypes = replacedDbContextTypes;
    }

    public Type ReplacedDbContextTypes { get; }

}
