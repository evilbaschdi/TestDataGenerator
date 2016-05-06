using System;

namespace TestDataGenerator.Internal
{
    /// <summary>
    /// </summary>
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
                throw new ArgumentNullException(nameof(lengthAsDouble));
            }
            _lengthAsDouble = lengthAsDouble;
        }

        /// <summary>
        /// </summary>
        public int Value
        {
            get { return Convert.ToInt32(_lengthAsDouble); }
        }
    }
}