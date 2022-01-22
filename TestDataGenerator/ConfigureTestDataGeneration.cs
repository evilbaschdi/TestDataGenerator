using System;
using EvilBaschdi.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TestDataGenerator.Internal;

namespace TestDataGenerator;

public class ConfigureTestDataGeneration : IConfigureTestDataGeneration
{
    private string _dataType;
    private double? _testDataLength;

    /// <inheritdoc />
    public void RunFor((IServiceCollection serviceCollection, string testDataType, double? testDataLength) value)
    {
        var (services, testDataType, testDataLength) = value;
        _dataType = testDataType;
        _testDataLength = testDataLength;

        services.AddSingleton<ITestDataType, TestDataType>(InitTestDataType);
        services.AddSingleton<ITestDataLength, TestDataLength>(InitTestDataLength);
        services.AddSingleton<IGenerateTestData, GenerateTestData>();
        services.AddSingleton<IGenerateTestGuids, GenerateTestGuids>();
        services.AddSingleton<ITestDataCharPool, TestDataCharPool>();
        services.AddSingleton<ITestData, TestData>();

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

    private TestDataLength InitTestDataLength(IServiceProvider serviceProvider) => new(_testDataLength);

    private TestDataType InitTestDataType(IServiceProvider serviceProvider) => new(_dataType);
}