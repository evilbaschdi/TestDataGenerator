﻿using System.Windows;
using TestDataGenerator.Wpf.DependencyInjection;
#if !DEBUG
using ControlzEx.Theming;
#endif

namespace TestDataGenerator.Wpf;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
// ReSharper disable once RedundantExtendsListEntry
public partial class App : Application
{
    private readonly IHandleAppExit _handleAppExit;
    private readonly IHandleAppStartup<Views.MainWindow> _handleAppStartup;
    private Views.MainWindow _mainWindow;

    /// <inheritdoc />
    public App()
    {
        IHostInstance hostInstance = new HostInstance();
        IConfigureDelegateForConfigureServices configureDelegateForConfigureServices = new ConfigureDelegateForConfigureServices();
        IConfigureServicesByHostBuilderAndConfigureDelegate configureServicesByHostBuilderAndConfigureDelegate =
            new ConfigureServicesByHostBuilderAndConfigureDelegate(hostInstance, configureDelegateForConfigureServices);

        ServiceProvider = configureServicesByHostBuilderAndConfigureDelegate.Value;

        _handleAppStartup = new HandleAppStartup<Views.MainWindow>(hostInstance);
        _handleAppExit = new HandleAppExit(hostInstance);
    }

    /// <summary>
    ///     ServiceProvider for DependencyInjection
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public static IServiceProvider ServiceProvider { get; set; }

    /// <inheritdoc />
    protected override async void OnStartup(StartupEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);
#if !DEBUG
        ThemeManager.Current.SyncTheme(ThemeSyncMode.SyncAll);
#endif

        _mainWindow = await _handleAppStartup.ValueFor(ServiceProvider);
        _mainWindow.Show();
    }

    /// <inheritdoc />
    protected override async void OnExit([NotNull] ExitEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);

        await _handleAppExit.Value();

        base.OnExit(e);
    }
}