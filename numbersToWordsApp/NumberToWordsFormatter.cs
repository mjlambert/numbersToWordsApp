using System;
using System.Collections.Generic;
using System.Linq;

namespace numbersToWordsApp
{

    /// <summary>
    /// A utility class used to format numbers into words.
    /// </summary>
    public static class NumberToWordsFormatter
    {
        // Support numbers up to 999 Septillion
        private const int MAX_SUPPORTED_DIGITS = 27;

        private const string CURRENCY_WHOLE = "DOLLAR";
        private const string CURRENCY_FRACTIONAL = "CENT";

        private static readonly Dictionary<char, string> ONES_MAP = new Dictionary<char, string>
        {
            { '0', "ZERO" },
            { '1', "ONE" },
            { '2', "TWO" },
            { '3', "THREE" },
            { '4', "FOUR" },
            { '5', "FIVE" },
            { '6', "SIX" },
            { '7', "SEVEN" },
            { '8', "EIGHT" },
            { '9', "NINE" },
        };

        private static readonly Dictionary<char, string> TEENS_MAP = new Dictionary<char, string>
        {
            { '0', "TEN" },
            { '1', "ELEVEN" },
            { '2', "TWELVE" },
            { '3', "THIRTEEN" },
            { '4', "FOURTEEN" },
            { '5', "FIFTEEN" },
            { '6', "SIXTEEN" },
            { '7', "SEVENTEEN" },
            { '8', "EIGHTEEN" },
            { '9', "NINETEEN" },
        };

        private static readonly Dictionary<char, string> TENS_MAP = new Dictionary<char, string>
        {
            // There is no "1" here becasue the tens are special and have unique words.
            { '2', "TWENTY" },
            { '3', "THIRTY" },
            { '4', "FORTY" },
            { '5', "FIFTY" },
            { '6', "SIXTY" },
            { '7', "SEVENTY" },
            { '8', "EIGHTY" },
            { '9', "NINETY" },
        };

        private static readonly Dictionary<int, string> PERIOD_MAP = new Dictionary<int, string>
        {
            { 2, "THOUSAND" },
            { 3, "MILLION" },
            { 4, "BILLION" },
            { 5, "TRILLION" },
            { 6, "QUADRILLION" },
            { 7, "QUINTILLION" },
            { 8, "SEXTILLION" },
            { 9, "SEPTILLION" },
        };

        /// <summary>
        /// Converts a number into words using short scale.
        /// </summary>
        /// <param name="number">Number to convert.</param>
        /// <returns>A string representing the number in words.</returns>
        public static string convertNumberToWords(decimal number)
        {
            List<string> words = new List<string>();

            if (number < 0)
            {
                words.Add("NEGATIVE");
            }

            String[] integerAndFractionalParts = splitIntegerAndFractionalAsString(Math.Abs(number));
            string integerPart = integerAndFractionalParts[0];
            string fractionalPart = integerAndFractionalParts[1];

            Stack<string> integerPartPeriods = splitIntegerIntoPeriods(integerPart);
            Stack<string> fractionalPartPeriods = splitIntegerIntoPeriods(fractionalPart);

            // Convert integer part of number
            words.AddRange(convertPeriodsIntoWords(integerPartPeriods));

            if (words.Count < 3 && words.Last() == "ONE")
            {
                words.Add(CURRENCY_WHOLE);
            }
            else
            {
                words.Add(CURRENCY_WHOLE + 'S');
            }
            words.Add("AND");

            // Convert fractional part of number
            words.AddRange(convertPeriodsIntoWords(fractionalPartPeriods));

            if (words.Last() == "ONE")
            {
                words.Add(CURRENCY_FRACTIONAL);
            }
            else
            {
                words.Add(CURRENCY_FRACTIONAL + 'S');
            }

            string numberInWords = string.Join(" ", words);
            return numberInWords;
        }

        // Splits a decimal number into its integer and fractional parts
        // and returns the result in an array. The integer part being in
        // index 0 and the fractional part in index 1.
        // Will pad to two decimal places, so there will always be an integer
        // and a fractional part.
        private static String[] splitIntegerAndFractionalAsString(decimal number)
        {
            return number.ToString("0.00").Split('.');
        }

