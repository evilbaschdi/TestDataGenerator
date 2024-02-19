using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;
using EvilBaschdi.About.Core;
using EvilBaschdi.About.Core.Models;
using EvilBaschdi.About.Wpf;
using EvilBaschdi.Core.Wpf;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using TestDataGenerator.Core;
using TestDataGenerator.Wpf.Internal.Core;

namespace TestDataGenerator.Wpf;

/// <inheritdoc cref="MetroWindow" />
/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
// ReSharper disable once RedundantExtendsListEntry
// ReSharper disable once InconsistentNaming
public partial class MainWindow : IOnLoaded
{
    private ProgressDialogController _controller;
    private string _dataType;
    private string _result;
    private int _testDataLength;
    private readonly IApplicationLayout _applicationLayout;
    private readonly IServiceProvider _serviceProvider;

    /// <inheritdoc />
    public MainWindow()
    {
        InitializeComponent();

        IApplicationStyle applicationStyle = new ApplicationStyle();
        _applicationLayout = new ApplicationLayout();
        applicationStyle.Run();
        _applicationLayout.RunFor((this, true, true));

        Load();

        _serviceProvider = App.ServiceProvider;
        Loaded += RunFor;
    }

    private void Load()
    {
        TestDataLength.Focus();
        DataType.SetCurrentValue(System.Windows.Controls.ComboBox.TextProperty, "Letters");
        TestDataLength.SetCurrentValue(NumericUpDown.ValueProperty, (double?)1);
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
                DataType.SetCurrentValue(System.Windows.Controls.ComboBox.IsDropDownOpenProperty, true);
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
        TaskbarItemInfo.SetCurrentValue(TaskbarItemInfo.ProgressStateProperty, TaskbarItemProgressState.Indeterminate);
        SetCurrentValue(CursorProperty, Cursors.Wait);

        _testDataLength = (int)(TestDataLength.Value ?? 1);
        _dataType = DataType.Text;

        var options = new MetroDialogSettings
                      {
                          ColorScheme = MetroDialogColorScheme.Accented
                      };

        var tokenSource = new CancellationTokenSource();

        SetCurrentValue(MetroDialogOptionsProperty, options);
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

        ITestData testData = _serviceProvider.GetRequiredService<ITestData>();
        ITestDataLength testDataLength = _serviceProvider.GetRequiredService<ITestDataLength>();
        ITestDataType testDataType = _serviceProvider.GetRequiredService<ITestDataType>();
        testDataLength.Value = _testDataLength;
        testDataType.Value = _dataType;

        if (testData != null)
        {
            _result = testData.Value;
        }
    }

    private void ControllerClosed(object sender, EventArgs e)
    {
        TaskbarItemInfo.SetCurrentValue(TaskbarItemInfo.ProgressStateProperty, TaskbarItemProgressState.Normal);
        TaskbarItemInfo.SetCurrentValue(TaskbarItemInfo.ProgressValueProperty, (double)1);
        SetCurrentValue(CursorProperty, Cursors.Arrow);

        Output.SetCurrentValue(System.Windows.Controls.TextBox.TextProperty, _result);
    }

    private void AboutWindowClick(object sender, RoutedEventArgs e)
    {
        ICurrentAssembly currentAssembly = new CurrentAssembly();
        IAboutContent aboutContent = new AboutContent(currentAssembly);
        IAboutViewModel aboutModel = new AboutViewModel(aboutContent);
        IApplyMicaBrush applyMicaBrush = new ApplyMicaBrush();
        var aboutWindow = new AboutWindow(aboutModel, _applicationLayout, applyMicaBrush);

        aboutWindow.ShowDialog();
    }

    #endregion Process Generation

    /// <inheritdoc />
    public void RunFor(object sender, RoutedEventArgs e)
    {
        if (sender == null)
        {
            throw new ArgumentNullException(nameof(sender));
        }

        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        //DataContext = ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, typeof(MainWindowViewModel));
    }
}