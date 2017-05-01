namespace TestDataGenerator.Internal
{
    /// <summary>
    /// </summary>
    public class TestDataCharPool : ITestDataCharPool
    {
        /// <summary>
        /// </summary>
        public string CapitalLetters => "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// </summary>
        public string SmallLetters => "abcdefghijklmnopqrstuvwyxz";

        /// <summary>
        /// </summary>
        public string Numbers => "1234567890";

        /// <summary>
        /// </summary>
        public string Signs => @"@€!§$%&/(){}[]\=?+*~#,;.:";

        /// <summary>
        /// </summary>
        public string Letters => $"{CapitalLetters}{SmallLetters}";

        /// <summary>
        /// </summary>
        public string LettersNumbers => $"{Letters}{Numbers}";

        /// <summary>
        /// </summary>
        public string LettersNumbersSigns => $"{LettersNumbers}{Signs}";
    }
}