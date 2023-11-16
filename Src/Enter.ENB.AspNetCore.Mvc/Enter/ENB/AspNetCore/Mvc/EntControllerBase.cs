using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Enter.ENB.AspNetCore.Mvc;

public class EntControllerBase : ControllerBase
{
    public IEntLazyServiceProvider LazyServiceProvider { get; set; }

    public EntControllerBase(IEntLazyServiceProvider lazyServiceProvider) 
    {
        LazyServiceProvider = lazyServiceProvider; 
    }

}