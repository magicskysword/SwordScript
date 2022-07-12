namespace SwordScript;

public class ASTContinueStatement : ASTStatement
{
    public override object Evaluate(SwordEnvironment env)
    {
        throw new ContinueException();
    }
    
    public override string ToString()
    {
        return "continue;";
    }
}