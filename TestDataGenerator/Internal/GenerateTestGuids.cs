using System;
using System.Text;

namespace TestDataGenerator.Internal
{
    /// <summary>
    /// </summary>
    public class GenerateTestGuids : IGenerator
    {
        private readonly int _count;
        private readonly string _type;
        private readonly StringBuilder _guids = new StringBuilder();

        /// <summary>
        /// </summary>
        /// <param name="count"></param>
        /// <param name="type"></param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count" /> is smaller or equal 0.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="type" /> is <see langword="null" />.</exception>
        public GenerateTestGuids(int count, string type)
        {
            if(count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if(type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            _count = count;
            _type = type;
        }

        /// <summary>
        /// </summary>
        public string Value
        {
            get
            {
                for(var i = 0; i < _count; i++)
                {
                    _guids.Append($"{Guid.NewGuid().ToString(_type)}");
                    _guids.AppendLine();
                }

                return _guids.ToString();
            }
        }
    }
}