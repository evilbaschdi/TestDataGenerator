using System;
using System.Windows;

namespace TestDataGenerator.Internal
{
    /// <inheritdoc />
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TestDataLength : ITestDataLength
    {
        private readonly double? _lengthAsDouble;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        public TestDataLength(double? lengthAsDouble)
        {
            _lengthAsDouble = lengthAsDouble ?? throw new ArgumentNullException(nameof(lengthAsDouble));
        }

        /// <inheritdoc />
        public int Value
        {
            get
            {
                var result = 1;
                try
                {
                    result = (int)(_lengthAsDouble ?? 1);
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