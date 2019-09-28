using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Descartes.Demo.Controllers;
using Descartes.Demo.Models;
using Descartes.Demo.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Descartes.Demo.Tests
{
    public class UnitTests
    {
        private Mock<IDiffService> _diffServiceMock;
        private DiffController _target;

        public UnitTests()
        {
            _diffServiceMock = new Mock<IDiffService>();
            _target = new DiffController(_diffServiceMock.Object);
        }

        [Fact]
        public async Task DiffControllerValidatesInput()
        {
            // arrange
            _target.ModelState.AddModelError("error", "error");

            // act
            var result = await _target.Put(1, Side.Left, new DiffRequest { Data = string.Empty });

            // assert
            Assert.True(result is BadRequestObjectResult);
        }

        [Fact]
        public async Task DiffControllerCallsService()
        {
            var id = 12;
            var side = Side.Right;
            var data = new DiffRequest { Data = "AA" };
            // arrange
            _diffServiceMock.Setup(x => x.AddDiffAsync(id, side, data))
                            .Returns(Task.FromResult(null as object))
                            .Verifiable();

            // act
            var result = await _target.Put(id, side, data);

            // assert
            _diffServiceMock.Verify(x => x.AddDiffAsync(id, side, data), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CorrectResultParmaters))]
        public async Task ReturnsCorrectResult(int id, DiffResponse response, Type expectedType)
        {
            // arrange
            _diffServiceMock.Setup(x => x.GetDiffAsync(id)).Returns(Task.FromResult(response));

            // act
            var result = await _target.Get(id);

            // assert
            Assert.Equal(expectedType.FullName, result.GetType().FullName);
        }

        public static IEnumerable<object[]> CorrectResultParmaters => new List<object[]>
        {
            new object[] { 10, new DiffResponse(new Difference[] {}), typeof(OkObjectResult)},
            new object[] { 1, null, typeof(NotFoundResult)}
        };
    }
}