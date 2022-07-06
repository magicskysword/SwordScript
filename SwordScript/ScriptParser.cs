using Sprache;

namespace SwordScript;

public static class ScriptParser
{
    public static readonly Parser<ASTLiteral> Literal =
        (from nullValue in Lexer.Null select new ASTNullLiteral())
        .Or<ASTLiteral>(from booleanValue in Lexer.Boolean select new ASTBooleanLiteral(booleanValue))
        .Or(from doubleValue in Lexer.DoubleFloat select new ASTDoubleFloatLiteral(doubleValue))
        .Or(from longValue in Lexer.LongInteger select new ASTLongIntegerLiteral(longValue))
        .Or(from stringValue in Lexer.String select new ASTStringLiteral(stringValue));

    public static readonly Parser<ASTIdentifier> Identifier =
        from id in Parse.Ref(() => Lexer.Identifier) select new ASTIdentifier(id);
    
    public static readonly Parser<ASTNode> Expr;

    public static readonly Parser<ASTNode> Primary = (
            from left in Parse.Char('(').SuperToken()
            from expr in Parse.Ref(() => Expr)
            from right in Parse.Char(')').SuperToken()
            select expr)
        .Or(Literal)
        .Or(Identifier)
        .SuperToken();

    public static readonly Parser<ASTNode> Expr2 =(
            from symbol in Lexer.Negate.Or(Lexer.Not)
            from expr in Primary
            select (ASTNode)(symbol == "-" ? new ASTUnaryExprNegative(expr) : new ASTUnaryExprNot(expr)))
        .Or(Primary)
        .SuperToken();
    
    public static readonly Parser<ASTNode> Expr3 =(
            from left in Expr2
            from symbol in Lexer.Power
            from right in Expr3
            select new ASTBinaryExprPower(left, right))
        .Or(Expr2)
        .SuperToken();
}