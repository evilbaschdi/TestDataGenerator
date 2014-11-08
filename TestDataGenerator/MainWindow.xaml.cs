using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using TestDataGenerator.Core;
using TestDataGenerator.Internal;

namespace TestDataGenerator
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>

    // ReSharper disable RedundantExtendsListEntry
    public partial class MainWindow : MetroWindow
        // ReSharper restore RedundantExtendsListEntry
    {
        public MainWindow()
        {
            InitializeComponent();
            ValidateForm();
            TestDataLength.Focus();
            DataType.Text = "Letters";
        }

        private void CallGetTestData()
        {
            var testDataLengh = new GetTestDataLengh(TestDataLength.Text);
            var testDataType = new GetTestDataType(DataType.Text);
            var testDataCharPool = new TestDataCharPool();
            var testData = new GetTestData(testDataLengh, testDataType, testDataCharPool);
            Output.Text = testData.Value;
        }

        private void GenerateOutputOnClick(object sender, RoutedEventArgs e)
        {
            CallGetTestData();
        }

        private void TestDataLengthOnKeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Return:
                    CallGetTestData();
                    break;

                case Key.Tab:
                    DataType.IsDropDownOpen = true;
                    break;
            }
        }

        private void DataTypeOnKeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Return:
                    CallGetTestData();
                    break;
            }
        }

        private void CopyToClipboardOnClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Output.Text);
        }

        private void ValidateForm()
        {
            var testDataLenghtIsNotNullOrWhiteSpace = !string.IsNullOrWhiteSpace(TestDataLength.Text);
            Generator.IsEnabled = testDataLenghtIsNotNullOrWhiteSpace;
            CopyToClipboard.IsEnabled = testDataLenghtIsNotNullOrWhiteSpace;
        }

        private void TestDataLengthTextChanged(object sender, TextChangedEventArgs e)
        {
            TestDataLength.ProhibitLettersToAllowOnlyNumbers();
            ValidateForm();
        }

        private void DataTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateForm();
        }
    }
}