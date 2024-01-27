using FluentAssertions;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using MarkupProcessor.Handlers;
using Microsoft.Extensions.Logging;
using Moq;

namespace MarkupProcessor.Tests.Handlers
{
    [TestClass]
    public class FlowDiagramInformationCommandHandlerTests
    {
        private Mock<IFlowDiagramInformationRepository> _flowDiagramInformationRepositionMock;
        private Mock<ILogger<FlowDiagramInformationCommandHandler>> _logger;

        [TestInitialize]
        public void Setup()
        {
            _flowDiagramInformationRepositionMock = new Mock<IFlowDiagramInformationRepository>();
            _logger = new Mock<ILogger<FlowDiagramInformationCommandHandler>>();
        }

        [TestMethod]
        public async Task WhenAFlowDiagramInformationCommandIsConsumed_ThenTheDatShouldBeStored()
        {
            var flowDiagramName = "TestFlowDiagram";
            _flowDiagramInformationRepositionMock.Setup(x => x.Add(It.IsAny<FlowDiagram>())).Returns(Task.FromResult(new FlowDiagram(flowDiagramName)));

            var sut = new FlowDiagramInformationCommandHandler(_flowDiagramInformationRepositionMock.Object, _logger.Object);
            var result = await sut.Handle(new Commands.FlowDiagramInformationCommand(new FlowDiagram(flowDiagramName)), default);

            result.Data.FlowDiagramName.Should().Be(flowDiagramName);
            result.Data.Id.Should().NotBe(Guid.Empty);
        }

        [TestMethod]
        public async Task WhenTheMDContentEventIsHandled_AndAnExceptionIsThrown_ThenItShouldBeHandled()
        {
            var flowDiagramName = "TestFlowDiagram";
            _flowDiagramInformationRepositionMock.Setup(x => x.Add(It.IsAny<FlowDiagram>())).Throws(new Exception("New Exception"));

            var sut = new FlowDiagramInformationCommandHandler(_flowDiagramInformationRepositionMock.Object, _logger.Object);
            var result = await sut.Handle(new Commands.FlowDiagramInformationCommand(new FlowDiagram(flowDiagramName)), default);

            result.Success.Should().BeFalse();
            VerifyLogger("There was an issue with the request", LogLevel.Error);
        }

        private void VerifyLogger(string message, LogLevel logLevel)
        {
            _logger.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == logLevel),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == message),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }
    }
}
