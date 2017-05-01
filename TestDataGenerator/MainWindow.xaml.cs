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
using EvilBaschdi.Core.Application;
using EvilBaschdi.Core.Wpf;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Unity;
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
        private readonly IMetroStyle _style;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly ISettings _coreSettings;

        private int _overrideProtection;
        private ProgressDialogController _controller;
        private string _result;
        private UnityContainer _unityContainer;

        /// <summary>
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _coreSettings = new CoreSettings(Properties.Settings.Default);
            var themeManagerHelper = new ThemeManagerHelper();
            _style = new MetroStyle(this, Accent, ThemeSwitch, _coreSettings, themeManagerHelper);
            _style.Load(true);
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

        #region Process Generation

        private async Task RunTestDataGenerationConfigurationAsync()
        {
            TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Indeterminate;
            Cursor = Cursors.Wait;

            _unityContainer = new UnityContainer();
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

            var testDataContainer = new TestDataContainer(_unityContainer);
            var container = testDataContainer.Value;
            _result = container.Resolve<ITestData>().Value;
        }

        private void ControllerClosed(object sender, EventArgs e)
        {
            TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
            TaskbarItemInfo.ProgressValue = 1;
            Cursor = Cursors.Arrow;

            Output.Text = _result;
        }

        #endregion Process Generation

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

        #region MetroStyle

        private void SaveStyleClick(object sender, RoutedEventArgs e)
        {
            if (_overrideProtection == 0)
            {
                return;
            }
            _style.SaveStyle();
        }

        private void Theme(object sender, EventArgs e)
        {
            if (_overrideProtection == 0)
            {
                return;
            }
            var routedEventArgs = e as RoutedEventArgs;
            if (routedEventArgs != null)
            {
                _style.SetTheme(sender, routedEventArgs);
            }
            else
            {
                _style.SetTheme(sender);
            }
        }

        private void AccentOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_overrideProtection == 0)
            {
                return;
            }
            _style.SetAccent(sender, e);
        }

        #endregion MetroStyle
    }
}