﻿namespace Enter.ENB.MultiTenancy;

[Flags]
public enum MultiTenancySides : byte
{
    /// <summary>
    /// Tenant side.
    /// </summary>
    Tenant = 1,

    /// <summary>
    /// Host side.
    /// </summary>
    Host = 2,

    /// <summary>
    /// Both sides
    /// </summary>
    Both = Tenant | Host
}
