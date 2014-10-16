using System;

namespace TestDataGenerator.Internal
{
    public class GetTestDataLengh : ITestDataLengh
    {
        private readonly string _lengthAsString;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        public GetTestDataLengh(string lengthAsString)
        {
            if (lengthAsString == null)
            {
                throw new ArgumentNullException("lengthAsString");
            }
            _lengthAsString = lengthAsString;
        }

        public int Value
        {
            get { return Convert.ToInt32(_lengthAsString); }
        }
    }
}