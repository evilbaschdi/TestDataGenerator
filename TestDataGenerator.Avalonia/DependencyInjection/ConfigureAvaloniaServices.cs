using EvilBaschdi.Core.Avalonia;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TestDataGenerator.Avalonia.ViewModels;

namespace TestDataGenerator.Avalonia.DependencyInjection;

/// <summary />
public static class ConfigureAvaloniaServices
{
    /// <summary />
    public static void AddAvaloniaServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddSingleton<IHandleOsDependentTitleBar, HandleOsDependentTitleBar>();
        services.TryAddSingleton<IApplicationLayout, ApplicationLayout>();
        services.TryAddSingleton<IMainWindowByApplicationLifetime, MainWindowByApplicationLifetime>();

        services.AddSingleton<MainWindowViewModel>();
    }
}