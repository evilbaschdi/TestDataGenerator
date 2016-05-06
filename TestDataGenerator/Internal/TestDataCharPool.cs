namespace TestDataGenerator.Internal
{
    /// <summary>
    /// </summary>
    public class TestDataCharPool : ITestDataCharPool
    {
        /// <summary>
        /// </summary>
        public string CapitalLetters
        {
            get { return "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
        }

        /// <summary>
        /// </summary>
        public string SmallLetters
        {
            get { return "abcdefghijklmnopqrstuvwyxz"; }
        }

        /// <summary>
        /// </summary>
        public string Numbers
        {
            get { return "1234567890"; }
        }

        /// <summary>
        /// </summary>
        public string Signs
        {
            get { return @"@€!§$%&/(){}[]\=?+*~#,;.:"; }
        }

        /// <summary>
        /// </summary>
        public string Letters
        {
            get { return $"{CapitalLetters}{SmallLetters}"; }
        }

        /// <summary>
        /// </summary>
        public string LettersNumbers
        {
            get { return $"{Letters}{Numbers}"; }
        }

        /// <summary>
        /// </summary>
        public string LettersNumbersSigns
        {
            get { return $"{LettersNumbers}{Signs}"; }
        }
    }
}