using System;
using System.Collections.Generic;

namespace numbersToWordsApp
{

    /// <summary>
    /// Summary description for NumberFormatter
    /// </summary>
    public static class NumberFormatter
    {
        private const int MAX_SUPPORTED_DIGITS = 3;

        private const int ONES_COLUMN = 0;
        private const int TENS_COLUMN = 1;
        private const int HUNDREDS_COLUMN = 2;

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
            { '9', "NINE" }
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

        public static string convertNumberToWords(decimal number)
        {
            string numberInWords = "";

            if (number < 0)
            {
                numberInWords += "NEGATIVE ";
            }

            String[] integerAndFractionalParts = splitIntegerAndFractionalAsString(Math.Abs(number));
            string integerPart = integerAndFractionalParts[0];
            string fractionalPart = integerAndFractionalParts[1];

            numberInWords += convertStringIntegerToWords(integerPart);
            numberInWords += " DOLLARS AND ";
            numberInWords += convertStringIntegerToWords(fractionalPart);
            numberInWords += " CENTS";

            return numberInWords;
        }

        private static String[] splitIntegerAndFractionalAsString(decimal number)
        {
            return number.ToString("0.00").Split('.');
        }

        private static string convertStringIntegerToWords(string integer)
        {
            List<string> words = new List<string>();
            int digits = integer.Length;

            // Reverse integer string so that digit indexes are consistent.
            // eg. Ones will always be index 0, tens will always be index 1 etc.
            string reversedInteger = reverseString(integer);

            if (digits > MAX_SUPPORTED_DIGITS)
            {
                throw new UnsupportedNumberOfDigitsException("This class currently"
                    + "only supports numbers up to 999.99");
            }

            if (isSignificantDigit(reversedInteger, HUNDREDS_COLUMN))
            {
                char hundredsDigit = reversedInteger[HUNDREDS_COLUMN];
                words.Add(ONES_MAP[hundredsDigit]);
                words.Add("HUNDRED");

                if (!isLastSignificantDigit(reversedInteger, HUNDREDS_COLUMN))
                {
                    words.Add("AND");
                }
            }

            if (isSignificantDigit(reversedInteger, TENS_COLUMN))
            {
                char tensDigit = reversedInteger[TENS_COLUMN];
                char onesDigit = reversedInteger[ONES_COLUMN];
                if (tensDigit == '1')
                {
                    words.Add(TEENS_MAP[onesDigit]);
                }
                else
                {
                    words.Add(TENS_MAP[tensDigit] + '-' + ONES_MAP[onesDigit]);
                }
            }
            else if (isSignificantDigit(reversedInteger, ONES_COLUMN))
            {
                char onesDigit = reversedInteger[ONES_COLUMN];
                words.Add(ONES_MAP[onesDigit]);
            }

            if (words.Count == 0)
            {
                words.Add("ZERO");
            }

            string integerInWords = string.Join(" ", words);
            return integerInWords;
        }

        private static bool columnExists(string integer, int column)
        {
            if (column < integer.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool isSignificantDigit(string reversedInteger, int column)
        {
            if (columnExists(reversedInteger, column) && reversedInteger[column] != '0')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool isLastSignificantDigit(string reversedInteger, int column)
        {
            for (int columnToCheck = column - 1; columnToCheck >= 0; columnToCheck--)
            {
                if (isSignificantDigit(reversedInteger, columnToCheck))
                {
                    return false;
                }
            }
            return true;
        }

        private static string reverseString(string input)
        {
            char[] inputArray = input.ToCharArray();
            Array.Reverse(inputArray);
            return new string(inputArray);
        }

    }

    public class UnsupportedNumberOfDigitsException : ApplicationException
    {
        public UnsupportedNumberOfDigitsException(string message) : base(message)
        {

        }
    }

}