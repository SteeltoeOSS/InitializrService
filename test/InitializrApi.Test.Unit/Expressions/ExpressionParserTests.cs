// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using FluentAssertions;
using Steeltoe.InitializrApi.Expressions;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Expressions
{
    public class ExpressionParserTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void True_Parameter_Should_Evaluate_To_True()
        {
            // Arrange
            var expression = new Expression<bool>("tDep");
            var parameters = new Dictionary<string, object>
            {
                { "tDep", true },
            };

            // Act
            var result = expression.Evaluate(parameters);

            // Assert
            result.Should().Be(true);
        }

        [Fact]
        public void False_Parameter_Should_Evaluate_To_False()
        {
            // Arrange
            var expression = new Expression<bool>("fDep");
            var parameters = new Dictionary<string, object>
            {
                { "fDep", false },
            };

            // Act
            var result = expression.Evaluate(parameters);

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public void Null_Parameter_Should_Evaluate_To_False()
        {
            // Arrange
            var expression = new Expression<bool>("nDep");
            var parameters = new Dictionary<string, object>();

            // Act
            var result = expression.Evaluate(parameters);

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public void Or_Operand_Should_Should_Evaluate_Per_Boolean_Rules()
        {
            // Arrange
            var expressionTt = new Expression<bool>("t1Dep || t2Dep");
            var expressionTf = new Expression<bool>("t1Dep || f1Dep");
            var expressionTn = new Expression<bool>("t1Dep || n1Dep");
            var expressionFt = new Expression<bool>("f1Dep || t1Dep");
            var expressionFf = new Expression<bool>("f1Dep || f2Dep");
            var expressionFn = new Expression<bool>("f1Dep || n1Dep");
            var expressionNt = new Expression<bool>("n1Dep || t1Dep");
            var expressionNf = new Expression<bool>("n1Dep || f1Dep");
            var expressionNn = new Expression<bool>("n1Dep || n2Dep");
            var parameters = new Dictionary<string, object>
            {
                { "t1Dep", true },
                { "t2Dep", true },
                { "f1Dep", false },
                { "f2Dep", false },
            };

            // Act
            var resultTt = expressionTt.Evaluate(parameters);
            var resultTf = expressionTf.Evaluate(parameters);
            var resultTn = expressionTn.Evaluate(parameters);
            var resultFt = expressionFt.Evaluate(parameters);
            var resultFf = expressionFf.Evaluate(parameters);
            var resultFn = expressionFn.Evaluate(parameters);
            var resultNt = expressionNt.Evaluate(parameters);
            var resultNf = expressionNf.Evaluate(parameters);
            var resultNn = expressionNn.Evaluate(parameters);

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
            var expressionFff = new Expression<bool>("f1Dep || f2Dep || f3Dep");
            var expressionNnn = new Expression<bool>("n1Dep || n2Dep || n3Dep");
            var expressionTtt = new Expression<bool>("t1Dep || t2Dep || t3Dep");
            var expressionTfn = new Expression<bool>("t1Dep || f1Dep || n1Dep");
            var parameters = new Dictionary<string, object>
            {
                { "t1Dep", true },
                { "t2Dep", true },
                { "t3Dep", true },
                { "f1Dep", false },
                { "f2Dep", false },
                { "f3Dep", false },
            };

            // Act
            var resultFff = expressionFff.Evaluate(parameters);
            var resultNnn = expressionNnn.Evaluate(parameters);
            var resultTtt = expressionTtt.Evaluate(parameters);
            var resultTfn = expressionTfn.Evaluate(parameters);

            // Assert
            resultFff.Should().Be(false);
            resultNnn.Should().Be(false);
            resultTtt.Should().Be(true);
            resultTfn.Should().Be(true);
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Unexpected_Character_Should_Throw_Exception()
        {
            // Arrange

            // Act
            Action act = () =>
            {
                var _ = new Expression<object>("@");
            };

            // Assert
            act.Should().Throw<ExpressionException>().WithMessage("Unexpected character: '@'");
        }
    }
}
