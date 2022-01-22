using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;
using EvilBaschdi.CoreExtended;
using EvilBaschdi.CoreExtended.Controls.About;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using TestDataGenerator.Internal;

namespace TestDataGenerator
{
    /// <inheritdoc cref="MetroWindow" />
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    // ReSharper disable once InconsistentNaming
    public partial class MainWindow : MetroWindow

    {
        private ProgressDialogController _controller;
        private string _dataType;
        private string _result;
        private double? _testDataLength;

        /// <inheritdoc />
        public MainWindow()
        {
            InitializeComponent();

            IRoundCorners roundCorners = new RoundCorners();
            IApplicationStyle style = new ApplicationStyle(roundCorners, true, true);
            style.Run();

            Load();
        }

        private void Load()
        {
            TestDataLength.Focus();
            DataType.Text = "Letters";
            TestDataLength.Value = 1;
        }

        private async void GenerateOutputOnClickAsync(object sender, RoutedEventArgs e)
        {
            await RunTestDataGenerationConfigurationAsync();
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

            _testDataLength = TestDataLength.Value;
            _dataType = DataType.Text;

            var options = new MetroDialogSettings
                          {
                              ColorScheme = MetroDialogColorScheme.Accented
                          };

            var tokenSource = new CancellationTokenSource();

            MetroDialogOptions = options;
            _controller =
                await this.ShowProgressAsync("Please wait...", "Test data are getting generated.", true, options);
            _controller.SetIndeterminate();
            _controller.Canceled += (_, _) => tokenSource.Cancel();
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

            IServiceCollection serviceCollection = new ServiceCollection();
            IConfigureTestDataGeneration configureTestDataGeneration = new ConfigureTestDataGeneration();
            configureTestDataGeneration.RunFor((serviceCollection, _dataType, _testDataLength));

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            ITestData testData = serviceProvider.GetRequiredService<ITestData>();

            if (testData != null)
            {
                _result = testData.Value;
            }
        }

        private void ControllerClosed(object sender, EventArgs e)
        {
            TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
            TaskbarItemInfo.ProgressValue = 1;
            Cursor = Cursors.Arrow;

            Output.Text = _result;
        }

        private void AboutWindowClick(object sender, RoutedEventArgs e)
        {
            ICurrentAssembly currentAssembly = new CurrentAssembly();
            IAboutContent aboutContent = new AboutContent(currentAssembly);
            IAboutModel aboutModel = new AboutViewModel(aboutContent);
            var aboutWindow = new AboutWindow(aboutModel);

            aboutWindow.ShowDialog();
        }

        #endregion Process Generation
    }
}