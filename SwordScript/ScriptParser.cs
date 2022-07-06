using Sprache;

namespace SwordScript;

public static class ScriptParser
{
    public static Parser<ASTLiteral> Literal =
        (from nullValue in Parse.Ref(() => Lexer.Null) select new ASTNullLiteral())
        .Or<ASTLiteral>(from booleanValue in Parse.Ref(() => Lexer.Boolean) select new ASTBooleanLiteral(booleanValue))
        .Or(from doubleValue in Parse.Ref(() => Lexer.DoubleFloat) select new ASTDoubleFloatLiteral(doubleValue))
        .Or(from longValue in Parse.Ref(() => Lexer.LongInteger) select new ASTLongIntegerLiteral(longValue))
        .Or(from stringValue in Parse.Ref(() => Lexer.String) select new ASTStringLiteral(stringValue));

    public static Parser<ASTIdentifier> Identifier =
        from id in Parse.Ref(() => Lexer.Identifier) select new ASTIdentifier(id);
    
    public static Parser<ASTNode> Expr;

    public static Parser<ASTNode> Primary = (
            from left in Parse.Char('(').SuperToken()
            from expr in Parse.Ref(() => Expr)
            from right in Parse.Char(')').SuperToken()
            select expr)
        .Or(Literal)
        .Or(Identifier)
        .SuperToken();

    public static Parser<ASTNode> Expr2 =(
            from symbol in Parse.Ref(() => Lexer.Negate).Or( Parse.Ref(() => Lexer.Not))
            from expr in Primary
            select (ASTNode)(symbol == "-" ? new ASTUnaryExprNegative(expr) : new ASTUnaryExprNot(expr)))
        .Or(Primary)
        .SuperToken();
    
    public static Parser<ASTNode> Expr3 =(
            from left in Expr2
            from symbol in Parse.Ref(() => Lexer.Power)
            from right in Parse.Ref(() => Expr3)
            select new ASTBinaryExprPower(left, right))
        .Or<ASTNode>(Expr2)
        .SuperToken();
}