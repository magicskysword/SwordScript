using System;
using NUnit.Framework;
using Sprache;
using SwordScript;

namespace Tests;

public class ExprTest
{
    [Test]
    public void PrimaryTest()
    {
        Assert.AreEqual("123", ScriptParser.Primary.Parse(" 123 ").ToString());
        Assert.AreEqual("0.5", ScriptParser.Primary.Parse(" 0.5 ").ToString());
        Assert.AreEqual("abc", ScriptParser.Primary.Parse(" abc ").ToString());
        Assert.AreEqual("true", ScriptParser.Primary.Parse(" true ").ToString());
        Assert.AreEqual("null", ScriptParser.Primary.Parse(" null ").ToString());
    }

    [Test]
    public void Expr2Test()
    {
        Assert.AreEqual("123", ScriptParser.Expr2.Parse(" 123 ").ToString());
        Assert.AreEqual("-123", ScriptParser.Expr2.Parse(" -123 ").ToString());
        Assert.AreEqual("not false", ScriptParser.Expr2.Parse(" not false ").ToString());
        Assert.AreEqual("nottrue", ScriptParser.Expr2.Parse(" nottrue ").ToString());
    }
    
    [Test]
    public void Expr3Test()
    {
        Assert.AreEqual("(1 ^ 4)", ScriptParser.Expr3.Parse(" 1 ^ 4 ").ToString());
        Assert.AreEqual("(5 ^ 0.7)", ScriptParser.Expr3.Parse(" 5 ^ .7 ").ToString());
        Assert.AreEqual("(0.1 ^ 0.1)", ScriptParser.Expr3.Parse(" 0.1 ^ .1 ").ToString());
        Assert.AreEqual("(2 ^ (2 ^ 2))", ScriptParser.Expr3.Parse(" 2 ^ 2 ^ 2 ").ToString());
    }
    
    [Test]
    public void Expr4Test()
    {
        Assert.AreEqual("(1 * 2)", ScriptParser.Expr4.Parse(" 1 * 2 ").ToString());
        Assert.AreEqual("(1 / 2)", ScriptParser.Expr4.Parse(" 1 / 2 ").ToString());
        Assert.AreEqual("(1 % 2)", ScriptParser.Expr4.Parse(" 1 % 2 ").ToString());
        Assert.AreEqual("((((1 * 2) / 3) % 4) * 5)", ScriptParser.Expr4.Parse(" 1 * 2 / 3 % 4 * 5 ").ToString());
    }

    [Test]
    public void Expr5Test()
    {
        Assert.AreEqual("(1 + 2)", ScriptParser.Expr5.Parse(" 1 + 2 ").ToString());
        Assert.AreEqual("(1 - 2)", ScriptParser.Expr5.Parse(" 1 - 2 ").ToString());
        Assert.AreEqual("(((1 + 2) - 3) + 4)", ScriptParser.Expr5.Parse(" 1 + 2 - 3 + 4 ").ToString());
    }

    [Test]
    public void Expr6Test()
    {
        Assert.AreEqual("(1 < 2)", ScriptParser.Expr6.Parse(" 1 < 2 ").ToString());
        Assert.AreEqual("(1 > 2)", ScriptParser.Expr6.Parse(" 1 > 2 ").ToString());
        Assert.AreEqual("(1 <= 2)", ScriptParser.Expr6.Parse(" 1 <= 2 ").ToString());
        Assert.AreEqual("(1 >= 2)", ScriptParser.Expr6.Parse(" 1 >= 2 ").ToString());
        Assert.AreEqual("(((1 < 2) < 3) < 4)", ScriptParser.Expr6.Parse(" 1 < 2 < 3 < 4 ").ToString());
    }

    [Test]
    public void Expr7Test()
    {
        Assert.AreEqual("(1 == 2)", ScriptParser.Expr7.Parse(" 1 == 2 ").ToString());
        Assert.AreEqual("(1 != 2)", ScriptParser.Expr7.Parse(" 1 != 2 ").ToString());
        Assert.AreEqual("(((1 == 2) == 3) == 4)", ScriptParser.Expr7.Parse(" 1 == 2 == 3 == 4 ").ToString());
    }

    [Test]
    public void Expr8Test()
    {
        Assert.AreEqual("(a and b)", ScriptParser.Expr8.Parse(" a and b ").ToString());
        Assert.AreEqual("((a and b) and c)", ScriptParser.Expr8.Parse(" a and b and c ").ToString());
    }

    [Test]
    public void Expr9Test()
    {
        Assert.AreEqual("(a or b)", ScriptParser.Expr9.Parse(" a or b ").ToString());
        Assert.AreEqual("((a or b) or c)", ScriptParser.Expr9.Parse(" a or b or c ").ToString());
    }
    
    [Test]
    public void ExpressionTest()
    {
        Assert.AreEqual("((1 + 2) * (3 + 4))", ScriptParser.Expr.Parse(" (1 + 2) * (3 + 4)  ").ToString());
        Assert.AreEqual("(a >= ((3 * (b ^ 5)) - 8))", ScriptParser.Expr.Parse(" a >= 3 * b ^ 5 - 8 ").ToString());
        Assert.AreEqual("((a and (b1 == b2)) or (((9 * 9) == c) and d))", ScriptParser.Expr.Parse(" a and b1 == b2 or 9*9 == c and d ").ToString());
        Assert.AreEqual("((a and (b1 == (b2 or ((9 * 9) == c)))) and d)", ScriptParser.Expr.Parse(" a and b1 == (b2 or 9*9 == c) and d ").ToString());
    }
    
    [Test]
    public void ExpressionEvaluateTest()
    {
        Assert.AreEqual(1, ScriptParser.Expr.Parse(" 1 ").Evaluate(null));
        Assert.AreEqual(3, ScriptParser.Expr.Parse(" 1 + 2 ").Evaluate(null));
        Assert.AreEqual(3, ScriptParser.Expr.Parse(" 1 + 2 * 3 - 4").Evaluate(null));
        Assert.AreEqual(false, ScriptParser.Expr.Parse(" not true ").Evaluate(null));
        Assert.AreEqual(true, ScriptParser.Expr.Parse(" 1 + 1 == 2 and 2 + 2 == 4 ").Evaluate(null));
        Assert.AreEqual("abc", ScriptParser.Expr.Parse(" \"ab\" + \"c\" ").Evaluate(null));
        Assert.AreEqual(true, ScriptParser.Expr.Parse(" \"Hello \"+\"World\" == \"Hello World\" ").Evaluate(null));
        Assert.AreEqual(2.0, (double)ScriptParser.Expr.Parse(" 4 ^ 0.5 ").Evaluate(null), 0.00001);
    }
}