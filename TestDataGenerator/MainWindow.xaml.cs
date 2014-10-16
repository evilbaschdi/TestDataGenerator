using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
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
            DataType.Text = "Letters";
        }

        private void GenerateOutputOnClick(object sender, RoutedEventArgs e)
        {
            ITestDataLengh testDataLengh = new GetTestDataLengh(TestDataLength.Text);
            ITestDataType testDataType = new GetTestDataType(DataType.Text);
            ITestData testData = new GetTestData(testDataLengh, testDataType);
            Output.Text = testData.Value;
        }

        private void CopyToClipboardOnClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Output.Text);
        }

        private void ValidateForm()
        {
            Generator.IsEnabled = !string.IsNullOrWhiteSpace(TestDataLength.Text);
            CopyToClipboard.IsEnabled = !string.IsNullOrWhiteSpace(TestDataLength.Text);
        }

        private void TestDataLengthTextChanged(object sender, TextChangedEventArgs e)
        {
            Extensions.ProhibitLettersToAllowOnlyNumbers(TestDataLength);
            ValidateForm();
        }

        private void DataTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateForm();
        }
    }
}