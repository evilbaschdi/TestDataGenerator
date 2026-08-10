using Avalonia;
using EvilBaschdi.About.Avalonia.DependencyInjection;
using EvilBaschdi.Core.Avalonia.AppBuilderImplementations;
using TestDataGenerator.Avalonia.DependencyInjection;
using TestDataGenerator.Core;

namespace TestDataGenerator.Avalonia;

internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    //public static AppBuilder BuildAvaloniaApp()
    //    => AppBuilder.Configure<App>()
    //                 .UsePlatformDetect()
    //                 .LogToTrace()
    //                 .UseReactiveUI();

    // Avalonia configuration, don't remove; also used by visual designer.
    // ReSharper disable once MemberCanBePrivate.Global
    public static AppBuilder BuildAvaloniaApp()
        => new AppBuilderImplementationToUseReactiveUIWithMicrosoftDependencyResolver<App>()
            .ValueFor(serviceCollection =>
                      {
                          serviceCollection.AddAboutServices();
                          serviceCollection.AddAvaloniaServices();
                          serviceCollection.AddTestDataGeneration();
                      }
            );
}