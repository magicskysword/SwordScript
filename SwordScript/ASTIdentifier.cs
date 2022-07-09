namespace SwordScript;

/// <summary>
/// 叶节点 - 标识符
/// </summary>
public class ASTIdentifier : ASTLeaf
{
    public ASTIdentifier(string name)
    {
        Name = name;
    }
    
    public string Name { get; }

    public override string ToString()
    {
        return Name;
    }

    public override object Evaluate(SwordEnvironment env)
    {
        return env.GetVariable(Name);
    }
}