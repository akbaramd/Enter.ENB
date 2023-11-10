namespace Enter.ENB.Uow;
public static class EventOrderGenerator
{
    private static long _lastOrder;

    public static long GetNext() => Interlocked.Increment(ref EventOrderGenerator._lastOrder);
}