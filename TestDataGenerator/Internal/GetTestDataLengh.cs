using System;

namespace TestDataGenerator.Internal
{
    public class GetTestDataLengh : ITestDataLengh
    {
        private readonly double? _lengthAsDouble;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        public GetTestDataLengh(double? lengthAsDouble)
        {
            if(lengthAsDouble == null)
            {
                throw new ArgumentNullException("lengthAsDouble");
            }
            _lengthAsDouble = lengthAsDouble;
        }

        public int Value
        {
            get { return Convert.ToInt32(_lengthAsDouble); }
        }
    }
}