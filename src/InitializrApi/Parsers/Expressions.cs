// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Steeltoe.InitializrApi.Parsers
{
    internal abstract class Expression
    {
        internal abstract object Evaluate(Dictionary<string, object> context);
    }

    internal class Integer : Expression
    {
        internal Integer(int i)
        {
            Value = i;
        }

        private int Value { get; }

        internal override object Evaluate(Dictionary<string, object> context)
        {
            return Value;
        }
    }

    internal class Parameter : Expression
    {
        internal Parameter(string name)
        {
            Name = name;
        }

        private string Name { get; }

        internal override object Evaluate(Dictionary<string, object> context)
        {
            context.TryGetValue(Name, out var value);
            return value;
        }
    }

    internal class OrOperation : Expression
    {
        internal OrOperation(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        private Expression Left { get; }

        private Expression Right { get; }

        internal override object Evaluate(Dictionary<string, object> context)
        {
            var left = ToBool(Left.Evaluate(context));
            var right = ToBool(Right.Evaluate(context));
            return left || right;
        }

        private static bool ToBool(object value)
        {
            if (value is null)
            {
                return false;
            }

            if (value is bool b)
            {
                return b;
            }

            return false;
        }
    }

    internal class GreaterThanOperation : Expression
    {
        internal GreaterThanOperation(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        private Expression Left { get; }

        private Expression Right { get; }

        internal override object Evaluate(Dictionary<string, object> context)
        {
            var left = (int)Left.Evaluate(context);
            var right = (int)Right.Evaluate(context);
            return left > right;
        }
    }

    internal class Count : Expression
    {
        internal Count(IEnumerable<Expression> expressions)
        {
            Expressions = expressions;
        }

        private IEnumerable<Expression> Expressions { get; }

        internal override object Evaluate(Dictionary<string, object> context)
        {
            int count = 0;
            foreach (var expression in Expressions)
            {
                var value = expression.Evaluate(context);
                if (value != null)
                {
                    if (value is bool)
                    {
                        if ((bool)value)
                        {
                            ++count;
                        }
                    }
                    else
                    {
                        ++count;
                    }
                }
            }

            return count;
        }
    }
}
