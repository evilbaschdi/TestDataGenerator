using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Input;
using EvilBaschdi.About.Avalonia;
using EvilBaschdi.Core.Avalonia;
using ReactiveUI;
using TestDataGenerator.Core;

// ReSharper disable UnusedMember.Global

namespace TestDataGenerator.Avalonia.ViewModels;

/// <inheritdoc cref="ViewModelBase" />
public class MainWindowViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ITestDataLength _testDataLength;
    private readonly ITestDataType _testDataType;
    private readonly ITestData _testData;
    private readonly IMainWindowByApplicationLifetime _mainWindowByApplicationLifetime;
    private string _output = string.Empty;
    private Window _mainWindow;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="testDataLength"></param>
    /// <param name="testDataType"></param>
    /// <param name="testData"></param>
    /// <param name="mainWindowByApplicationLifetime"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public MainWindowViewModel([NotNull] IServiceProvider serviceProvider,
                               [NotNull] ITestDataLength testDataLength,
                               [NotNull] ITestDataType testDataType,
                               [NotNull] ITestData testData,
                               [NotNull] IMainWindowByApplicationLifetime mainWindowByApplicationLifetime)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _testDataLength = testDataLength ?? throw new ArgumentNullException(nameof(testDataLength));
        _testDataType = testDataType ?? throw new ArgumentNullException(nameof(testDataType));
        _testData = testData ?? throw new ArgumentNullException(nameof(testData));
        _mainWindowByApplicationLifetime = mainWindowByApplicationLifetime ?? throw new ArgumentNullException(nameof(mainWindowByApplicationLifetime));
        AboutWindowCommand = ReactiveCommand.Create(AboutWindowCommandExecute);
        GenerateTestDataCommand = ReactiveCommand.Create(GenerateTestDataCommandExecute);
        CopyToClipboardCommand = ReactiveCommand.Create(CopyToClipboardCommandExecuteAsync);
    }

    /// <summary>
    /// </summary>
    public ReactiveCommand<Unit, Unit> AboutWindowCommand { get; set; }

    /// <summary>
    /// </summary>
    public ReactiveCommand<Unit, Unit> GenerateTestDataCommand { get; set; }

    /// <summary>
    /// </summary>
    public ReactiveCommand<Unit, Unit> CopyToClipboardCommand { get; set; }

    private void AboutWindowCommandExecute()
    {
        var aboutWindow = _serviceProvider.GetRequiredService<AboutWindow>();
        _mainWindow = _mainWindowByApplicationLifetime.Value;
        if (_mainWindow != null)
        {
            aboutWindow.ShowDialog(_mainWindow);
        }
    }

    private void GenerateTestDataCommandExecute()
    {
        Output = _testData.Value;
    }

    private async void CopyToClipboardCommandExecuteAsync()
    {
        _mainWindow = _mainWindowByApplicationLifetime.Value;
        var clipboard = _mainWindow.Clipboard;
        if (_mainWindow == null || clipboard == null)
        {
            return;
        }

        var dataObject = new DataObject();
        dataObject.Set(DataFormats.Text, _output);
        await clipboard.SetDataObjectAsync(dataObject);
    }

    /// <summary>
    ///     Binding for TestDataLength
    /// </summary>
    public double? TestDataLength
    {
        get => _testDataLength.Value;
        set => _testDataLength.Value = (int)(value ?? 1);
    }

    /// <summary>
    ///     Binding for TestDataType
    /// </summary>
    public string TestDataType
    {
        get => _testDataType.Value;
        set => _testDataType.Value = value ?? string.Empty;
    }

    /// <summary>
    ///     Binding for Output
    /// </summary>
    public string Output
    {
        get => _output;
        set => this.RaiseAndSetIfChanged(ref _output, value);
    }

    /// <summary>
    ///     TestDataTypeCollection
    /// </summary>
    public ObservableCollection<object> TestDataTypeCollection => new([
        "Letters",
        "Numbers",
        "Capital Letters",
        "Small Letters",
        "Letters and Numbers",
        "Letters, Numbers and Signs",
        "Guids (digits)",
        "Guids (hyphens)",
        "Guids (braces)",
        "Guids (parentheses)"
    ]);
}