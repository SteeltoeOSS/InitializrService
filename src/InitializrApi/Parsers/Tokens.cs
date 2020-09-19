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

        public override string ToString() => $"'{Value}'";
    }

    internal abstract class OperandToken : Token
    {
    }

    internal abstract class NamedToken : OperandToken
    {
        internal NamedToken(string name) => Name = name;

        internal string Name { get; }
    }

    internal class ParameterToken : NamedToken
    {
        internal ParameterToken(string name)
            : base(name)
        {
        }

        public override string ToString() => $"'{Name}' (parameter)";
    }

    internal class FunctionToken : NamedToken
    {
        internal FunctionToken(string name)
            : base(name)
        {
        }

        public override string ToString() => $"'{Name}' (function)";
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
