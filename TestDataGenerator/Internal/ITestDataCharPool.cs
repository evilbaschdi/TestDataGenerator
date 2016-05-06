namespace TestDataGenerator.Internal
{
    /// <summary>
    /// </summary>
    public interface ITestDataCharPool
    {
        /// <summary>
        /// </summary>
        string CapitalLetters { get; }

        /// <summary>
        /// </summary>
        string SmallLetters { get; }

        /// <summary>
        /// </summary>
        string Numbers { get; }

        /// <summary>
        /// </summary>
        string Signs { get; }

        /// <summary>
        /// </summary>
        string Letters { get; }

        /// <summary>
        /// </summary>
        string LettersNumbers { get; }

        /// <summary>
        /// </summary>
        string LettersNumbersSigns { get; }
    }
}