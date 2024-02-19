namespace TestDataGenerator.Core;

/// <inheritdoc />
public class ConfigureTestDataGeneration : IConfigureTestDataGeneration
{
    /// <inheritdoc />
    public void RunFor([NotNull] IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<IGenerateTestData, GenerateTestData>();
        services.AddSingleton<IGenerateTestGuids, GenerateTestGuids>();
        services.AddSingleton<ITestData, TestData>();
        services.AddSingleton<ITestDataCharPool, TestDataCharPool>();
        services.AddSingleton<ITestDataLength, TestDataLength>();
        services.AddSingleton<ITestDataType, TestDataType>();
        services.AddSingleton<ITestDataTypeCollection, TestDataTypeCollection>();

        services.Chain<IChainHelperFor<string, string>>()
                .Add<TestDataForLetters>()
                .Add<TestDataForNumbers>()
                .Add<TestDataForCapitalLetters>()
                .Add<TestDataForSmallLetters>()
                .Add<TestDataForLettersAndNumbers>()
                .Add<TestDataForLettersNumbersSigns>()
                .Add<TestDataForGuidsDigits>()
                .Add<TestDataForGuidsHyphens>()
                .Add<TestDataForGuidsBraces>()
                .Add<TestDataForGuidsParentheses>()
                .Configure();
    }
}