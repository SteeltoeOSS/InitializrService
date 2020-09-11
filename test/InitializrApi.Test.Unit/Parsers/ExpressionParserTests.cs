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
            var result = expression.Evaluate(null);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void True_Parameter_Should_Evaluate_To_True()
        {
            // Arrange
            var expression = new ExpressionParser("MyVar");
            var context = new Dictionary<string, object>
            {
                { "MyVar", true },
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
            var expression = new ExpressionParser("MyVar");
            var context = new Dictionary<string, object>
            {
                { "MyVar", false },
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
            var expression = new ExpressionParser("MyVar");
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
        public void GreaterThan1_Should_Be_True_If_More_Than_1_True_Or_NonNull_Values()
        {
            // Arrange
            var expression1 = new ExpressionParser("moreThan1(F1)");
            var expression2 = new ExpressionParser("moreThan1(T1)");
            var expression3 = new ExpressionParser("moreThan1(N1)");
            var expression4 = new ExpressionParser("moreThan1(V1)");
            var expression5 = new ExpressionParser("moreThan1(T1,V1)");
            var expression6 = new ExpressionParser("moreThan1(V1,T1,N1,F1,V2,N2,F2)");
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
            var result6 = expression6.Evaluate(context);

            // Assert
            result1.Should().Be(false);
            result2.Should().Be(false);
            result3.Should().Be(false);
            result4.Should().Be(false);
            result5.Should().Be(true);
            result6.Should().Be(true);
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Missing_Operand_Should_Throw_Exception()
        {
            // Arrange
            var expression1 = new ExpressionParser("||");
            var expression2 = new ExpressionParser("V ||");
            var expression3 = new ExpressionParser("V || ||");

            // Act
            Action act1 = () => expression1.Evaluate(null);
            Action act2 = () => expression2.Evaluate(null);
            Action act3 = () => expression3.Evaluate(null);

            // Assert
            act1.Should().Throw<ParserException>().WithMessage("Unexpected token: '||' (or)");
            act2.Should().Throw<ParserException>().WithMessage("Expected parameter; reached end of expression.");
            act3.Should().Throw<ParserException>().WithMessage("Expected parameter; actual: '||' (or)");
        }

        [Fact]
        public void Missing_Operator_Should_Throw_Exception()
        {
            // Arrange
            var expression = new ExpressionParser("V V");

            // Act
            Action act = () => expression.Evaluate(null);

            // Assert
            act.Should().Throw<ParserException>().WithMessage("Expected operator; actual: 'V' (parameter)");
        }

        [Fact]
        public void Function_Missing_Paren_Open_Should_Throw_Exception()
        {
            // Arrange
            var expression1 = new ExpressionParser("myfunc");
            var expression2 = new ExpressionParser("myfunc V");

            // Act
            Action act1 = () => expression1.Evaluate(null);
            Action act2 = () => expression2.Evaluate(null);

            // Assert
            act1.Should().Throw<ParserException>().WithMessage("Expected open parenthesis; reached end of expression.");
            act2.Should().Throw<ParserException>().WithMessage("Expected open parenthesis; actual: 'V' (parameter)");
        }

        [Fact]
        public void Function_Missing_Paren_Close_Should_Throw_Exception()
        {
            // Arrange
            var expression1 = new ExpressionParser("myfunc");
            var expression2 = new ExpressionParser("myfunc@");
            var expression3 = new ExpressionParser("myfunc(");
            var expression4 = new ExpressionParser("myfunc(@");
            var expression5 = new ExpressionParser("myfunc(,");
            var expression6 = new ExpressionParser("myfunc(V");
            var expression7 = new ExpressionParser("myfunc(V,");
            var expression8 = new ExpressionParser("myfunc(V,@");
            var expression9 = new ExpressionParser("myfunc(V0,V1");
            var expressionA = new ExpressionParser("myfunc(V0,V1,");
            var expressionB = new ExpressionParser("myfunc(V0,V1,@");

            // Act
            Action act1 = () => expression1.Evaluate(null);
            Action act2 = () => expression2.Evaluate(null);
            Action act3 = () => expression3.Evaluate(null);
            Action act4 = () => expression4.Evaluate(null);
            Action act5 = () => expression5.Evaluate(null);
            Action act6 = () => expression6.Evaluate(null);
            Action act7 = () => expression7.Evaluate(null);
            Action act8 = () => expression8.Evaluate(null);
            Action act9 = () => expression9.Evaluate(null);
            Action actA = () => expressionA.Evaluate(null);
            Action actB = () => expressionB.Evaluate(null);

            // Assert
            act1.Should().Throw<ParserException>().WithMessage("Expected open parenthesis; reached end of expression.");
            act2.Should().Throw<ParserException>().WithMessage("Expected open parenthesis; actual: '@'");
            act3.Should().Throw<ParserException>()
                .WithMessage("Expected parameter or close parenthesis; reached end of expression.");
            act4.Should().Throw<ParserException>().WithMessage("Expected parameter or close parenthesis; actual: '@'");
            act5.Should().Throw<ParserException>()
                .WithMessage("Expected parameter or close parenthesis; actual: ',' (comma)");
            act6.Should().Throw<ParserException>()
                .WithMessage("Expected comma or close parenthesis; reached end of expression.");
            act7.Should().Throw<ParserException>()
                .WithMessage("Expected parameter or close parenthesis; reached end of expression.");
            act8.Should().Throw<ParserException>().WithMessage("Expected comma or close parenthesis; actual: '@'");
            act9.Should().Throw<ParserException>()
                .WithMessage("Expected comma or close parenthesis; reached end of expression.");
            actA.Should().Throw<ParserException>()
                .WithMessage("Expected parameter or close parenthesis; reached end of expression.");
            actB.Should().Throw<ParserException>().WithMessage("Expected comma or close parenthesis; actual: '@'");
        }

        [Fact]
        public void Unknown_Function_Should_Throw_Exception()
        {
            // Arrange
            var expression = new ExpressionParser("unknownF()");

            // Act
            Action act = () => expression.Evaluate(null);

            // Assert
            act.Should().Throw<ParserException>().WithMessage("Unknown function: 'unknownF'");
        }
    }
}
