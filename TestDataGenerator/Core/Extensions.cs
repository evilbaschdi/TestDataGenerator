using System.Windows.Controls;

namespace TestDataGenerator.Core
{
    /// <summary>
    /// </summary>
    public static class Extensions
    {
        private static bool _textBoxChanging;

        /// <summary>
        ///     Modifies the entered text of a textbox to accept only numbers.
        /// </summary>
        /// <param name="box">Textbox to modify.</param>
        public static void ProhibitLettersToAllowOnlyNumbers(this TextBox box)
        {
            // stop multiple changes;
            if (_textBoxChanging)
            {
                return;
            }
            _textBoxChanging = true;

            var text = box.Text;

            if (text == "")
            {
                return;
            }

            var validText = "";
            var pos = box.SelectionStart;

            for (var i = 0; i < text.Length; i++)
            {
                var s = text[i];
                if (s < '0' || s > '9')
                {
                    if (i <= pos)
                    {
                        pos--;
                    }
                }
                else
                {
                    validText += s;
                }
            }

            // trim starting 00s
            while (validText.Length >= 2 && validText.StartsWith("00"))
            {
                validText = validText.Substring(1);
                if (pos < 2)
                {
                    pos--;
                }
            }

            if (pos > validText.Length)
            {
                pos = validText.Length;
            }
            box.Text = validText;
            box.SelectionStart = pos;
            _textBoxChanging = false;
        }
    }
}