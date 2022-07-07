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

public class ASTBinaryExprMultiply : ASTBinaryExpr
{
    public ASTBinaryExprMultiply(ASTNode left, ASTNode right) : base(left, right)
    {
        
    }

    public override string ToString()
    {
        return $"({Left} * {Right})";
    }
}

public class ASTBinaryExprDivide : ASTBinaryExpr
{
    public ASTBinaryExprDivide(ASTNode left, ASTNode right) : base(left, right)
    {
        
    }

    public override string ToString()
    {
        return $"({Left} / {Right})";
    }
}

public class ASTBinaryExprModulo : ASTBinaryExpr
{
    public ASTBinaryExprModulo(ASTNode left, ASTNode right) : base(left, right)
    {
        
    }

    public override string ToString()
    {
        return $"({Left} % {Right})";
    }
}

public class ASTBinaryExprPlus : ASTBinaryExpr
{
    public ASTBinaryExprPlus(ASTNode left, ASTNode right) : base(left, right)
    {
        
    }

    public override string ToString()
    {
        return $"({Left} + {Right})";
    }
}

public class ASTBinaryExprMinus : ASTBinaryExpr
{
    public ASTBinaryExprMinus(ASTNode left, ASTNode right) : base(left, right)
    {
        
    }

    public override string ToString()
    {
        return $"({Left} - {Right})";
    }
}

public class ASTBinaryExprGreaterThan : ASTBinaryExpr
{
    public ASTBinaryExprGreaterThan(ASTNode left, ASTNode right) : base(left, right)
    {
        
    }

    public override string ToString()
    {
        return $"({Left} > {Right})";
    }
}

public class ASTBinaryExprGreaterThanOrEqual : ASTBinaryExpr
{
    public ASTBinaryExprGreaterThanOrEqual(ASTNode left, ASTNode right) : base(left, right)
    {
        
    }

    public override string ToString()
    {
        return $"({Left} >= {Right})";
    }
}

public class ASTBinaryExprLessThan : ASTBinaryExpr
{
    public ASTBinaryExprLessThan(ASTNode left, ASTNode right) : base(left, right)
    {
        
    }

    public override string ToString()
    {
        return $"({Left} < {Right})";
    }
}

public class ASTBinaryExprLessThanOrEqual : ASTBinaryExpr
{
    public ASTBinaryExprLessThanOrEqual(ASTNode left, ASTNode right) : base(left, right)
    {
        
    }

    public override string ToString()
    {
        return $"({Left} <= {Right})";
    }
}

public class ASTBinaryExprEqual : ASTBinaryExpr
{
    public ASTBinaryExprEqual(ASTNode left, ASTNode right) : base(left, right)
    {
        
    }

    public override string ToString()
    {
        return $"({Left} == {Right})";
    }
}

public class ASTBinaryExprNotEqual : ASTBinaryExpr
{
    public ASTBinaryExprNotEqual(ASTNode left, ASTNode right) : base(left, right)
    {
        
    }

    public override string ToString()
    {
        return $"({Left} != {Right})";
    }
}

public class ASTBinaryExprAnd : ASTBinaryExpr
{
    public ASTBinaryExprAnd(ASTNode left, ASTNode right) : base(left, right)
    {
        
    }

    public override string ToString()
    {
        return $"({Left} and {Right})";
    }
}

public class ASTBinaryExprOr : ASTBinaryExpr
{
    public ASTBinaryExprOr(ASTNode left, ASTNode right) : base(left, right)
    {
        
    }

    public override string ToString()
    {
        return $"({Left} or {Right})";
    }
}