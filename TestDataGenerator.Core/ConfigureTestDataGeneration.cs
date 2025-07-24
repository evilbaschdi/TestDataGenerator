namespace TestDataGenerator.Core;

/// <summary />
public static class ConfigureTestDataGeneration
{
    /// <summary />
    public static void AddTestDataGeneration(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<IGenerateTestData, GenerateTestData>();
        services.AddSingleton<IGenerateTestGuids, GenerateTestGuids>();
        services.AddSingleton<ITestData, TestData>();
        services.AddSingleton<ITestDataCharPool, TestDataCharPool>();
        services.AddSingleton<ITestDataLength, TestDataLength>();
        services.AddSingleton<ITestDataType, TestDataType>();
        services.AddSingleton<ITestDataTypeCollection, TestDataTypeCollection>();

        services.Chain<ITestDataFor>()
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
                .Add<TestDataFor>()
                .Configure();
    }
}