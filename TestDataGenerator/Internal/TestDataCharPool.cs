namespace TestDataGenerator.Internal
{
    public class TestDataCharPool : ITestDataCharPool
    {
        public string CapitalLetters
        {
            get { return "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
        }

        public string SmallLetters
        {
            get { return "abcdefghijklmnopqrstuvwyxz"; }
        }

        public string Numbers
        {
            get { return "1234567890"; }
        }

        public string Signs
        {
            get { return @"@€!§$%&/(){}[]\=?+*~#,;.:"; }
        }

        public string Letters
        {
            get { return string.Format("{0}{1}", CapitalLetters, SmallLetters); }
        }

        public string LettersNumbers
        {
            get { return string.Format("{0}{1}", Letters, Numbers); }
        }

        public string LettersNumbersSigns
        {
            get { return string.Format("{0}{1}", LettersNumbers, Signs); }
        }
    }
}