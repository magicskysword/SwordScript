using NUnit.Framework;
using Sprache;
using SwordScript;

namespace Tests;

public class LexerText
{
    [Test]
    public void Identifier()
    {
        Assert.AreEqual("abc", Lexer.Identifier.Parse(" abc "));
        Assert.AreEqual("_abc", Lexer.Identifier.Parse(" _abc "));
        Assert.AreEqual("_abc123", Lexer.Identifier.Parse(" _abc123 "));
        Assert.AreEqual("变量", Lexer.Identifier.Parse(" 变量 "));
        Assert.AreEqual("变量123", Lexer.Identifier.Parse(" 变量123 "));
        Assert.Catch<ParseException>(() => Lexer.Identifier.Parse(" "));
        Assert.Catch<ParseException>(() => Lexer.Identifier.Parse(" 123 "));
        Assert.Catch<ParseException>(() => Lexer.Identifier.Parse(" 123abc "));
    }

    [Test]
    public void LongInteger()
    {
        Assert.AreEqual(0L, Lexer.LongInteger.Parse(" 0 "));
        Assert.AreEqual(1L, Lexer.LongInteger.Parse(" 1 "));
        Assert.AreEqual(123L, Lexer.LongInteger.Parse(" 123 "));
        Assert.AreEqual(123456789L, Lexer.LongInteger.Parse(" 123456789 "));
        Assert.AreEqual(1234567890123456789L, Lexer.LongInteger.Parse(" 1234567890123456789 "));
        Assert.Catch<ParseException>(() => Lexer.LongInteger.Parse(" "));
        Assert.Catch<ParseException>(() => Lexer.LongInteger.Parse(" abc "));
    }
    
    [Test]
    public void DoubleFloat()
    {
        Assert.AreEqual(0.0, Lexer.DoubleFloat.Parse(" 0.0 "), 0.00001);
        Assert.AreEqual(0.5, Lexer.DoubleFloat.Parse(" 0.5 "), 0.00001);
        Assert.AreEqual(.5, Lexer.DoubleFloat.Parse(" .5 "), 0.00001);
        Assert.AreEqual(1.0, Lexer.DoubleFloat.Parse(" 1.0 "), 0.00001);
        Assert.AreEqual(10.01, Lexer.DoubleFloat.Parse(" 10.01 "), 0.00001);
        Assert.AreEqual(9999.9999, Lexer.DoubleFloat.Parse(" 9999.9999 "), 0.00001);
    }
    
    [Test]
    public void String()
    {
        Assert.AreEqual("", Lexer.String.Parse(@" """" "));
        Assert.AreEqual("abc", Lexer.String.Parse(@" ""abc"" "));
        Assert.AreEqual("abc\ndef", Lexer.String.Parse(@" ""abc\ndef"" "));
        Assert.AreEqual("abc\ndef\n", Lexer.String.Parse(@" ""abc\ndef\n"" "));
        Assert.AreEqual("你好，世界", Lexer.String.Parse(@" ""你好，世界"" "));
    }
}