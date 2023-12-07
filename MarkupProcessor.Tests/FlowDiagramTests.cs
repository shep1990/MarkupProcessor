using FluentAssertions;
using FluentValidation.Results;
using MarkupProcessor.Application.Dto;
using MarkupProcessor.Commands;
using MarkupProcessor.Controllers;
using MarkupProcessor.Handlers;
using MarkupProcessor.Validators;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MarkupProcessor.Tests
{
    [TestClass]
    public class FlowDiagramTests
    {
        private Mock<ILogger<FlowDiagramController>> _flowDiagram = null!;
        private Mock<IMediator> _mediatr = null!;
        private FlowDiagramValidator _validator = null!;
        private Guid flowDiagramId;

        [TestInitialize]
        public void Setup()
        {
            _flowDiagram = new Mock<ILogger<FlowDiagramController>>();
            _mediatr = new Mock<IMediator>();
            _validator = new FlowDiagramValidator(); 

            flowDiagramId = Guid.NewGuid();
            var response = new HandlerResponse<FlowDiagramDto> { Success = true, Data = new FlowDiagramDto(flowDiagramId, "MyFlowDiagram")};
            _mediatr.Setup(x => x.Send(It.IsAny<FlowDiagramInformationCommand>(), It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(response));
        }

        [TestMethod]
        public async Task WhenANewFlowDiagramObjectIsPassed_ThenItShouldBePassedOverToTheCommandHandler()
        {
            var controller = new FlowDiagramController(_flowDiagram.Object, _mediatr.Object, _validator);

            var flowDiagramMock = new FlowDiagramDto(flowDiagramId, "MyFlowDiagram");

            var sut = await controller.Post(flowDiagramMock) as OkObjectResult;

            _mediatr.Verify(x => x.Send(It.Is<FlowDiagramInformationCommand>(x => x.FlowDiagramInformationDto.Name == flowDiagramMock.Name), default), Times.Once);
            ((FlowDiagramDto)sut!.Value!).Id.Should().Be(flowDiagramMock.Id);
            ((FlowDiagramDto)sut!.Value!).Name.Should().Be(flowDiagramMock.Name);
            sut.StatusCode.Should().Be(200);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public async Task WhenANewFlowDiagramObjectIsPassed_AndTheValidationFails_ThenTheExpectedValidationMessageWillBeReturned(string name)
        {
            var controller = new FlowDiagramController(_flowDiagram.Object, _mediatr.Object, _validator);

            var flowDiagramMock = new FlowDiagramDto(flowDiagramId, name);

            var sut = await controller.Post(flowDiagramMock) as BadRequestObjectResult;

            _mediatr.Verify(x => x.Send(It.IsAny<FlowDiagramInformationCommand>(), default), Times.Never);
            sut!.StatusCode.Should().Be(400);
            ((List<ValidationFailure>)sut!.Value!).First().ErrorMessage.Should().Be("'Name' must not be empty.");          
        }

        [TestMethod]
        public async Task WhenANewFlowDiagramObjectIsPassed_AndAnExceptionGetsThrown_ThenTheExpectedMessageShouldBeReturned()
        {
            _mediatr.Setup(x => x.Send(It.IsAny<FlowDiagramInformationCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception("New Exception"));
            var controller = new FlowDiagramController(_flowDiagram.Object, _mediatr.Object, _validator);

            var flowDiagramMock = new FlowDiagramDto(flowDiagramId, "MyFlowDiagram");

            var sut = await controller.Post(flowDiagramMock) as BadRequestObjectResult;

            sut!.StatusCode.Should().Be(400);
            sut!.Value!.Should().Be("New Exception");
        }
    }
}
