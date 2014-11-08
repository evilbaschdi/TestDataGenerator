using System;

namespace TestDataGenerator.Internal
{
    public class GenerateTestGuids : IGenerator
    {
        private readonly int _count;
        private readonly string _type;
        private string _guids;

        public GenerateTestGuids(int count, string type)
        {
            _count = count;
            _type = type;
        }

        public string Value
        {
            get
            {
                for(var i = 0; i < _count; i++)
                {
                    _guids += string.Format("{0}{1}", Guid.NewGuid().ToString(_type), Environment.NewLine);
                }

                return _guids;
            }
        }
    }
}