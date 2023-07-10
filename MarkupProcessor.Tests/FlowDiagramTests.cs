using FluentAssertions;
using MarkupProcessor.Application.Dto;
using MarkupProcessor.Commands;
using MarkupProcessor.Controllers;
using MarkupProcessor.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupProcessor.Tests
{
    [TestClass]
    public class FlowDiagramTests
    {
        private Mock<ILogger<FlowDiagramController>> _flowDiagram = null!;
        private Mock<IMediator> _mediatr = null!;

        [TestInitialize]
        public void Setup()
        {
            _flowDiagram = new Mock<ILogger<FlowDiagramController>>();
            _mediatr = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task WhenANewFlowDiagramObjectIsPassed_ThenItShouldBePassedOverToTheCommandHandler()
        {
            var response = new HandlerResponse<FlowDiagramDto> { Success = true, Data = new FlowDiagramDto { Name = "MyFlowDiagram" } };
            _mediatr.Setup(x => x.Send(It.IsAny<FlowDiagramInformationCommand>(), It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(response));
            var controller = new FlowDiagramController(_flowDiagram.Object, _mediatr.Object);

            var flowDiagramMock = new FlowDiagramDto
            {
                Name = "MyFlowDiagram",
            };

            var sut = await controller.Post(flowDiagramMock) as HandlerResponse<FlowDiagramDto>;

            _mediatr.Verify(x => x.Send(It.Is<FlowDiagramInformationCommand>(x => CompareResults(flowDiagramMock, x.FlowDiagramInformationDto)), default), Times.Once);
        }

        private bool CompareResults(FlowDiagramDto expected, FlowDiagramDto actual)
        {
            return expected.Name == actual.Name;
        }
    }
}
