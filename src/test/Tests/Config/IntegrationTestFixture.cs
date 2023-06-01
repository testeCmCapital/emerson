using Moq.AutoMock;
using System;
using Xunit;

namespace Tests.Config
{
    [CollectionDefinition(nameof(IntegrationServicesAppTestFixtureCollection))]
    public class IntegrationServicesAppTestFixtureCollection : ICollectionFixture<IntegrationTestFixture> { }

    public class IntegrationTestFixture : IDisposable
    {
        public readonly string RandString;

        public IntegrationTestFixture()
        {
            RandString = Guid.NewGuid().ToString().Substring(0, 18);
        }

        //public DomainService GetMockDomainService()
        //{
        //    return new AutoMocker().CreateInstance<DomainService>();
        //}

        public void Dispose()
        {
        }
    }
}
