using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Descartes.Demo.Tests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string DiffRoute = "v1/diff";
        private readonly WebApplicationFactory<Startup> _factory;
        public IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [MemberData(nameof(Parameters))]
        public async Task Returns404ForWrongUrl(int id, string side)
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var response = await client.PutAsync($"{DiffRoute}/{id}/{side}", null);

            // assert
            Assert.True(!response.IsSuccessStatusCode);
            if (string.IsNullOrEmpty(side))
            {
                Assert.Equal(response.StatusCode, HttpStatusCode.MethodNotAllowed);
            }
            else
            {
                Assert.Equal(response.StatusCode, HttpStatusCode.NotFound);
            }
        }

        public static IEnumerable<object[]> Parameters => new List<object[]>
        {
            new object[] { -1, string.Empty},
            new object[] { 0, "asbcd" },
            new object[] { 1, string.Empty },
            new object[] { -100, "left" },
            new object[] { 0, "right" }
        };
    }
}
