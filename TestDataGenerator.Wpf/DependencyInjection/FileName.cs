using EvilBaschdi.About.Core.DependencyInjection;
using EvilBaschdi.About.Wpf.DependencyInjection;
using EvilBaschdi.Core.Wpf;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using TestDataGenerator.Core;

namespace TestDataGenerator.Wpf.DependencyInjection;

/// <inheritdoc />
public class ConfigureDelegateForConfigureServices : IConfigureDelegateForConfigureServices
{
    /// <inheritdoc />
    public void RunFor([NotNull] HostBuilderContext _, IServiceCollection serviceCollection)
    {
        ArgumentNullException.ThrowIfNull(_);
        ArgumentNullException.ThrowIfNull(serviceCollection);

        serviceCollection.AddSingleton(_ => DialogCoordinator.Instance);

        IConfigureWpfServices configureWpfServices = new ConfigureWpfServices();
        configureWpfServices.RunFor(serviceCollection);

        IConfigureAboutServices configureAboutServices = new ConfigureAboutServices();
        configureAboutServices.RunFor(serviceCollection);

        IConfigureTestDataGeneration configureTestDataGeneration = new ConfigureTestDataGeneration();
        configureTestDataGeneration.RunFor(serviceCollection);
    }
}

/// <inheritdoc />
public interface IConfigureWpfServices : IConfigureServiceCollection
{
}

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

        services.AddTransient(typeof(MainWindow));
    }
}