        // Takes an integer as a string and splits it into periods.
        // A period in this context is the group of three digits in
        // between the commas in large numbers.
        // All periods will be padded to three digits.
        // eg. 12589123 will be split into 012, 589, 123.
        private static Stack<string> splitIntegerIntoPeriods(string integer)
        {
            Stack<string> periods = new Stack<string>();

            if (integer.Length > MAX_SUPPORTED_DIGITS)
            {
                throw new UnsupportedNumberOfDigitsException("Program does not"
                    + " support numbers larger than 999 Septillion");
            }

            // Starting from the end on the string and working backwards,
            // Add every three digits to the stack.
            for (int i = integer.Length - 3; i > -3; i -= 3)
            {
                int periodSize = 3;
                int startIndex = i;
                int paddedZeroes = 0;
                string padding = "";

                // If index is less than zero, final period
                // must be less than three digits, so pad it.
                if (i < 0)
                {
                    periodSize = 3 + i;
                    startIndex = 0;
                    paddedZeroes = Math.Abs(i);
                }

                for (int paddedZero = 0; paddedZero < paddedZeroes; paddedZero++)
                {
                    padding += '0';
                }

                periods.Push(padding + integer.Substring(startIndex, periodSize));
            }

            return periods;
        }

        // Takes a stack of periods and converts them into words.
        private static List<string> convertPeriodsIntoWords(Stack<string> periods)
        {
            List<string> words = new List<string>();

            while (periods.Count != 0)
            {
                int currentPeriod = periods.Count;
                List<string> periodInWords = convertPeriodIntoWords(periods.Pop());
                words.AddRange(periodInWords);

                // Unless we are on the last period, add
                // the period name. eg. THOUSAND
                // Don't add the name if current period is empty.
                if (currentPeriod > 1 && periodInWords.Count > 0)
                {
                    words.Add(PERIOD_MAP[currentPeriod]);
                }
            }

            if (words.Count == 0)
            {
                words.Add("ZERO");
            }

            return words;
        }

        // Takes a period as a string and converts it into words.
        // Expects period to be 3 digits.
        private static List<string> convertPeriodIntoWords(string period)
        {
            List<string> words = new List<string>();
            char hundredsDigit = period[0];
            char tensDigit = period[1];
            char onesDigit = period[2];

            // Hundreds Column
            if (isSignificantDigit(hundredsDigit))
            {
                words.Add(ONES_MAP[hundredsDigit]);
                words.Add("HUNDRED");

                if (!isLastSignificantDigit(period, 0))
                {
                    words.Add("AND");
                }
            }

            // Tens and Ones Column
            if (isSignificantDigit(tensDigit))
            {
                if (tensDigit == '1')
                {
                    words.Add(TEENS_MAP[onesDigit]);
                }
                else if (isSignificantDigit(onesDigit))
                {
                    words.Add(TENS_MAP[tensDigit] + '-' + ONES_MAP[onesDigit]);
                }
                else
                {
                    words.Add(TENS_MAP[tensDigit]);
                }
            }
            else if (isSignificantDigit(onesDigit))
            {
                words.Add(ONES_MAP[onesDigit]);
            }

            return words;
        }

        // Checks to see if a digit is significant or not.
        // 0 is an insignificant digit.
        private static bool isSignificantDigit(char digit)
        {
            if (digit == '0')
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Checks to see if the digit at the specified column (index in string) is
        // the last significant digit in the integer.
        // eg. the '1' in '100' is the last significant digit becasue the remaining
        // digits are all zeroes.
        private static bool isLastSignificantDigit(string integer, int column)
        {
            for (int columnToCheck = column + 1; columnToCheck < integer.Length; columnToCheck++)
            {
                if (isSignificantDigit(integer[columnToCheck]))
                {
                    return false;
                }
            }
            return true;
        }

    }

    public class UnsupportedNumberOfDigitsException : ApplicationException
    {
        public UnsupportedNumberOfDigitsException(string message) : base(message)
        {

        }
    }

}