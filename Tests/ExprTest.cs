﻿using System;
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
}