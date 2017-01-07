using System;
using System.Windows;

namespace TestDataGenerator.Internal
{
    /// <summary>
    /// </summary>
    public class TestDataLengh : ITestDataLengh
    {
        private readonly double? _lengthAsDouble;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        public TestDataLengh(double? lengthAsDouble)
        {
            if (lengthAsDouble == null)
            {
                throw new ArgumentNullException(nameof(lengthAsDouble));
            }
            _lengthAsDouble = lengthAsDouble;
        }

        /// <summary>
        /// </summary>
        public int Value
        {
            get
            {
                int result = 1;
                try
                {
                    result = (int) (_lengthAsDouble ?? 1);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                return result;
            }
        }
    }
}