﻿using Enter.ENB.DependencyInjection;
using Enter.ENB.Extensions;

namespace Enter.ENB.Test;

[ExposeServices(typeof(ITestCounter))]
public class TestCounter : ITestCounter, ISingletonDependency
{
    private readonly Dictionary<string, int> _values;

    public TestCounter()
    {
        _values = new Dictionary<string, int>();
    }

    public int Increment(string name)
    {
        return Add(name, 1);
    }

    public int Decrement(string name)
    {
        return Add(name, -1);
    }

    public int Add(string name, int count)
    {
        lock (_values)
        {
            var newValue = _values.GetOrDefault(name) + count;
            _values[name] = newValue;
            return newValue;
        }
    }

    public int GetValue(string name)
    {
        lock (_values)
        {
            return _values.GetOrDefault(name);
        }
    }
}
