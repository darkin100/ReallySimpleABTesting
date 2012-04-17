﻿using System;
using NUnit.Framework;
using Website.Models;
using System.Linq;

namespace UITest
{
    [TestFixture]
    public class ConfidenceCalculatorTestFixture
    {
        [TestCase(182,35, 19.23, 0)]
        [TestCase(180,45,25,1.33)]
        [TestCase(189,28,14.81,-1.13)]
        [TestCase(188,61,32.45,2.94)]
        public void TestConversionRate(double treated, double converted, double rate, double score)
        {
            var model = new ConfidenceRow()
                            {
                                Visitors = treated,
                                Conversions = converted
                            };


            Assert.That(model.Rate, Is.EqualTo(rate).Within(0.01));

        }

        [TestCase(180, 45, 25, 1.33)]
        [TestCase(189, 28, 14.81, -1.13)]
        [TestCase(188, 61, 32.45, 2.94)]        
        public void TestConfidenceCalculator(double visited, double converted, double rate, double score)
        {
            var conversions = 35d;// Conversion Rate of control
            var visitors = 182d;// Sample size of control

            var calc = new ConfidenceCalculator(visitors, conversions);

            calc.AddTest(visited, converted);

            ConfidenceRow row = calc.Tests.First();

            Assert.That(row.ZScore,Is.EqualTo(score).Within(0.01));
        }
    }
}