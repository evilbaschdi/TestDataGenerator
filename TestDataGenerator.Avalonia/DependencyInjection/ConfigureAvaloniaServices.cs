using EvilBaschdi.Core.Avalonia;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TestDataGenerator.Avalonia.ViewModels;

namespace TestDataGenerator.Avalonia.DependencyInjection;

/// <inheritdoc />
public class ConfigureAvaloniaServices : IConfigureAvaloniaServices
{
    /// <inheritdoc />
    public void RunFor(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddSingleton<IHandleOsDependentTitleBar, HandleOsDependentTitleBar>();
        services.TryAddSingleton<IApplicationLayout, ApplicationLayout>();
        services.TryAddSingleton<IMainWindowByApplicationLifetime, MainWindowByApplicationLifetime>();

        services.AddSingleton<MainWindowViewModel>();
    }
}