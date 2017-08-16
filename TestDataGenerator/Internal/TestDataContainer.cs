using System;
using Microsoft.Practices.Unity;

namespace TestDataGenerator.Internal
{
    /// <inheritdoc />
    public class TestDataContainer : ITestDataContainer
    {
        private readonly UnityContainer _unityContainer;

        /// <summary>
        ///     Constructor of the class
        /// </summary>
        /// <param name="unityContainer"></param>
        public TestDataContainer(UnityContainer unityContainer)
        {
            _unityContainer = unityContainer ?? throw new ArgumentNullException(nameof(unityContainer));
        }

        /// <inheritdoc />
        public UnityContainer Value
        {
            get
            {
                _unityContainer.RegisterType<IGenerateTestData, GenerateTestData>();
                _unityContainer.RegisterType<IGenerateTestGuids, GenerateTestGuids>();
                _unityContainer.RegisterType<ITestDataCharPool, TestDataCharPool>();
                _unityContainer.RegisterType<ITestDataLength, TestDataLength>();
                _unityContainer.RegisterType<ITestDataType, TestDataType>();

                _unityContainer.RegisterType<IChainHelperFor<string, string>, TestDataForLetters>("TestDataForLetters");
                _unityContainer.RegisterType<IChainHelperFor<string, string>, TestDataForSmallLetters>("TestDataForSmallLetters",
                    new InjectionConstructor(new ResolvedParameter<IChainHelperFor<string, string>>("TestDataForLetters")));
                _unityContainer.RegisterType<IChainHelperFor<string, string>, TestDataForCapitalLetters>("TestDataForCapitalLetters",
                    new InjectionConstructor(new ResolvedParameter<IChainHelperFor<string, string>>("TestDataForSmallLetters")));
                _unityContainer.RegisterType<IChainHelperFor<string, string>, TestDataForNumbers>("TestDataForNumbers",
                    new InjectionConstructor(new ResolvedParameter<IChainHelperFor<string, string>>("TestDataForCapitalLetters")));

                _unityContainer.RegisterType<ITestData, TestData>();

                return _unityContainer;
            }
        }
    }
}