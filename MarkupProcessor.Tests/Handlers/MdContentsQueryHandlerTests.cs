using FluentAssertions;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using MarkupProcessor.Handlers;
using MarkupProcessor.Queries;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MarkupProcessor.Tests.Handlers
{
    [TestClass]
    public class MdContentsQueryHandlerTests
    {
        private Mock<IMarkupRepository> _markDownRepositoryMock;
        private Mock<ILogger<MDContentsQueryHandler>> _logger;

        [TestInitialize]
        public void Setup()
        {
            _markDownRepositoryMock = new Mock<IMarkupRepository>();
            _logger = new Mock<ILogger<MDContentsQueryHandler>>();
        }

        [TestMethod]
        public async Task WhenAMarkDownQueryRequestIsReceived_ThenTheDataShouldBeReturnedIfItExists()
        {
            var mdEventPayload = new MdPayload { Id = Guid.NewGuid(), EventName = "EventName" };
            var flowChartId = Guid.NewGuid().ToString();
            var expectedMdContents = new List<MDContents>
            {
                new MDContents
                (
                    flowChartId,
                    DateTime.UtcNow,
                    "1.0",
                    "Unit Test",
                    "Test",
                    JsonConvert.SerializeObject(mdEventPayload)
                )
            };
            var queryRequest = _markDownRepositoryMock.Setup(x => x.Get(flowChartId)).ReturnsAsync(expectedMdContents);

            var sut = new MDContentsQueryHandler(_markDownRepositoryMock.Object, _logger.Object);

            var result = await sut.Handle(new MDContentsQuery(flowChartId), default);

            result.Success.Should().BeTrue();
        }

        public class MdPayload
        {
            public Guid Id { get; set; }
            public string EventName { get; set; } = string.Empty;
        }
    }
}
