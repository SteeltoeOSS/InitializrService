// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using FluentAssertions;
using Steeltoe.InitializrApi.Parsers;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Parsers
{
    public class ExpressionParserTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Empty_Token_List_Should_Evaluate_To_Null()
        {
            // Arrange
            var expression = new ExpressionParser(String.Empty);

            // Act
            var result = expression.Evaluate();

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void Integer_Should_Evaluate_To_Integer()
        {
            // Arrange
            var expression = new ExpressionParser("725");

            // Act
            var result = expression.Evaluate();

            // Assert
            result.Should().Be(725);
        }

        [Fact]
        public void True_Parameter_Should_Evaluate_To_True()
        {
            // Arrange
            var expression = new ExpressionParser("my-var");
            var context = new Dictionary<string, object>
            {
                { "my-var", true },
            };

            // Act
            var result = expression.Evaluate(context);

            // Assert
            result.Should().Be(true);
        }

        [Fact]
        public void False_Parameter_Should_Evaluate_To_False()
        {
            // Arrange
            var expression = new ExpressionParser("my-var");
            var context = new Dictionary<string, object>
            {
                { "my-var", false },
            };

            // Act
            var result = expression.Evaluate(context);

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public void Null_Parameter_Should_Evaluate_To_Null()
        {
            // Arrange
            var expression = new ExpressionParser("my-var");
            var context = new Dictionary<string, object>();

            // Act
            var result = expression.Evaluate(context);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void Or_Operand_Should_Should_Evaluate_Per_Boolean_Rules()
        {
            // Arrange
            var expressionTt = new ExpressionParser("T1 || T2");
            var expressionTf = new ExpressionParser("T1 || F1");
            var expressionTn = new ExpressionParser("T1 || N1");
            var expressionFt = new ExpressionParser("F1 || T1");
            var expressionFf = new ExpressionParser("F1 || F2");
            var expressionFn = new ExpressionParser("F1 || N1");
            var expressionNt = new ExpressionParser("N1 || T1");
            var expressionNf = new ExpressionParser("N1 || F1");
            var expressionNn = new ExpressionParser("N1 || N2");
            var context = new Dictionary<string, object>
            {
                { "T1", true },
                { "T2", true },
                { "F1", false },
                { "F2", false },
            };

            // Act
            var resultTt = expressionTt.Evaluate(context);
            var resultTf = expressionTf.Evaluate(context);
            var resultTn = expressionTn.Evaluate(context);
            var resultFt = expressionFt.Evaluate(context);
            var resultFf = expressionFf.Evaluate(context);
            var resultFn = expressionFn.Evaluate(context);
            var resultNt = expressionNt.Evaluate(context);
            var resultNf = expressionNf.Evaluate(context);
            var resultNn = expressionNn.Evaluate(context);

            // Assert
            resultTt.Should().Be(true);
            resultTf.Should().Be(true);
            resultTn.Should().Be(true);
            resultFt.Should().Be(true);
            resultFf.Should().Be(false);
            resultFn.Should().Be(false);
            resultNt.Should().Be(true);
            resultNf.Should().Be(false);
            resultNn.Should().Be(false);
        }

        [Fact]
        public void GreaterThan_Operand_Should_Should_Evaluate_Any_Operand()
        {
            // Arrange
            var expression1 = new ExpressionParser("1 > 0");
            var expression2 = new ExpressionParser("1 > 1");
            var expression3 = new ExpressionParser("1 > 2");
            var expression4 = new ExpressionParser("count(T1,T2) > 1");
            var expression5 = new ExpressionParser("count(T1,T2) > 3");
            var context = new Dictionary<string, object>
            {
                { "T1", true },
                { "T2", true },
            };

            // Act
            var result1 = expression1.Evaluate(context);
            var result2 = expression2.Evaluate(context);
            var result3 = expression3.Evaluate(context);
            var result4 = expression4.Evaluate(context);
            var result5 = expression5.Evaluate(context);

            // Assert
            result1.Should().Be(true);
            result2.Should().Be(false);
            result3.Should().Be(false);
            result4.Should().Be(true);
            result5.Should().Be(false);
        }

        [Fact]
        public void Complex_Or_Operand_Should_Should_Evaluate_Per_Boolean_Rules()
        {
            // Arrange
            var expressionFff = new ExpressionParser("F1 || F2 || F3");
            var expressionNnn = new ExpressionParser("N1 || N2 || N3");
            var expressionTtt = new ExpressionParser("T1 || T2 || T3");
            var expressionTfn = new ExpressionParser("T1 || F1 || N1");
            var context = new Dictionary<string, object>
            {
                { "T1", true },
                { "T2", true },
                { "T3", true },
                { "F1", false },
                { "F2", false },
                { "F3", false },
            };

            // Act
            var resultFff = expressionFff.Evaluate(context);
            var resultNnn = expressionNnn.Evaluate(context);
            var resultTtt = expressionTtt.Evaluate(context);
            var resultTfn = expressionTfn.Evaluate(context);

            // Assert
            resultFff.Should().Be(false);
            resultNnn.Should().Be(false);
            resultTtt.Should().Be(true);
            resultTfn.Should().Be(true);
        }

        [Fact]
        public void Deep_Expression_Should_Should_Evaluate_Per_Boolean_Rules()
        {
            // Arrange
            var expressionNnnnn = new ExpressionParser("N1 || N2 || N3 || N4 || N5");
            var expressionNnnnt = new ExpressionParser("N1 || N2 || N3 || N4 || T1");
            var context = new Dictionary<string, object>
            {
                { "T1", true },
            };

            // Act
            var resultNnnnn = expressionNnnnn.Evaluate(context);
            var resultNnnnt = expressionNnnnt.Evaluate(context);

            // Assert
            resultNnnnn.Should().Be(false);
            resultNnnnt.Should().Be(true);
        }

        [Fact]
        public void Count_Should_Count_True_And_NonNull_Values()
        {
            // Arrange
            var expression1 = new ExpressionParser("count(F1)");
            var expression2 = new ExpressionParser("count(T1)");
            var expression3 = new ExpressionParser("count(N1)");
            var expression4 = new ExpressionParser("count(V1)");
            var expression5 = new ExpressionParser("count(V1,T1,N1,F1,V2,N2,F2)");
            var context = new Dictionary<string, object>
            {
                { "T1", true },
                { "V1", "val1" },
                { "V2", "val1" },
            };

            // Act
            var result1 = expression1.Evaluate(context);
            var result2 = expression2.Evaluate(context);
            var result3 = expression3.Evaluate(context);
            var result4 = expression4.Evaluate(context);
            var result5 = expression5.Evaluate(context);

            // Assert
            result1.Should().Be(0);
            result2.Should().Be(1);
            result3.Should().Be(0);
            result4.Should().Be(1);
            result5.Should().Be(3);
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Malformed_Expression_Should_Throw_Exception()
        {
            // Arrange
            var expression1 = new ExpressionParser("-");
            var expression2 = new ExpressionParser("(");
            var expression3 = new ExpressionParser(")");

            // Act
            Action act1 = () => expression1.Evaluate();
            Action act2 = () => expression2.Evaluate();
            Action act3 = () => expression3.Evaluate();

            // Assert
            act1.Should().Throw<ParserException>().WithMessage("Expected operand; actual: '-' (unknown)");
            act2.Should().Throw<ParserException>().WithMessage("Expected operand; actual: '(' (paren open)");
            act3.Should().Throw<ParserException>().WithMessage("Expected operand; actual: ')' (paren close)");
        }

        [Fact]
        public void Missing_Operand_Should_Throw_Exception()
        {
            // Arrange
            var expression1 = new ExpressionParser("||");
            var expression2 = new ExpressionParser("V ||");
            var expression3 = new ExpressionParser("V || ||");

            // Act
            Action act1 = () => expression1.Evaluate();
            Action act2 = () => expression2.Evaluate();
            Action act3 = () => expression3.Evaluate();

            // Assert
            act1.Should().Throw<ParserException>().WithMessage("Expected operand; actual: '||' (or)");
            act2.Should().Throw<ParserException>().WithMessage("Expected operand; reached end of expression.");
            act3.Should().Throw<ParserException>().WithMessage("Expected operand; actual: '||' (or)");
        }

        [Fact]
        public void Missing_Operator_Should_Throw_Exception()
        {
            // Arrange
            var expression = new ExpressionParser("V V");

            // Act
            Action act = () => expression.Evaluate();

            // Assert
            act.Should().Throw<ParserException>().WithMessage("Expected operator; actual: 'V' (name)");
        }

        [Fact]
        public void Function_Missing_Paren_Close_Should_Throw_Exception()
        {
            // Arrange
            var expression1 = new ExpressionParser("myfunc(");
            var expression2 = new ExpressionParser("myfunc(@");
            var expression3 = new ExpressionParser("myfunc(,");
            var expression4 = new ExpressionParser("myfunc(V");
            var expression5 = new ExpressionParser("myfunc(V,");
            var expression6 = new ExpressionParser("myfunc(V,@");
            var expression7 = new ExpressionParser("myfunc(V0,V1");
            var expression8 = new ExpressionParser("myfunc(V0,V1,");
            var expression9 = new ExpressionParser("myfunc(V0,V1,@");

            // Act
            Action act1 = () => expression1.Evaluate();
            Action act2 = () => expression2.Evaluate();
            Action act3 = () => expression3.Evaluate();
            Action act4 = () => expression4.Evaluate();
            Action act5 = () => expression5.Evaluate();
            Action act6 = () => expression6.Evaluate();
            Action act7 = () => expression7.Evaluate();
            Action act8 = () => expression8.Evaluate();
            Action act9 = () => expression9.Evaluate();

            // Assert
            act1.Should().Throw<ParserException>()
                .WithMessage("Expected operand or close parenthesis; reached end of expression.");
            act2.Should().Throw<ParserException>().WithMessage("Expected operand or close parenthesis; actual: '@' (unknown)");
            act3.Should().Throw<ParserException>()
                .WithMessage("Expected operand or close parenthesis; actual: ',' (comma)");
            act4.Should().Throw<ParserException>()
                .WithMessage("Expected comma or close parenthesis; reached end of expression.");
            act5.Should().Throw<ParserException>()
                .WithMessage("Expected operand or close parenthesis; reached end of expression.");
            act6.Should().Throw<ParserException>().WithMessage("Expected operand or close parenthesis; actual: '@' (unknown)");
            act7.Should().Throw<ParserException>()
                .WithMessage("Expected comma or close parenthesis; reached end of expression.");
            act8.Should().Throw<ParserException>()
                .WithMessage("Expected operand or close parenthesis; reached end of expression.");
            act9.Should().Throw<ParserException>().WithMessage("Expected operand or close parenthesis; actual: '@' (unknown)");
        }

        [Fact]
        public void Unknown_Function_Should_Throw_Exception()
        {
            // Arrange
            var expression = new ExpressionParser("unknownF()");

            // Act
            Action act = () => expression.Evaluate();

            // Assert
            act.Should().Throw<ParserException>().WithMessage("Unknown function: 'unknownF' (name)");
        }
    }
}
