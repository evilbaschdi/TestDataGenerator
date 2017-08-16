namespace TestDataGenerator.Internal
{
    /// <inheritdoc />
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TestDataCharPool : ITestDataCharPool
    {
        /// <inheritdoc />
        public string CapitalLetters => "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <inheritdoc />
        public string SmallLetters => "abcdefghijklmnopqrstuvwyxz";

        /// <inheritdoc />
        public string Numbers => "1234567890";

        /// <inheritdoc />
        public string Signs => @"@€!§$%&/(){}[]\=?+*~#,;.:";

        /// <inheritdoc />
        public string Letters => $"{CapitalLetters}{SmallLetters}";

        /// <inheritdoc />
        public string LettersNumbers => $"{Letters}{Numbers}";

        /// <inheritdoc />
        public string LettersNumbersSigns => $"{LettersNumbers}{Signs}";
    }
}