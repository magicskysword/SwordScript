namespace SwordScript;

public class ASTExprNot : ASTList
{
    public ASTExprNot(ASTNode expr) : base(new ASTNode[] { expr })
    {
        
    }
    
    public ASTNode Expr => this[0];

    public override string ToString()
    {
        return $"-{Expr}";
    }
}