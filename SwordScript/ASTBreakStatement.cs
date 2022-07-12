namespace SwordScript;

public class ASTBreakStatement : ASTStatement
{
    public override object Evaluate(SwordEnvironment env)
    {
        throw new BreakException();
    }
    
    public override string ToString()
    {
        return "break;";
    }
}