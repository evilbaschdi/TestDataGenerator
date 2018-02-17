using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shell;
using EvilBaschdi.Core.Extensions;
using EvilBaschdi.CoreExtended.AppHelpers;
using EvilBaschdi.CoreExtended.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using TestDataGenerator.Internal;
using TestDataGenerator.Properties;
using Unity;

namespace TestDataGenerator
{
    /// <inheritdoc cref="MetroWindow" />
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MainWindow : MetroWindow

    {
        
        private ProgressDialogController _controller;
        private string _dataType;
        private int _overrideProtection;
        private string _result;
        private double? _testDataLength;
        private UnityContainer _unityContainer;
        private readonly IApplicationStyle _applicationStyle;


        /// <inheritdoc />
        public MainWindow()
        {
            InitializeComponent();
            IAppSettingsBase appSettingsBase = new AppSettingsBase(Settings.Default);
            IApplicationStyleSettings applicationStyleSettings = new ApplicationStyleSettings(appSettingsBase);
            IThemeManagerHelper themeManagerHelper = new ThemeManagerHelper();
            _applicationStyle = new ApplicationStyle(this, Accent, ThemeSwitch, applicationStyleSettings, themeManagerHelper);
            _applicationStyle.Load(true);
            var linkerTime = Assembly.GetExecutingAssembly().GetLinkerTime();
            LinkerTime.Content = linkerTime.ToString(CultureInfo.InvariantCulture);
            Load();
        }

        private void Load()
        {
            TestDataLength.Focus();
            DataType.Text = "Letters";
            TestDataLength.Value = 1;
            _overrideProtection = 1;
        }

        private async void GenerateOutputOnClickAsync(object sender, RoutedEventArgs e)
        {
            await RunTestDataGenerationConfigurationAsync();

            //ITestDataType testDataType = new TestDataType(DataType.Text);
            //ITestDataLength testDataLength = new TestDataLength(TestDataLength.Value);
            //IGenerateTestData generateTestData = new GenerateTestData(testDataLength);
            //ITestDataCharPool testDataCharPool = new TestDataCharPool();
            //IChainHelperFor<string, string> testDataForLetters = new TestDataForLetters(null, testDataType, generateTestData, testDataCharPool);
            //IChainHelperFor<string, string> testDataForSmallLetters = new TestDataForSmallLetters(testDataForLetters, testDataType, generateTestData, testDataCharPool);
            //IChainHelperFor<string, string> testDataForCapitalLetters = new TestDataForCapitalLetters(testDataForSmallLetters, testDataType, generateTestData, testDataCharPool);
            //ITestData testData = new TestData(testDataForCapitalLetters);
            //Output.Text = testData.Value;
        }

        private async void TestDataLengthOnKeyDownAsync(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Return:
                    await RunTestDataGenerationConfigurationAsync();
                    break;

                case Key.Tab:
                    DataType.IsDropDownOpen = true;
                    break;
            }
        }

