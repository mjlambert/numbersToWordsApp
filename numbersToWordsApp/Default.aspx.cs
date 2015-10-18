using System;

namespace numbersToWordsApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submitNumber_Click(object sender, EventArgs e)
        {
            try
            {
                numberInWords.Text = NumberFormatter.convertNumberToWords(Convert.ToDecimal(numberInput.Text));
            }
            catch (FormatException exception)
            {
                numberInWords.Text = exception.Message;
            }
            catch (UnsupportedNumberOfDigitsException exception)
            {
                numberInWords.Text = exception.Message; // TODO have a separate label for errors
            }
        }
    }
}