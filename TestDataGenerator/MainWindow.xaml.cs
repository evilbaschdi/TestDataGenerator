using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
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
            TestDataLength.Focus();
            DataType.Text = "Letters";
            TestDataLength.Value = 1;
        }

        private void CallGetTestData()
        {
            var testDataLengh = new GetTestDataLengh(TestDataLength.Value);
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
    }
}