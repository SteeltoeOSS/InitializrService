// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using FluentAssertions;
using Steeltoe.InitializrApi.Parsers;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Parsers
{
    public class TokenizerTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Empty_String_Should_Return_Zero_Tokens()
        {
            // Arrange
            var expr = string.Empty;
            var tokenizer = new Tokenizer();

            // Act
            var tokens = tokenizer.Scan(expr).ToList();


            // Asserty
            tokens.Count.Should().Be(0);
        }

        [Fact]
        public void Whitespace_Should_Return_Zero_Tokens()
        {
            // Arrange
            var expr = " \f\n\r\t\v";
            var tokenizer = new Tokenizer();

            // Act
            var tokens = tokenizer.Scan(expr).ToList();


            // Assert
            tokens.Count.Should().Be(0);
        }

        [Fact]
        public void Scan_Should_Yield_UnknownToken()
        {
            // Arrange
            var expr = "@";
            var tokenizer = new Tokenizer();

            // Act
            var tokens = tokenizer.Scan(expr).ToList();


            // Assert
            tokens.Count.Should().Be(1);
            var token = Assert.IsType<UnknownToken>(tokens[0]);
            token.Value.Should().Be('@');
        }

        [Fact]
        public void Scan_Should_Yield_IntegerToken()
        {
            // Arrange
            var expr = "123";
            var tokenizer = new Tokenizer();

            // Act
            var tokens = tokenizer.Scan(expr).ToList();


            // Assert
            tokens.Count.Should().Be(1);
            var token = Assert.IsType<IntegerToken>(tokens[0]);
            token.Value.Should().Be(123);
        }

        [Fact]
        public void Scan_Should_Yield_NameToken()
        {
            // Arrange
            var expr = " my-name ";
            var tokenizer = new Tokenizer();

            // Act
            var tokens = tokenizer.Scan(expr).ToList();

            // Assert
            tokens.Count.Should().Be(1);
            var token = Assert.IsType<NameToken>(tokens[0]);
            token.Name.Should().Be("my-name");
        }

        [Fact]
        public void Scan_Should_Yield_OrOperatorToken()
        {
            // Arrange
            var expr = "||";
            var tokenizer = new Tokenizer();

            // Act
            var tokens = tokenizer.Scan(expr).ToList();


            // Assert
            tokens.Count.Should().Be(1);
            Assert.IsType<OrOperatorToken>(tokens[0]);
        }

        [Fact]
        public void Scan_Should_Yield_GreaterThanOperatorToken()
        {
            // Arrange
            var expr = ">";
            var tokenizer = new Tokenizer();

            // Act
            var tokens = tokenizer.Scan(expr).ToList();


            // Assert
            tokens.Count.Should().Be(1);
            Assert.IsType<GreaterThanOperatorToken>(tokens[0]);
        }

        [Fact]
        public void Scan_Should_Yield_ParenOpenToken()
        {
            // Arrange
            var expr = "(";
            var tokenizer = new Tokenizer();

            // Act
            var tokens = tokenizer.Scan(expr).ToList();


            // Assert
            tokens.Count.Should().Be(1);
            Assert.IsType<ParenOpenToken>(tokens[0]);
        }

        [Fact]
        public void Scan_Should_Yield_ParenCloseToken()
        {
            // Arrange
            var expr = ")";
            var tokenizer = new Tokenizer();

            // Act
            var tokens = tokenizer.Scan(expr).ToList();


            // Assert
            tokens.Count.Should().Be(1);
            Assert.IsType<ParenCloseToken>(tokens[0]);
        }

        [Fact]
        public void Scan_Should_Yield_CommaToken()
        {
            // Arrange
            var expr = ",";
            var tokenizer = new Tokenizer();

            // Act
            var tokens = tokenizer.Scan(expr).ToList();


            // Assert
            tokens.Count.Should().Be(1);
            Assert.IsType<CommaToken>(tokens[0]);
        }

        [Fact]
        public void Scan_Should_Find_Multiple_Tokens()
        {
            // Arrange
            var expr = "|| MyName";
            var tokenizer = new Tokenizer();

            // Act
            var tokens = tokenizer.Scan(expr).ToList();


            // Assert
            tokens.Count.Should().Be(2);
            Assert.IsType<OrOperatorToken>(tokens[0]);
            Assert.IsType<NameToken>(tokens[1]);
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Incomplete_Or_Operator_Should_Throw_ArgumentException()
        {
            // Arrange
            var expr = "|";
            var tokenizer = new Tokenizer();

            // Act
            Action act = () => tokenizer.Scan(expr);


            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Expected: '|'");
        }
    }
}
