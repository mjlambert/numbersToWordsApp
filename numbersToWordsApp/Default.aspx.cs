using System;
using System.Text.RegularExpressions;

namespace numbersToWordsApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submitNumber_Click(object sender, EventArgs e)
        {
            // Strip out invalid characters.
            string numberAsString = Regex.Replace(numberInput.Text, @"[^\d\.,+-]", "");
            numberInput.Text = numberAsString;

            try
            {
                numberInWords.Text = NumberToWordsFormatter.convertNumberToWords(Convert.ToDecimal(numberAsString));
            }
            catch (FormatException exception)
            {
                numberInWords.Text = "Could not convert. Number is not is correct format.";
            }
            catch (UnsupportedNumberOfDigitsException exception)
            {
                numberInWords.Text = exception.Message;
            }
        }
    }
}