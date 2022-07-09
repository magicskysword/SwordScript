using System.Collections.Generic;

namespace SwordScript;

public class SwordEnvironment
{
    private Dictionary<string, object> _variables = new Dictionary<string, object>();
    
    public void SetVariable(string name, object value)
    {
        _variables[name] = value;
    }
    
    public object GetVariable(string name)
    {
        if(_variables.ContainsKey(name))
            return _variables[name];
        return null;
    }
}