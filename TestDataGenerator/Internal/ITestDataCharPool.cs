namespace TestDataGenerator.Internal
{
    public interface ITestDataCharPool
    {
        string CapitalLetters { get; }
        string SmallLetters { get; }
        string Numbers { get; }
        string Signs { get; }
        string Letters { get; }
        string LettersNumbers { get; }
        string LettersNumbersSigns { get; }
    }
}