using EvilBaschdi.Core;
using Microsoft.Extensions.DependencyInjection;

namespace TestDataGenerator;

public interface IConfigureTestDataGeneration : IRunFor<(IServiceCollection serviceCollection, string testDataType, double? testDataLength)>
{
}