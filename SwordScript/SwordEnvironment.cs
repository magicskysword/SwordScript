using System.Collections.Generic;

namespace SwordScript;

public class SwordEnvironment
{
    private Dictionary<string, object> _variables = new Dictionary<string, object>();
    
    public void SetVariable(string name, object value)
    {
        unchecked
        {
            switch (value)
            {
                case uint ui:
                    value = (long)ui;
                    break;
                case int i:
                    value = (long)i;
                    break;
                case ulong ul:
                    value = (long)ul;
                    break;
                case float f:
                    value = (double)f;
                    break;
                case char c:
                    value = c.ToString();
                    break;
            }
        }

        _variables[name] = value;
    }
    
    public object GetVariable(string name)
    {
        if(_variables.ContainsKey(name))
            return _variables[name];
        return null;
    }
}