using EvilBaschdi.Core.Wpf;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TestDataGenerator.Wpf.ViewModels;
using TestDataGenerator.Wpf.Views;

namespace TestDataGenerator.Wpf.DependencyInjection;

/// <inheritdoc />
public class ConfigureWpfServices : IConfigureWpfServices
{
    /// <inheritdoc />
    public void RunFor([NotNull] IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddSingleton<IApplicationLayout, ApplicationLayout>();
        services.TryAddSingleton<IApplicationStyle, ApplicationStyle>();
        services.TryAddSingleton<IApplyMicaBrush, ApplyMicaBrush>();

        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient(typeof(MainWindow));
    }
}