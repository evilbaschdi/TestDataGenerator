using System;

namespace TestDataGenerator.Internal
{
    public class GenerateTestData : IGenerator
    {
        private readonly int _length;
        private readonly string _charPool;
        private static Random _random;
        private char[] _buffer;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        public GenerateTestData(int length, string charPool)
        {
            if(charPool == null)
            {
                throw new ArgumentNullException("charPool");
            }
            _length = length;
            _charPool = charPool;
        }

        public string Value
        {
            get
            {
                _random = new Random();

                _buffer = new char[_length];

                for(var i = 0; i < _length; i++)
                {
                    _buffer[i] = _charPool[_random.Next(_charPool.Length)];
                }
                return new string(_buffer);
            }
        }
    }
}