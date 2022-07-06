using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

namespace SwordScript;

/// <summary>
/// 词法分析器
/// </summary>
public static class Lexer
{
    /// <summary>
    /// 去除空格与注释
    /// </summary>
    /// <param name="parser"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Parser<T> SuperToken<T>(this Parser<T> parser)
    {
        return from leftComment in Parse.Ref(()=>Comment.AnyComment).Token().Many()
            from token in parser.Token()
            from rightComment in Parse.Ref(()=>Comment.AnyComment).Token().Many()
            select token;
    }
    
    /// <summary>
    /// 标识符
    /// </summary>
    public static readonly Parser<string> Identifier = Parse.Regex(@"[_\p{L}][0-9_\p{L}]*")
        .Where(t => !Words.ALL_RESERVED_WORDS.Contains(t)).SuperToken();
    
    /// <summary>
    /// 长整形
    /// </summary>
    public static readonly Parser<long> LongInteger = Parse.Chars("0123456789").AtLeastOnce().Text().Select(long.Parse).SuperToken();

    /// <summary>
    /// 浮点数
    /// </summary>
    public static readonly Parser<double> DoubleFloat = Parse.Regex(@"[0-9]*\.[0-9]+").Text().Select(double.Parse).SuperToken();

    /// <summary>
    /// 字符串
    /// </summary>
    public static readonly Parser<string> String =
        Parse.Regex(@"""(\\""|[^""])*""")
            .Select(s => s.Substring(1, s.Length - 2)
                .Replace(@"\""", @"""")
                .Replace(@"\\", @"\")
                .Replace(@"\n", "\n")).SuperToken();
    
    public static readonly Parser<bool> Boolean = Parse.String(Words.BOOLEAN_TRUE)
        .Or(Parse.String(Words.BOOLEAN_FALSE))
        .Text()
        .Select(s => s == Words.BOOLEAN_TRUE)
        .SuperToken();
    
    public static readonly Parser<object> Null = Parse.String(Words.NULL).Return<IEnumerable<char>,object>(null).SuperToken();

    public static readonly CommentParser Comment = new CommentParser();

    /// <summary>
    /// 返回下个字符不为Unicode字符的字符符号解析器
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public static Parser<string> LetterSymbol(string symbol)
    {
        return (from symbolStr in Parse.String(symbol).Text()
            from nextChar in Parse.Regex(@"\p{L}").Preview()
            where nextChar.IsEmpty
            select symbolStr).SuperToken();
    }
    
    /// <summary>
    /// 返回普通的标点符号解析器
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public static Parser<string> PunctuationSymbol(string symbol)
    {
        return (from symbolStr in Parse.String(symbol).Text() select symbolStr).SuperToken();
    }
    
    public static readonly Parser<string> Negate = PunctuationSymbol("-");
    public static readonly Parser<string> Not = LetterSymbol("not");
    public static readonly Parser<string> Power = PunctuationSymbol("^");
}