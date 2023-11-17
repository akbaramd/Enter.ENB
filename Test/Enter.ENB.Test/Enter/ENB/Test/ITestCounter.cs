﻿namespace Enter.ENB.Test;

public interface ITestCounter
{
    int Add(string name, int count);

    int Decrement(string name);

    int Increment(string name);

    int GetValue(string name);
}