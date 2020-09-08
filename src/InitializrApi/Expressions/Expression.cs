// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Steeltoe.InitializrApi.Expressions
{
    /// <summary>
    /// Parser for expressions.
    /// </summary>
    /// BNF:
    /// expression       := operand | operand operator operand
    /// operator         := booleanOperator
    /// booleanOperator  := orOperator
    /// orOperator       := "||"
    /// operand          := parameter
    /// parameter        := alpha | alpha alphanumeric
    /// alpha            := "a-Z"
    /// digit            := "0-9"
    /// alphanumeric     := alpha | digit
    public class Expression<T>
    {
        private readonly IEnumerator<Token> _tokens;

        private readonly Type _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="Expression{T}"/> class.
        /// </summary>
        /// <param name="expression">The expression in string form.</param>
        public Expression(string expression)
        {
            _type = typeof(T);
            _tokens = new Tokenizer().Scan(expression).GetEnumerator();
        }

        /// <summary>
        /// Evaluates the expression.
        /// </summary>
        /// <param name="parameters"> Expression parameters.</param>
        /// <returns>The evaluation.</returns>
        public T Evaluate(Dictionary<string, object> parameters)
        {
            if (_type == typeof(bool))
            {
                return (T)(object)EvaluateBooleanExpression(parameters);
            }

            return default(T);
        }

        private bool EvaluateBooleanExpression(Dictionary<string, object> parameters)
        {
            bool result = false;

            while (_tokens.MoveNext())
            {
                switch (_tokens.Current)
                {
                    case ParameterToken parameter:
                        result = GetBooleanValue(parameter.Name, parameters);
                        break;
                    default:
                        throw new ExpressionException($"Expected a parameter: {_tokens.Current}");
                }

                if (!_tokens.MoveNext())
                {
                    break;
                }

                do
                {
                    if (!(_tokens.Current is OrOperatorToken))
                    {
                        throw new ExpressionException($"Expected an or operator: {_tokens.Current}");
                    }

                    if (!_tokens.MoveNext())
                    {
                        throw new ExpressionException($"Expected a parameter.");
                    }

                    switch (_tokens.Current)
                    {
                        case ParameterToken parameter:
                            result = result || GetBooleanValue(parameter.Name, parameters);
                            break;
                        default:
                            throw new ExpressionException($"Expected a parameter: {_tokens.Current}");
                    }
                }
                while (_tokens.MoveNext());
            }

            return result;
        }

        private bool GetBooleanValue(string name, Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(name))
            {
                var value = parameters[name];
                if (value is bool)
                {
                    return (bool)value;
                }
            }

            return false;
        }
    }
}
