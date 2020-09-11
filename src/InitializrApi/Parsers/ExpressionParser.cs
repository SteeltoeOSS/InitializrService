// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Steeltoe.InitializrApi.Parsers
{
    /// <summary>
    /// Parser for expressions.
    /// </summary>
    public class ExpressionParser
    {
        private IEnumerator<Token> _tokens;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParser"/> class.
        /// </summary>
        /// <param name="expression">The expression in string form.</param>
        public ExpressionParser(string expression)
        {
            _tokens = new Tokenizer().Scan(expression).GetEnumerator();
        }

        /// <summary>
        /// Evaluates the expression using the specified parameters.
        /// </summary>
        /// <param name="context"> Expression parameter context.</param>
        /// <returns>The evaluation.</returns>
        public object Evaluate(Dictionary<string, object> context)
        {
            if (!_tokens.MoveNext())
            {
                return null;
            }

            if (context is null)
            {
                context = new Dictionary<string, object>();
            }

            Expression result;

            if (_tokens.Current is ParameterToken)
            {
                result = BuildParameterExpression((ParameterToken)_tokens.Current);
            }
            else if (_tokens.Current is FunctionToken)
            {
                result = BuildFunctionExpression((FunctionToken)_tokens.Current);
            }
            else
            {
                throw new ParserException($"Unexpected token: {_tokens.Current}");
            }

            return result?.Evaluate(context);
        }

        private Expression BuildParameterExpression(ParameterToken pToken)
        {
            Expression result = new Parameter(pToken.Name);
            if (!_tokens.MoveNext())
            {
                return result;
            }

            if (!(_tokens.Current is OrOperatorToken))
            {
                throw new ParserException($"Expected operator; actual: {_tokens.Current}");
            }

            do
            {
                if (!_tokens.MoveNext())
                {
                    throw new ParserException("Expected parameter; reached end of expression.");
                }

                if (!(_tokens.Current is ParameterToken))
                {
                    throw new ParserException($"Expected parameter; actual: {_tokens.Current}");
                }

                pToken = _tokens.Current as ParameterToken;
                result = new OrOperation(result, new Parameter(pToken.Name));
            }
            while (_tokens.MoveNext());

            return result;
        }

        private Expression BuildFunctionExpression(FunctionToken fToken)
        {
            if (!_tokens.MoveNext())
            {
                throw new ParserException("Expected open parenthesis; reached end of expression.");
            }

            if (!(_tokens.Current is ParenOpenToken))
            {
                throw new ParserException($"Expected open parenthesis; actual: {_tokens.Current}");
            }

            if (!_tokens.MoveNext())
            {
                throw new ParserException("Expected parameter or close parenthesis; reached end of expression.");
            }

            var parameters = new List<Parameter>();
            if (_tokens.Current is ParameterToken)
            {
                parameters.Add(new Parameter(((ParameterToken)_tokens.Current).Name));
                if (!_tokens.MoveNext())
                {
                    throw new ParserException("Expected comma or close parenthesis; reached end of expression.");
                }

                while (_tokens.Current is CommaToken)
                {
                    if (!_tokens.MoveNext())
                    {
                        throw new ParserException(
                            "Expected parameter or close parenthesis; reached end of expression.");
                    }

                    if (!(_tokens.Current is ParameterToken))
                    {
                        throw new ParserException($"Expected comma or close parenthesis; actual: {_tokens.Current}");
                    }

                    parameters.Add(new Parameter(((ParameterToken)_tokens.Current).Name));
                    if (!_tokens.MoveNext())
                    {
                        throw new ParserException("Expected comma or close parenthesis; reached end of expression.");
                    }
                }
            }

            if (!(_tokens.Current is ParenCloseToken))
            {
                throw new ParserException($"Expected parameter or close parenthesis; actual: {_tokens.Current}");
            }

            if (fToken.Name.Equals("moreThan1"))
            {
                return new MoreThan1(parameters);
            }

            throw new ParserException($"Unknown function: '{fToken.Name}'");
        }
    }
}
