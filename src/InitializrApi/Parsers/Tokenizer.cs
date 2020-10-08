// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Steeltoe.InitializrApi.Test.Unit")]

namespace Steeltoe.InitializrApi.Parsers
{
    internal class Tokenizer
    {
        private const int EndOfString = -1;

        internal IEnumerable<Token> Scan(string expression)
        {
            using var reader = new StringReader(expression);
            var tokens = new List<Token>();
            while (reader.Peek() != EndOfString)
            {
                var ch = (char)reader.Peek();

                if (char.IsWhiteSpace(ch))
                {
                    reader.Read();
                }
                else if (char.IsDigit(ch))
                {
                    var i = ReadInteger(reader);
                    tokens.Add(new IntegerToken(i));
                }
                else if (char.IsLetter(ch))
                {
                    var name = ReadName(reader);
                    tokens.Add(new NameToken(name));
                }
                else if (ch.Equals(','))
                {
                    reader.Read();
                    tokens.Add(new CommaToken());
                }
                else if (ch.Equals('('))
                {
                    reader.Read();
                    tokens.Add(new ParenOpenToken());
                }
                else if (ch.Equals(')'))
                {
                    reader.Read();
                    tokens.Add(new ParenCloseToken());
                }
                else if (ch.Equals('|'))
                {
                    reader.Read();
                    ch = (char)reader.Read();
                    if (!ch.Equals('|'))
                    {
                        throw new ArgumentException("expected: '|'");
                    }

                    tokens.Add(new OrOperatorToken());
                }
                else if (ch.Equals('>'))
                {
                    reader.Read();
                    tokens.Add(new GreaterThanOperatorToken());
                }
                else
                {
                    tokens.Add(new UnknownToken(ch));
                    reader.Read();
                }
            }

            return tokens;
        }

        private int ReadInteger(StringReader reader)
        {
            var buf = new StringBuilder();
            while (true)
            {
                var ch = (char)reader.Peek();
                if (!char.IsDigit(ch))
                {
                    break;
                }

                buf.Append((char)reader.Read());
            }

            return int.Parse(buf.ToString());
        }

        private string ReadName(StringReader reader)
        {
            var buf = new StringBuilder();
            while (true)
            {
                var ch = (char)reader.Peek();
                if (!(char.IsLetterOrDigit(ch) || ch == '-'))
                {
                    break;
                }

                buf.Append((char)reader.Read());
            }

            return buf.ToString();
        }
    }
}
