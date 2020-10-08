// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Steeltoe.InitializrApi.Parsers
{
    internal abstract class Token
    {
    }

    internal class UnknownToken : Token
    {
        internal UnknownToken(char value) => Value = value;

        internal char Value { get; }

        public override string ToString() => $"'{Value}' (unknown)";
    }

    internal abstract class OperandToken : Token
    {
    }

    internal class IntegerToken : OperandToken
    {
        public IntegerToken(int i)
        {
            Value = i;
        }

        internal int Value { get; }

        public override string ToString() => $"'{Value}' (integer)";
    }

    internal abstract class NamedToken : OperandToken
    {
        internal NamedToken(string name) => Name = name;

        internal string Name { get; }
    }

    internal class NameToken : NamedToken
    {
        internal NameToken(string name)
            : base(name)
        {
        }

        public override string ToString() => $"'{Name}' (name)";
    }

    internal abstract class OperatorToken : Token
    {
    }

    internal abstract class BooleanOperatorToken : OperatorToken
    {
    }

    internal class OrOperatorToken : BooleanOperatorToken
    {
        public override string ToString() => $"'||' (or)";
    }

    internal class GreaterThanOperatorToken : BooleanOperatorToken
    {
        public override string ToString() => $"'>' (greater than)";
    }

    internal class ParenOpenToken : Token
    {
        public override string ToString() => $"'(' (paren open)";
    }

    internal class ParenCloseToken : Token
    {
        public override string ToString() => $"')' (paren close)";
    }

    internal class CommaToken : Token
    {
        public override string ToString() => $"',' (comma)";
    }
}
