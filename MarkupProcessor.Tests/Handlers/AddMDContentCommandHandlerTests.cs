using FluentAssertions;
using MarkupProcessor.Commands;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using MarkupProcessor.Handlers;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json.Nodes;

namespace MarkupProcessor.Tests.Handlers
{
    [TestClass]
    public class AddMDContentCommandHandlerTests
    {
        private Mock<IMarkupRepository> _markupRepository = null!;
        private Mock<ILogger<AddMDContentCommandHandler>> _logger = null!;

        [TestInitialize]
        public void Setup()
        {
            _markupRepository = new Mock<IMarkupRepository>();
            _logger = new Mock<ILogger<AddMDContentCommandHandler>>();
        }

        [TestMethod]
        public async Task WhenTheMDContentEventIsHandled_ThenTheResultShouldContainTheExpectedResults()
        {
            var command = AddMdCommand();
            _markupRepository.Setup(x => x.Add(It.IsAny<MDContents>())).Returns(Task.FromResult(CreateMdContentObject(command)));

            var sut = new AddMDContentCommandHandler(_markupRepository.Object, _logger.Object);

            var result = await sut.Handle(command, default);

            result.Success.Should().BeTrue();
            CheckResults(command, result);
        }

        [TestMethod]
        public async Task WhenTheMDContentEventIsHandled_AndAnExceptionIsThrown_ThenItShouldBeHandled()
        {
            var command = AddMdCommand();
            _markupRepository.Setup(x => x.Add(It.IsAny<MDContents>())).Throws(new Exception("New Exception"));

            var sut = new AddMDContentCommandHandler(_markupRepository.Object, _logger.Object);

            var result = await sut.Handle(command, default);

            result.Success.Should().BeFalse();
            VerifyLogger("There was an issue with the request", LogLevel.Error);
        }

        private static bool CheckResults(AddMDContentCommand command, HandlerResponse<MDContents> result)
        {
            return result.Data.SourceSystem == command.MDContentsDto.SourceSystem &&
            result.Data.CreationDate == command.MDContentsDto.CreationDate &&
            result.Data.FlowChartId == command.MDContentsDto.FlowChartId &&
            result.Data.Payload == command.MDContentsDto.Payload;
        }

        private static MDContents CreateMdContentObject(AddMDContentCommand command)
        {
            return new MDContents(
                command.MDContentsDto.FlowChartId,
                command.MDContentsDto.CreationDate,
                command.MDContentsDto.Version,
                command.MDContentsDto.SourceSystem,
                command.MDContentsDto.EventName,
                command.MDContentsDto.Payload as string);
        }

        private AddMDContentCommand AddMdCommand()
        {
            return new AddMDContentCommand(new MDContentsDto
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.UtcNow,
                FlowChartId = Guid.NewGuid().ToString(),
                SourceSystem = "UnitTest",
                Version = "1",
                Payload = "{demandId: 1234}"
            });
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
