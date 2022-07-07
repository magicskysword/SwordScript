namespace SwordScript;

public abstract class ASTNode
{
    public abstract object Evaluate(SwordEnvironment env);
}