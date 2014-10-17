using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using TestDataGenerator.Internal;

namespace TestDataGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    // ReSharper disable RedundantExtendsListEntry
    public partial class MainWindow : MetroWindow
    // ReSharper restore RedundantExtendsListEntry
    {
        private bool _textBoxChanging;

        /// <summary>
        /// MainWindows
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            ValidateForm();
            DataType.Text = "Letters";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TestData.DataType = DataType.Text;
            TestData.DataLength = Convert.ToInt32(Length.Text);

            TestData.Generate();
            Output.Text = TestData.Output;
        }

        private void ValidateForm()
        {
            Generator.IsEnabled = !string.IsNullOrEmpty(Length.Text);
        }

        private void Length_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProhibitLettersToAllowOnlyNumbers(Length);
            ValidateForm();
        }

        private void DataType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateForm();
        }

        private void ProhibitLettersToAllowOnlyNumbers(TextBox box)
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