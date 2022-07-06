namespace SwordScript;

public abstract class ASTBinaryExpr
{
    public ASTBinaryExpr(ASTNode left, ASTNode right)
    {
        this.Left = left;
        this.Right = right;
    }

    public ASTNode Right { get; }

    public ASTNode Left { get; }
}