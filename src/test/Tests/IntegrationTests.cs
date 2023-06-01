using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Tests.Config;
using Microsoft.AspNetCore.Mvc.Testing;
using Services;

namespace Tests
{
    [TestCaseOrderer("IntegrationTests.Config.PriorityOrderer", "IntegrationTests")]
    [Collection(nameof(IntegrationServicesAppTestFixtureCollection))]
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestFixture _testFixture;
        public readonly WebApplicationFactory<Startup> _factory;

        public IntegrationTests(IntegrationTestFixture testFixture, WebApplicationFactory<Startup> factory)
        {
            _testFixture = testFixture;
            _factory = factory;
        }

        [TestPriority(1)]
        [Fact(DisplayName = "DESCRIPTION")]
        [Trait("CATEGORY", "CATEGORY DESCRIPTION")]
        public void Test1()
        {
            //Arrange

            //Act

            //Asserts
        }
    }
}
