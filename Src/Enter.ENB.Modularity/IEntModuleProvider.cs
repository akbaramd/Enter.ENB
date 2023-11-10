namespace Enter.ENB.Modularity;

public interface IEntModuleProvider
{
    public void Register<T>(T module) where T : EntModule;
    
    public bool Exists<T>(T module) where T : EntModule;
    public bool Exists(string moduleKey);

}