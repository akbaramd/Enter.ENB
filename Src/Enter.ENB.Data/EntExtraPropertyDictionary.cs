using System;
using System.Collections.Generic;

namespace Enter.ENB.Uow;

[Serializable]
public class EntExtraPropertyDictionary : Dictionary<string, object?>
{
    public EntExtraPropertyDictionary()
    {
    }

    public EntExtraPropertyDictionary(IDictionary<string, object?> dictionary)
        : base(dictionary)
    {
    }
}