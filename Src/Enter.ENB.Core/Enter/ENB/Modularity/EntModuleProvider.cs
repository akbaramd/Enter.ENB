namespace Enter.ENB.Modularity;

public class EntModuleProvider :   IEntModuleProvider
{

    private Dictionary<string, EntModule> _registeredModule = new Dictionary<string, EntModule>();
    
    

    public void Register<T>(T module) where T : EntModule
    {
        if (!Exists(module))
        {
            _registeredModule.Add(module.GetType().Name,module);
        }
    }

    

    public bool Exists<T>(T module) where T : EntModule
    {
        return Exists(module.GetType().Name);
    }

    public bool Exists(string moduleKey)
    {
        return _registeredModule.Any(x => x.Key == moduleKey);
    }
}