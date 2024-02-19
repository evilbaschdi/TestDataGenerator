namespace TestDataGenerator.Core;

/// <inheritdoc />
public class GenerateTestData : IGenerateTestData
{
    private static Random _random;
    private readonly ITestDataLength _testDataLength;
    private char[] _buffer;

    /// <summary>
    ///     Constructor of the class
    /// </summary>
    /// <param name="testDataLength"></param>
    public GenerateTestData(ITestDataLength testDataLength)
    {
        _testDataLength = testDataLength ?? throw new ArgumentNullException(nameof(testDataLength));
    }

    /// <inheritdoc />
    public string ValueFor(string charPool)
    {
        ArgumentNullException.ThrowIfNull(charPool);

        var length = _testDataLength.Value;
        _random = new();

        _buffer = new char[length];

        for (var i = 0; i < length; i++)
        {
            _buffer[i] = charPool[_random.Next(charPool.Length)];
        }

        return new(_buffer);
    }
}