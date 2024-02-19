using System.Windows;
using MahApps.Metro.Controls;
using TestDataGenerator.Wpf.Internal.Core;
using TestDataGenerator.Wpf.ViewModels;

namespace TestDataGenerator.Wpf.Views;

/// <inheritdoc cref="MetroWindow" />
/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
// ReSharper disable once RedundantExtendsListEntry
// ReSharper disable once InconsistentNaming
public partial class MainWindow : IOnLoaded
{
    private readonly IServiceProvider _serviceProvider;

    /// <inheritdoc />
    public MainWindow()
    {
        InitializeComponent();

        _serviceProvider = App.ServiceProvider;
        Loaded += RunFor;
    }

    /// <inheritdoc />
    public void RunFor(object sender, RoutedEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(sender);
        ArgumentNullException.ThrowIfNull(e);

        DataContext = ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, typeof(MainWindowViewModel));
    }
}