using System.Collections.ObjectModel;
using System.Windows;
using EvilBaschdi.About.Wpf;
using EvilBaschdi.Core.Wpf;
using EvilBaschdi.Core.Wpf.Mvvm.ViewModel;
using EvilBaschdi.Core.Wpf.Mvvm.ViewModel.Command;
using TestDataGenerator.Core;
using TestDataGenerator.Wpf.Internal;

namespace TestDataGenerator.Wpf.ViewModels;

/// <inheritdoc />
public class MainWindowViewModel : ApplicationLayoutViewModel
{
    private readonly ITestData _testData;
    private readonly ITestDataLength _testDataLength;
    private readonly ITestDataType _testDataType;
    private readonly ITestDataTypeCollection _testDataTypeCollection;
    private string _outputText;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="applicationLayout"></param>
    /// <param name="applicationStyle"></param>
    /// <param name="testData"></param>
    /// <param name="testDataLength"></param>
    /// <param name="testDataType"></param>
    /// <param name="testDataTypeCollection"></param>
    public MainWindowViewModel(
        [NotNull] IApplicationLayout applicationLayout,
        [NotNull] IApplicationStyle applicationStyle,
        [NotNull] ITestData testData,
        [NotNull] ITestDataLength testDataLength,
        [NotNull] ITestDataType testDataType,
        [NotNull] ITestDataTypeCollection testDataTypeCollection)
        : base(applicationLayout, applicationStyle, true, true)
    {
        _testDataLength = testDataLength ?? throw new ArgumentNullException(nameof(testDataLength));
        _testDataType = testDataType ?? throw new ArgumentNullException(nameof(testDataType));
        _testData = testData ?? throw new ArgumentNullException(nameof(testData));
        _testDataTypeCollection = testDataTypeCollection ?? throw new ArgumentNullException(nameof(testDataTypeCollection));
        AboutWindowCommand = CommandExtensions.Create(AboutWindowCommandExecute);
        GenerateTestDataCommand = CommandExtensions.Create(GenerateTestDataCommandExecute);
        CopyToClipboardCommand = CommandExtensions.Create(CopyToClipboardCommandExecute);
    }

    /// <summary>
    /// </summary>
    public ICommandViewModel AboutWindowCommand { get; set; }

    /// <summary>
    /// </summary>
    public ICommandViewModel GenerateTestDataCommand { get; set; }

    /// <summary>
    /// </summary>
    public ICommandViewModel CopyToClipboardCommand { get; set; }

    private void AboutWindowCommandExecute()
    {
        var aboutWindow = App.ServiceProvider.GetRequiredService<AboutWindow>();
        aboutWindow.ShowDialog();
    }

    private void GenerateTestDataCommandExecute()
    {
        _outputText = _testData.Value;
        OnPropertyChanged(nameof(OutputText));
    }

    private void CopyToClipboardCommandExecute()
    {
        Clipboard.SetText(OutputText);
    }

    /// <summary>
    ///     Binding for TestDataLength
    /// </summary>
    public double? TestDataLength
    {
        get => _testDataLength.Value;
        set
        {
            _testDataLength.Value = (int)(value ?? 1);
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Binding for TestDataType
    /// </summary>
    public string TestDataType
    {
        get => _testDataType.Value;
        set
        {
            _testDataType.Value = value ?? string.Empty;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Binding for Output
    /// </summary>
    public string OutputText
    {
        get => _outputText;
        set
        {
            if (value == _outputText)
            {
                return;
            }

            _outputText = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     TestDataTypeCollection
    /// </summary>
    public ObservableCollection<object> TestDataTypeCollection => new(_testDataTypeCollection.Value);
}