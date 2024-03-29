﻿using System.Text;

namespace TestDataGenerator.Core;

/// <inheritdoc />
public class GenerateTestGuids : IGenerateTestGuids
{
    private readonly StringBuilder _guids = new();
    private readonly ITestDataLength _testDataLength;

    /// <summary>
    ///     Constructor of the class
    /// </summary>
    /// <param name="testDataLength"></param>
    public GenerateTestGuids(ITestDataLength testDataLength)
    {
        _testDataLength = testDataLength ?? throw new ArgumentNullException(nameof(testDataLength));
    }

    /// <inheritdoc />
    public string ValueFor(string type)
    {
        ArgumentNullException.ThrowIfNull(type);

        for (var i = 0; i < _testDataLength.Value; i++)
        {
            _guids.Append($"{Guid.NewGuid().ToString(type)}");
            _guids.AppendLine();
        }

        return _guids.ToString();
    }
}