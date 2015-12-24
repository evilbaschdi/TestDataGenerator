using MahApps.Metro.Controls;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        private readonly ApplicationStyle _style;

        public MainWindow()
        {
            _style = new ApplicationStyle(this);
            InitializeComponent();
            _style.Load();
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
            switch (e.Key)
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
            switch (e.Key)
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

        #region Flyout

        private void ToggleSettingsFlyoutClick(object sender, RoutedEventArgs e)
        {
            ToggleFlyout(0);
        }

        private void ToggleFlyout(int index, bool stayOpen = false)
        {
            var activeFlyout = (Flyout)Flyouts.Items[index];
            if (activeFlyout == null)
            {
                return;
            }

            foreach (
                var nonactiveFlyout in
                    Flyouts.Items.Cast<Flyout>()
                        .Where(nonactiveFlyout => nonactiveFlyout.IsOpen && nonactiveFlyout.Name != activeFlyout.Name))
            {
                nonactiveFlyout.IsOpen = false;
            }

            if (activeFlyout.IsOpen && stayOpen)
            {
                activeFlyout.IsOpen = true;
            }
            else
            {
                activeFlyout.IsOpen = !activeFlyout.IsOpen;
            }
        }

        #endregion Flyout

        #region Style

        private void SaveStyleClick(object sender, RoutedEventArgs e)
        {
            _style.SaveStyle();
        }

        private void Theme(object sender, RoutedEventArgs e)
        {
            _style.SetTheme(sender, e);
        }

        private void AccentOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _style.SetAccent(sender, e);
        }

        #endregion Style
    }
}