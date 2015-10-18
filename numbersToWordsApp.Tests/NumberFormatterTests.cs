using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace numbersToWordsApp.Tests
{
    [TestClass]
    public class NumberFormatterTests
    {

        [TestMethod]
        public void convertNumberToWords_WithValid_ReturnsCorrectString()
        {
            Dictionary<decimal, string> testCases = new Dictionary<decimal, string>
            {
                { 123.45m, "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS" },
                { 100.32m, "ONE HUNDRED DOLLARS AND THIRTY-TWO CENTS" },
                { 57m, "FIFTY-SEVEN DOLLARS AND ZERO CENTS" },
                { 0m, "ZERO DOLLARS AND ZERO CENTS" },
                { 101.99m, "ONE HUNDRED AND ONE DOLLARS AND NINETY-NINE CENTS" },
            };

            foreach (KeyValuePair<decimal, string> testCase in testCases)
            {
                string actualNumberInWords = NumberFormatter.convertNumberToWords(testCase.Key);
                string expectedNumberInWords = testCase.Value;
                Assert.AreEqual(
                    expectedNumberInWords,
                    actualNumberInWords, 
                    "String not in correct format using test value: " + testCase.Key + ".");
            }
        }

        [TestMethod]
        public void convertNumberToWords_WithInvalid_ThrowsException()
        {
            try
            {
                NumberFormatter.convertNumberToWords(123456789.456m);
            }
            catch(Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(UnsupportedNumberOfDigitsException));
            }
        }
    }
}