        private async void DataTypeOnKeyDownAsync(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Return:
                    await RunTestDataGenerationConfigurationAsync();
                    break;
            }
        }

        private void CopyToClipboardOnClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Output.Text);
        }

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable

        #region Process Generation

        private async Task RunTestDataGenerationConfigurationAsync()
        {
            TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Indeterminate;
            Cursor = Cursors.Wait;

            _unityContainer = new UnityContainer();
            _testDataLength = TestDataLength.Value;
            _dataType = DataType.Text;
            _unityContainer.RegisterInstance(TestDataLength.Value);
            _unityContainer.RegisterInstance(DataType.Text);


            var options = new MetroDialogSettings
                          {
                              ColorScheme = MetroDialogColorScheme.Accented
                          };

            var tokenSource = new CancellationTokenSource();

            MetroDialogOptions = options;
            _controller = await this.ShowProgressAsync("Please wait...", "Test data are getting generated.", true, options);
            _controller.SetIndeterminate();
            _controller.Canceled += (_, __) => tokenSource.Cancel();
            _controller.Closed += ControllerClosed;

            var token = tokenSource.Token;

            try
            {
                await Task.Run(() => RunTestDataGeneration(token), token);
            }
            catch (OperationCanceledException)
            {
                // Canceling an awaited Task throws an OperationCanceledException.
                // We just want nothing to happen, but the application shouldn't crash.
            }

            await _controller.CloseAsync();
        }

        private void RunTestDataGeneration(CancellationToken ct)
        {
            for (var i = 0; i < 10; i++)
            {
                if (ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                }
            }

            //var testDataContainer = new TestDataContainer(_unityContainer);
            //var container = testDataContainer.Value;

            ITestDataType testDataType = new TestDataType(_dataType);
            ITestDataLength testDataLength = new TestDataLength(_testDataLength);
            IGenerateTestData generateTestData = new GenerateTestData(testDataLength);
            IGenerateTestGuids generateTestGuids = new GenerateTestGuids(testDataLength);
            ITestDataCharPool testDataCharPool = new TestDataCharPool();
            IChainHelperFor<string, string> testDataForLetters = new TestDataForLetters(null, generateTestData, testDataCharPool);
            IChainHelperFor<string, string> testDataForNumbers = new TestDataForNumbers(testDataForLetters, generateTestData, testDataCharPool);
            IChainHelperFor<string, string> testDataForCapitalLetters = new TestDataForCapitalLetters(testDataForNumbers, generateTestData, testDataCharPool);
            IChainHelperFor<string, string> testDataForSmallLetters = new TestDataForSmallLetters(testDataForCapitalLetters, generateTestData, testDataCharPool);
            IChainHelperFor<string, string> testDataForLettersAndNumbers = new TestDataForLettersAndNumbers(testDataForSmallLetters, generateTestData, testDataCharPool);
            IChainHelperFor<string, string> testDataForLettersNumbersSigns = new TestDataForLettersNumbersSigns(testDataForLettersAndNumbers, generateTestData, testDataCharPool);
            IChainHelperFor<string, string> testDataForGuidsDigits = new TestDataForGuidsDigits(testDataForLettersNumbersSigns, generateTestGuids);
            IChainHelperFor<string, string> testDataForGuidsHyphens = new TestDataForGuidsHyphens(testDataForGuidsDigits, generateTestGuids);
            IChainHelperFor<string, string> testDataForGuidsBraces = new TestDataForGuidsBraces(testDataForGuidsHyphens, generateTestGuids);
            IChainHelperFor<string, string> testDataForGuidsParentheses = new TestDataForGuidsParentheses(testDataForGuidsBraces, generateTestGuids);

            ITestData testData = new TestData(testDataForGuidsParentheses, testDataType);
            _result = testData.Value;


            // _result = container.Resolve<ITestData>().Value;
        }

        private void ControllerClosed(object sender, EventArgs e)
        {
            TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
            TaskbarItemInfo.ProgressValue = 1;
            Cursor = Cursors.Arrow;

            Output.Text = _result;
        }

        #endregion Process Generation

        #region Flyout

        private void ToggleSettingsFlyoutClick(object sender, RoutedEventArgs e)
        {
            ToggleFlyout(0);
        }

        private void ToggleFlyout(int index, bool stayOpen = false)
        {
            var activeFlyout = (Flyout) Flyouts.Items[index];
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

            activeFlyout.IsOpen = activeFlyout.IsOpen && stayOpen || !activeFlyout.IsOpen;
        }

        #endregion Flyout

        #region MetroStyle

        private void SaveStyleClick(object sender, RoutedEventArgs e)
        {
            if (_overrideProtection == 0)
            {
                return;
            }

            _applicationStyle.SaveStyle();
        }

        private void Theme(object sender, EventArgs e)
        {
            if (_overrideProtection == 0)
            {
                return;
            }

            _applicationStyle.SetTheme(sender);
        }

        private void AccentOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_overrideProtection == 0)
            {
                return;
            }

            _applicationStyle.SetAccent(sender, e);
        }

        #endregion MetroStyle
    }
}