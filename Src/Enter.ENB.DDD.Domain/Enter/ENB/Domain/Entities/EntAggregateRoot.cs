using Enter.ENB.Auditing;
using Enter.ENB.Data;

namespace Enter.ENB.Domain.Entities;

[Serializable]
public abstract class AggregateRoot : BasicAggregateRoot,
    IHasConcurrencyStamp
{
     public virtual EntExtraPropertyDictionary EntExtraProperties { get; protected set; }

    [DisableAuditing]
    public virtual string ConcurrencyStamp { get; set; }

    protected AggregateRoot()
    {
        ConcurrencyStamp = Guid.NewGuid().ToString("N");
        // EntExtraProperties = new EntExtraPropertyDictionary();
        // this.SetDefaultsForExtraProperties();
    }

    // public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    // {
    //     return ExtensibleObjectValidator.GetValidationErrors(
    //         this,
    //         validationContext
    //     );
    // }
}

[Serializable]
public abstract class AggregateRoot<TKey> : BasicAggregateRoot<TKey>,
     IHasExtraProperties,
    IHasConcurrencyStamp
{
    public virtual EntExtraPropertyDictionary EntExtraProperties { get; protected set; }

    [DisableAuditing]
    public virtual string ConcurrencyStamp { get; set; }

    protected AggregateRoot()
    {
        ConcurrencyStamp = Guid.NewGuid().ToString("N");
         EntExtraProperties = new EntExtraPropertyDictionary();
         //this.SetDefaultsForExtraProperties();
    }

    protected AggregateRoot(TKey id)
        : base(id)
    {
        ConcurrencyStamp = Guid.NewGuid().ToString("N");
         EntExtraProperties = new EntExtraPropertyDictionary();
         //this.SetDefaultsForExtraProperties();
    }

    // public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    // {
        // return ExtensibleObjectValidator.GetValidationErrors(
            // this,
            // validationContext
        // );
    // }
}
