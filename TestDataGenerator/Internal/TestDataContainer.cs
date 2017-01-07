using System;
using Microsoft.Practices.Unity;

namespace TestDataGenerator.Internal
{
    /// <summary>
    /// </summary>
    public class TestDataContainer : ITestDataContainer
    {
        private readonly UnityContainer _unityContainer;

        /// <summary>
        ///     Constructor of the class
        /// </summary>
        /// <param name="unityContainer"></param>
        public TestDataContainer(UnityContainer unityContainer)
        {
            if (unityContainer == null)
            {
                throw new ArgumentNullException(nameof(unityContainer));
            }
            _unityContainer = unityContainer;
        }

        /// <summary>
        /// </summary>
        public UnityContainer Value
        {
            get
            {
                _unityContainer.RegisterType<ITestDataLengh, TestDataLengh>();
                _unityContainer.RegisterType<ITestDataType, TestDataType>();
                _unityContainer.RegisterType<ITestDataCharPool, TestDataCharPool>();
                _unityContainer.RegisterType<ITestData, TestData>();

                return _unityContainer;
            }
        }
    }
}