using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
        private readonly IMetroStyle _style;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly ISettings _coreSettings;
        private readonly BackgroundWorker _bw;
        private int _overrideProtection;
        private int _executionCount;
        private ProgressDialogController _controller;
        private string _result;
        private UnityContainer _unityContainer;

        /// <summary>
        /// </summary>
        public MainWindow()
        {
            _coreSettings = new CoreSettings();
            InitializeComponent();
            _bw = new BackgroundWorker();
            _style = new MetroStyle(this, Accent, ThemeSwitch, _coreSettings);
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

        private void CallBackgroundWorkerConfiguration()
        {
            _unityContainer = new UnityContainer();
            _unityContainer.RegisterInstance(TestDataLength.Value);
            _unityContainer.RegisterInstance(DataType.Text);
            TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Indeterminate;
            var configureBackroundWorker = ConfigureBackroundWorker();
            if(configureBackroundWorker.IsCompleted || configureBackroundWorker.IsCanceled)
            {
                configureBackroundWorker.Dispose();
            }
        }

        private void RunTestDataGeneration()
        {
            var testDataContainer = new TestDataContainer(_unityContainer);
            var container = testDataContainer.Value;
            _result = container.Resolve<ITestData>().Value;
        }

        #region BackroundWorker

        private async Task ConfigureBackroundWorker()
        {
            _executionCount++;

            Cursor = Cursors.Wait;

            if(_executionCount == 1)
            {
                _bw.DoWork += (o, args) => RunTestDataGeneration();
                _bw.WorkerReportsProgress = true;
                _bw.WorkerSupportsCancellation = true;
                _bw.RunWorkerCompleted += BackgroundWorkerRunWorkerCompleted;
            }
            var options = new MetroDialogSettings
                          {
                              ColorScheme = MetroDialogColorScheme.Theme
                          };

            MetroDialogOptions = options;
            _controller = await this.ShowProgressAsync("Please wait...", "Testdata are getting generated.", true, options);
            _controller.SetIndeterminate();
            _controller.Canceled += ControllerCanceled;
            _bw.RunWorkerAsync();
        }

        private void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _controller.CloseAsync();
            _controller.Closed += ControllerClosed;
        }

        #endregion BackroundWorker

        #region Process Controller

        private void ControllerClosed(object sender, EventArgs e)
        {
            TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
            TaskbarItemInfo.ProgressValue = 1;
            Cursor = Cursors.Arrow;

            Output.Text = _result;
        }

        private void ControllerCanceled(object sender, EventArgs e)
        {
            _bw.CancelAsync();
        }

        #endregion Process Controller

        private void GenerateOutputOnClick(object sender, RoutedEventArgs e)
        {
            CallBackgroundWorkerConfiguration();
        }

        private void TestDataLengthOnKeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Return:
                    CallBackgroundWorkerConfiguration();
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
                    CallBackgroundWorkerConfiguration();
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
            if(activeFlyout == null)
            {
                return;
            }

            foreach(
                var nonactiveFlyout in
                Flyouts.Items.Cast<Flyout>()
                       .Where(nonactiveFlyout => nonactiveFlyout.IsOpen && nonactiveFlyout.Name != activeFlyout.Name))
            {
                nonactiveFlyout.IsOpen = false;
            }

            if(activeFlyout.IsOpen && stayOpen)
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
            if(_overrideProtection == 0)
            {
                return;
            }
            _style.SaveStyle();
        }

        private void Theme(object sender, EventArgs e)
        {
            if(_overrideProtection == 0)
            {
                return;
            }
            var routedEventArgs = e as RoutedEventArgs;
            if(routedEventArgs != null)
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
            if(_overrideProtection == 0)
            {
                return;
            }
            _style.SetAccent(sender, e);
        }

        #endregion MetroStyle
    }
}