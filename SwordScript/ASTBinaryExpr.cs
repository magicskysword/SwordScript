namespace SwordScript;

public abstract class ASTBinaryExpr : ASTList
{
    public ASTBinaryExpr(ASTNode left, ASTNode right) : base(new []{left, right})
    {

    }

    public ASTNode Left => this[0];
    
    public ASTNode Right => this[1];
}

public class ASTBinaryExprPower : ASTBinaryExpr
{
    public ASTBinaryExprPower(ASTNode left, ASTNode right) : base(left, right)
    {
        
    }

    public override string ToString()
    {
        return $"({Left} ^ {Right})";
    }
}