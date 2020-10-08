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
        public object Evaluate(Dictionary<string, object> context = null)
        {
            if (!_tokens.MoveNext())
            {
                return null;
            }

            var expression = BuildExpression();
            return expression.Evaluate(context);
        }

        private Expression BuildExpression()
        {
            var operand = BuildOperand();

            if (_tokens.Current is null)
            {
                return operand;
            }

            if (_tokens.Current is OrOperatorToken)
            {
                _tokens.MoveNext();
                return new OrOperation(operand, BuildExpression());
            }

            if (_tokens.Current is GreaterThanOperatorToken)
            {
                _tokens.MoveNext();
                return new GreaterThanOperation(operand, BuildExpression());
            }

            throw new ParserException($"Expected operator; actual: {_tokens.Current}");
        }

        private Expression BuildOperand()
        {
            if (_tokens.Current is null)
            {
                throw new ParserException($"Expected operand; reached end of expression.");
            }

            if (!(_tokens.Current is OperandToken))
            {
                throw new ParserException($"Expected operand; actual: {_tokens.Current}");
            }

            if (_tokens.Current is IntegerToken)
            {
                var iToken = _tokens.Current as IntegerToken;
                _tokens.MoveNext();
                return BuildInteger(iToken);
            }

            var nToken = _tokens.Current as NameToken;
            _tokens.MoveNext();
            if (_tokens.Current is ParenOpenToken)
            {
                _tokens.MoveNext();
                return BuildFunction(nToken);
            }

            return BuildParameter(nToken);
        }

        private Expression BuildInteger(IntegerToken iToken)
        {
            return new Integer(iToken.Value);
        }

        private Expression BuildParameter(NameToken nToken)
        {
            return new Parameter(nToken.Name);
        }

        private Expression BuildFunction(NameToken nToken)
        {
            if (_tokens.Current is null)
            {
                throw new ParserException("Expected operand or close parenthesis; reached end of expression.");
            }

            if (!(_tokens.Current is NameToken || _tokens.Current is ParenCloseToken))
            {
                throw new ParserException($"Expected operand or close parenthesis; actual: {_tokens.Current}");
            }

            var parameters = new List<Expression>();
            while (!(_tokens.Current is ParenCloseToken))
            {
                if (parameters.Count > 0)
                {
                    if (!(_tokens.Current is CommaToken))
                    {
                        throw new ParserException("Expected comma or close parenthesis; reached end of expression.");
                    }

                    _tokens.MoveNext();
                }

                if (_tokens.Current is null)
                {
                    throw new ParserException("Expected operand or close parenthesis; reached end of expression.");
                }

                if (!(_tokens.Current is NameToken || _tokens.Current is ParenCloseToken))
                {
                    throw new ParserException($"Expected operand or close parenthesis; actual: {_tokens.Current}");
                }

                parameters.Add(BuildOperand());
            }

            if (!(_tokens.Current is ParenCloseToken))
            {
                throw new ParserException($"Expected operand or close parenthesis; actual: {_tokens.Current}");
            }

            _tokens.MoveNext();
            if (nToken.Name.Equals("count"))
            {
                return new Count(parameters);
            }

            throw new ParserException($"Unknown function: {nToken}");
        }
    }
}
