using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using EvilBaschdi.About.Avalonia.DependencyInjection;
using EvilBaschdi.About.Core.DependencyInjection;
using TestDataGenerator.Avalonia.DependencyInjection;
using TestDataGenerator.Avalonia.ViewModels;
using TestDataGenerator.Avalonia.Views;
using TestDataGenerator.Core;

namespace TestDataGenerator.Avalonia;

/// <inheritdoc />
public class App : Application
{
    /// <inheritdoc />
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    /// </summary>
    public static IServiceProvider ServiceProvider { get; set; }

    /// <inheritdoc />
    public override void OnFrameworkInitializationCompleted()
    {
        IServiceCollection serviceCollection = new ServiceCollection();

        IConfigureAboutServices configureAboutServices = new ConfigureAboutServices();
        IConfigureAvaloniaServices configureAvaloniaServices = new ConfigureAvaloniaServices();
        IConfigureTestDataGeneration configureTestDataGeneration = new ConfigureTestDataGeneration();

        configureAboutServices.RunFor(serviceCollection);
        configureAvaloniaServices.RunFor(serviceCollection);
        configureTestDataGeneration.RunFor(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindow = new MainWindow
                             {
                                 DataContext = ServiceProvider.GetRequiredService<MainWindowViewModel>()
                             };

            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}