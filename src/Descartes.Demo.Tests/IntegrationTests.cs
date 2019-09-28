using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Descartes.Demo.Models;
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
        [MemberData(nameof(ErrorParameters))]
        public async Task ReturnsErrorForWrongUrl(int id, string side)
        {
            // arrange & act
            var response = await PutAsync($"{DiffRoute}/{id}/{side}");

            // assert
            Assert.True(!response.IsSuccessStatusCode);
            if (string.IsNullOrEmpty(side))
            {
                Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
            }
            else
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        public static IEnumerable<object[]> ErrorParameters => new List<object[]>
        {
            new object[] { -1, string.Empty},
            new object[] { 0, "asbcd" },
            new object[] { 1, string.Empty },
            new object[] { -100, "left" },
            new object[] { 0, "right" }
        };

        [Theory]
        [MemberData(nameof(BadRequestParameters))]
        public async Task Returns400ForWrongInputData(string diffData)
        {
            // arrange & act
            var response = await PutAsync($"{DiffRoute}/1/left", diffData);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        public static IEnumerable<object[]> BadRequestParameters => new List<object[]>
        {
            new object[]{"đžćasdo2"},
            new object[]{ string.Empty },
            new object[]{"[]"},
            new object[]{"{}"}
        };

        [Theory]
        [MemberData(nameof(ValidDataParameters))]
        public async Task AcceptsValidDiffData(int id, Side side, string diffData)
        {
            // arrange & act
            var response = await PutAsync($"{DiffRoute}/{id}/{side}", diffData);

            // assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        public static IEnumerable<object[]> ValidDataParameters => new List<object[]>
        {
            new object[] { 1, Side.Left, GetValidBase64String()},
            new object[] { 2, Side.Left, GetValidBase64String()},
            new object[] { 3, Side.Left, GetValidBase64String()},
            new object[] { 1, Side.Right, GetValidBase64String()},
            new object[] { 2, Side.Right, GetValidBase64String()},
        };

        private async Task<HttpResponseMessage> PutAsync(string url, string diff = null)
        {
            using var client = _factory.CreateClient();

            var data = new DiffRequest { Data = diff };
            var content = new StringContent(JsonSerializer.Serialize<DiffRequest>(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await client.PutAsync(url, content).ConfigureAwait(false);
        }

        private static string GetValidBase64String()
        {
            var rnd = new Random();
            // up to 100 bytes
            var limit = rnd.Next(100);
            var bytes = new byte[limit];
            rnd.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
