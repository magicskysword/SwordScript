using System;
using System.Collections.Generic;
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
        from id in Lexer.Identifier select new ASTIdentifier(id);
    
    public static readonly Parser<ASTNode> Expr = Parse.Ref(() => Expr9);

    public static readonly Parser<ASTNode> Primary = (
            from left in Lexer.LeftBracket
            from expr in Parse.Ref(() => Expr)
            from right in Lexer.RightBracket
            select expr)
        .Or(Literal)
        .Or(Identifier);

    public static readonly Parser<ASTNode> Expr2 =(
            from symbol in Lexer.Negate.Or(Lexer.Not)
            from expr in Primary
            select (ASTNode)(symbol == "-" ? new ASTUnaryExprNegative(expr) : new ASTUnaryExprNot(expr)))
        .Or(Primary);
    
    public static readonly Parser<ASTNode> Expr3 =(
            from left in Expr2
            from symbol in Lexer.Power
            from right in Expr3
            select new ASTBinaryExprPower(left, right))
        .Or(Expr2);

    public delegate ASTNode CreateNode(ASTNode left,string op,ASTNode right);

    public static Parser<ASTNode> LeftOperator(Parser<ASTNode> leftExpr, Parser<string> symbol,
        CreateNode apply)
    {
        ASTNode CreateNode(ASTNode left, IEnumerable<Tuple<string,ASTNode>> rights)
        {
            foreach(var right in rights)
            {
                left = apply(left, right.Item1, right.Item2);
            }

            return left;
        }
        
        Parser<Tuple<string,ASTNode>> innerOperatorExpr = (
            from getSymbol in symbol
            from right in leftExpr
            select new Tuple<string, ASTNode>(getSymbol, right)
        );
        
        Parser<ASTNode> operatorExpr = (
                from left in leftExpr
                from rights in innerOperatorExpr.Many()
                select CreateNode(left, rights)
            );
        
        return operatorExpr;
    }
    
    public static readonly Parser<ASTNode> Expr4 = LeftOperator(Expr3, Lexer.Multiply.Or(Lexer.Divide).Or(Lexer.Modulo),
        (left, op, right) =>
        {
            switch (op)
            {
                case "*":
                    return new ASTBinaryExprMultiply(left, right);
                case "/":
                    return new ASTBinaryExprDivide(left, right);
                case "%":
                    return new ASTBinaryExprModulo(left, right);
                default:
                    throw new ArgumentException($"Unknown operator '{op}'");
            }
        });

    public static readonly Parser<ASTNode> Expr5 = LeftOperator(Expr4, Lexer.Plus.Or(Lexer.Minus),
        (left, op, right) =>
        {
            switch (op)
            {
                case "+":
                    return new ASTBinaryExprPlus(left, right);
                case "-":
                    return new ASTBinaryExprMinus(left, right);
                default:
                    throw new ArgumentException($"Unknown operator '{op}'");
            }
        });
    
    public static readonly Parser<ASTNode> Expr6 = LeftOperator(Expr5, 
        Lexer.GreaterThanOrEqual.Or(Lexer.GreaterThan)
        .Or(Lexer.LessThanOrEqual).Or(Lexer.LessThan),
        (left, op, right) =>
        {
            switch (op)
            {
                case ">":
                    return new ASTBinaryExprGreaterThan(left, right);
                case ">=":
                    return new ASTBinaryExprGreaterThanOrEqual(left, right);
                case "<":
                    return new ASTBinaryExprLessThan(left, right);
                case "<=":
                    return new ASTBinaryExprLessThanOrEqual(left, right);
                default:
                    throw new ArgumentException($"Unknown operator '{op}'");
            }
        });
        
    public static readonly Parser<ASTNode> Expr7 = LeftOperator(Expr6, Lexer.Equal.Or(Lexer.NotEqual),
        (left, op, right) =>
        {
            switch (op)
            {
                case "==":
                    return new ASTBinaryExprEqual(left, right);
                case "!=":
                    return new ASTBinaryExprNotEqual(left, right);
                default:
                    throw new ArgumentException($"Unknown operator '{op}'");
            }
        });
    
    public static readonly Parser<ASTNode> Expr8 = LeftOperator(Expr7, Lexer.And,
        (left, op, right) => new ASTBinaryExprAnd(left, right));
    
    public static readonly Parser<ASTNode> Expr9 = LeftOperator(Expr8, Lexer.Or,
        (left, op, right) => new ASTBinaryExprOr(left, right));

    public static readonly Parser<ASTNode> Assignment =
        from left in Identifier
        from assign in Lexer.Assign
        from right in Expr
        from _ in Lexer.Semicolon.Optional()
        select new ASTBinaryExprAssignment(left, right);
}