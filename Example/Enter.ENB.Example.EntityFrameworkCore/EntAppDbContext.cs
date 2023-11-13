﻿using Enter.ENB.EntityFrameworkCore;
using Enter.ENB.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Example.EntityFrameworkCore;

public class EntAppDbContext : EntDbContext<EntAppDbContext>
{
    public EntAppDbContext(DbContextOptions<EntAppDbContext> options) : base(options)
    {
        
    }
}