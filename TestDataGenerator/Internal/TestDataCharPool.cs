namespace TestDataGenerator.Internal
{
    public static class TestDataCharPool
    {
        public static string CapitalLetters
        {
            get { return "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
        }

        public static string SmallLetters
        {
            get { return "abcdefghijklmnopqrstuvwyxz"; }
        }

        public static string Numbers
        {
            get { return "1234567890"; }
        }

        public static string Signs
        {
            get { return @"@€!§$%&/(){}[]\=?+*~#,;.:"; }
        }

        public static string Letters
        {
            get { return string.Format("{0}{1}", CapitalLetters, SmallLetters); }
        }

        public static string LettersNumbers
        {
            get { return string.Format("{0}{1}", Letters, Numbers); }
        }

        public static string LettersNumbersSigns
        {
            get { return string.Format("{0}{1}", LettersNumbers, Signs); }
        }
    }
}