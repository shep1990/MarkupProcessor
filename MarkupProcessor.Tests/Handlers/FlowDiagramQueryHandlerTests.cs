using FluentAssertions;
using MarkupProcessor.Application.Dto;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using MarkupProcessor.Handlers;
using MarkupProcessor.Queries;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupProcessor.Tests.Handlers
{
    [TestClass]
    public class FlowDiagramQueryHandlerTests
    {
        private Mock<IFlowDiagramInformationRepository> _flowDiagramInformationRepositionMock;
        private Mock<ILogger<FlowDiagramQueryHandler>> _logger;

        [TestInitialize]
        public void Setup()
        {
            _flowDiagramInformationRepositionMock = new Mock<IFlowDiagramInformationRepository>();
            _logger = new Mock<ILogger<FlowDiagramQueryHandler>>();
        }

        [TestMethod]
        public async Task WhenAFlowDiagramQueryIsConsumed_ThenTheDatShouldBeSuccessfullyRetrieved()
        {
            var queryRequest = new FlowDiagramQuery();
            List<FlowDiagram> flowDiagramList = SetUpFlowDiagramList();
            _flowDiagramInformationRepositionMock.Setup(x => x.Get()).Returns(Task.FromResult(flowDiagramList));

            var sut = new FlowDiagramQueryHandler(_flowDiagramInformationRepositionMock.Object, _logger.Object);
            var result = await sut.Handle(queryRequest, default);

            result.Success.Should().BeTrue();
            result.Data.Count.Should().Be(5);
            CheckOutputListValuesMatch(result.Data, flowDiagramList).Should().BeTrue();
        }

        public static bool CheckOutputListValuesMatch(List<FlowDiagramDto> dto, List<FlowDiagram> entity)
        {
            return dto.Select(x =>
            {
                return x.FlowDiagramName == entity.FirstOrDefault(e => e.Id == x.Id)!.FlowDiagramName;
            }).All(b => b == true);
        }

        private static List<FlowDiagram> SetUpFlowDiagramList()
        {
            var flowDiagramList = new List<FlowDiagram>();
            for (var i = 0; i < 5; i++)
            {
                flowDiagramList.Add(new FlowDiagram
                {
                    FlowDiagramName = $"flowDiagram{i}",
                    Id = Guid.NewGuid()
                });
            }

            return flowDiagramList;
        }
    }
}
