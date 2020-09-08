// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Steeltoe.InitializrApi.Expressions
{
    internal class Tokenizer
    {
        private const int EndOfString = -1;

        internal IEnumerable<Token> Scan(string expression)
        {
            var reader = new StringReader(expression);
            var tokens = new List<Token>();
            while (reader.Peek() != EndOfString)
            {
                var ch = (char)reader.Peek();
                if (char.IsWhiteSpace(ch))
                {
                    reader.Read();
                    continue;
                }

                if (char.IsLetter(ch))
                {
                    tokens.Add(ParseParameterToken(reader));
                }
                else if (ch.Equals('|'))
                {
                    tokens.Add(ParseOrOperatorToken(reader));
                }
                else
                {
                    throw new ExpressionException($"unexpected character: '{ch}'");
                }
            }

            return tokens;
        }

        private ParameterToken ParseParameterToken(StringReader reader)
        {
            var ch = (char)reader.Peek();
            if (!char.IsLetter(ch))
            {
                throw new ExpressionException($"expected a letter: '{ch}'");
            }

            var buf = new StringBuilder();
            while (char.IsLetterOrDigit((char)reader.Peek()))
            {
                ch = (char)reader.Read();
                buf.Append(ch);
            }

            return new ParameterToken(buf.ToString());
        }

        private OrOperatorToken ParseOrOperatorToken(StringReader reader)
        {
            if (!((char)reader.Read()).Equals('|'))
            {
                throw new ExpressionException("expected character: '|'");
            }

            if (!((char)reader.Read()).Equals('|'))
            {
                throw new ExpressionException("expected character: '|'");
            }

            return new OrOperatorToken();
        }
    }
}
