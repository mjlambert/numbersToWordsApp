using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace numbersToWordsApp.Tests
{
    [TestClass]
    public class NumberToWordsFormatterTests
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
                { 10120.00m, "TEN THOUSAND ONE HUNDRED AND TWENTY DOLLARS AND ZERO CENTS" },
                { 1000000000.01m, "ONE BILLION DOLLARS AND ONE CENT" },
                { 1.00m, "ONE DOLLAR AND ZERO CENTS" },
                { 45686346.18m, "FORTY-FIVE MILLION SIX HUNDRED AND EIGHTY-SIX THOUSAND THREE HUNDRED AND FORTY-SIX DOLLARS AND EIGHTEEN CENTS" },
            };

            foreach (KeyValuePair<decimal, string> testCase in testCases)
            {
                string actualNumberInWords = NumberToWordsFormatter.convertNumberToWords(testCase.Key);
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
                NumberToWordsFormatter.convertNumberToWords(3758593957635465787364567493m);
            }
            catch(Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(UnsupportedNumberOfDigitsException));
            }
        }
    }
}
