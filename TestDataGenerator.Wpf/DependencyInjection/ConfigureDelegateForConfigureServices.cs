using EvilBaschdi.About.Core.DependencyInjection;
using EvilBaschdi.About.Wpf.DependencyInjection;
using MahApps.Metro.Controls.Dialogs;
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