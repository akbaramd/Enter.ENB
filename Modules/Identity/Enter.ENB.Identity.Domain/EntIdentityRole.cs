using Enter.ENB.Domain.Auditing;
using Enter.ENB.Domain.Entities;
using Enter.ENB.Statics;

namespace Enter.ENB.Identity.Domain;

public  class EntIdentityRole : AggregateRoot<Guid>
{
    private EntIdentityRole(){}
    
    public EntIdentityRole(Guid id,string name,string title)
    {

        Id = id;
        SetName(name);
        SetTitle(title);
    }
    
    public EntIdentityRole(Guid id,string name)
    {
        Id = id;
        SetName(name);
        SetTitle(name);
    }
    public string Name { get; private set; }
    public string Title { get; private  set; }

    public void SetName(string name)
    {
        EntCheck.NotNullOrWhiteSpace(name, nameof(name));
        Name = name.Trim();
    }
    
    public void SetTitle(string title)
    {
        EntCheck.NotNullOrWhiteSpace(title, nameof(title));
        Title = title.Trim();
    }
    
}