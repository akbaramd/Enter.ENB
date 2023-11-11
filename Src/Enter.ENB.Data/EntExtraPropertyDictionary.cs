namespace Enter.ENB.Data;

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