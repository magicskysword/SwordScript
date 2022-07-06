namespace SwordScript;

public abstract class ASTUnaryExpr : ASTList
{
    public ASTUnaryExpr(ASTNode expr) : base(new ASTNode[] { expr })
    {
        
    }
    
    public ASTNode Expr => this[0];
}

public class ASTUnaryExprNegative : ASTUnaryExpr
{
    public ASTUnaryExprNegative(ASTNode expr) : base(expr)
    {
    }
    
    public override string ToString()
    {
        return $"-{Expr}";
    }
}

public class ASTUnaryExprNot : ASTUnaryExpr
{
    public ASTUnaryExprNot(ASTNode expr) : base(expr)
    {
    }
    
    public override string ToString()
    {
        return $"not {Expr}";
    }
}