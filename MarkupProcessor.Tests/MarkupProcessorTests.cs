using FluentAssertions;
using MarkupProcessor.Commands;
using MarkupProcessor.Controllers;
using MarkupProcessor.Data.Models;
using MarkupProcessor.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using System.Text;

namespace MarkupProcessor.Tests
{
    [TestClass]
    public class MarkupProcessorTests
    {
        private Mock<ILogger<MarkupProcessorController>> _markUpProcessorLogger = null!;
        private Mock<IMediator> _mediatr = null!;

        [TestInitialize]
        public void Setup()
        {
            _markUpProcessorLogger = new Mock<ILogger<MarkupProcessorController>>();
            _mediatr = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task WhenAMdFileIsReceived_ThenTheContentsShouldBeDeserialised()
        {
            var response = new HandlerResponse<MDContents> { Success = true };
            var flowDiagramId = Guid.NewGuid().ToString();
            _mediatr.Setup(x => x.Send(It.IsAny<AddMDContentCommand>(), It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(response));
            var controller = new MarkupProcessorController(_markUpProcessorLogger.Object, _mediatr.Object);

            var fileCollection = GetFileCollectionMock("text/markdown", "Test Md files\\TestMd.md", 1);

            controller.ControllerContext = this.RequestWithFileCollection(fileCollection);
            var sut = await controller.Post(flowDiagramId) as OkObjectResult;

            sut!.StatusCode.Should().Be(200);
            _mediatr.Verify(x => x.Send(It.Is<AddMDContentCommand>(x => x.MDContentsDto.FlowChartId == flowDiagramId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task WhenMultipleMdFilesAreReceived_ThenTheContentsShouldBeDeserialised()
        {
            var response = new HandlerResponse<MDContents> { Success = true };
            var flowDiagramId = Guid.NewGuid().ToString();
            _mediatr.Setup(x => x.Send(It.IsAny<AddMDContentCommand>(), It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(response));
            var controller = new MarkupProcessorController(_markUpProcessorLogger.Object, _mediatr.Object);

            var file = GetFileCollectionMock("text/markdown", "Test Md files\\TestMd.md", 2);

            controller.ControllerContext = this.RequestWithFileCollection(file);
            var sut = await controller.Post(flowDiagramId) as OkObjectResult;

            sut!.StatusCode.Should().Be(200);
            _mediatr.Verify(x => x.Send(It.Is<AddMDContentCommand>(x => x.MDContentsDto.FlowChartId == flowDiagramId), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [TestMethod]
        public async Task WhenAnMDFileIsReceived_AndThereIsNoJSONPresent_ThenTheRequestShouldNotBeProcessed()
        {
            var response = new HandlerResponse<MDContents> { Success = true };
            _mediatr.Setup(x => x.Send(It.IsAny<AddMDContentCommand>(), It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(response));
            var controller = new MarkupProcessorController(_markUpProcessorLogger.Object, _mediatr.Object);

            var file = GetFileCollectionMock("text/markdown", "Test Md files\\TestMdNoJson.md", 1);

            controller.ControllerContext = this.RequestWithFileCollection(file);
            var sut = await controller.Post(It.IsAny<string>()) as OkObjectResult;

            sut!.StatusCode.Should().Be(200);
            _mediatr.Verify(x => x.Send(It.IsAny<AddMDContentCommand>(), It.IsAny<CancellationToken>()), Times.Never);
            VerifyLogger("No JSON Content was found", LogLevel.Warning);
        }

        [TestMethod]
        public async Task WhenAnMDFileIsReceived_AndAnExceptionIsThrown_ThenTheRequestShouldNotBeProcessed()
        {
            _mediatr.Setup(x => x.Send(It.IsAny<AddMDContentCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception("New Exception"));
            var controller = new MarkupProcessorController(_markUpProcessorLogger.Object, _mediatr.Object);

            var file = GetFileCollectionMock("text/markdown", "Test Md files\\TestMd.md", 1);

            controller.ControllerContext = this.RequestWithFileCollection(file);
            var sut = await controller.Post(It.IsAny<string>()) as BadRequestObjectResult;

            sut!.StatusCode.Should().Be(400);
            sut!.Value!.Should().Be("New Exception");
            VerifyLogger("There was an issue with the request", LogLevel.Error);
        }

        [TestMethod]
        public async Task WhenAnMDFileIsReceived_AndNoFilesAreInTheCollection_ThenTheRequestShouldNotBeProcessed()
        {
            var response = new HandlerResponse<MDContents> { Success = true };
            _mediatr.Setup(x => x.Send(It.IsAny<AddMDContentCommand>(), It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(response));
            var controller = new MarkupProcessorController(_markUpProcessorLogger.Object, _mediatr.Object);

            var file = new FormFileCollection();

            controller.ControllerContext = this.RequestWithFileCollection(file);
            var sut = await controller.Post(It.IsAny<string>()) as OkObjectResult;

            sut!.StatusCode.Should().Be(200);
            _mediatr.Verify(x => x.Send(It.IsAny<AddMDContentCommand>(), It.IsAny<CancellationToken>()), Times.Never);

            VerifyLogger("No JSON Content was found", LogLevel.Warning);
        }

        private IFormFileCollection GetFileCollectionMock(string contentType, string filePath, int count)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            List<string> allLines = new List<string>();
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line;
                while ((line = reader.ReadLine()!) != null)
                {
                    allLines.Add(line); // Add to list.     
                }
            }

            byte[] dataAsBytes = allLines.SelectMany(s =>
                Encoding.UTF8.GetBytes(s + Environment.NewLine)).ToArray();

            var files = new FormFileCollection();
            for (var i = 0; i < count; i++)
            {
                files.Add(new FormFile(
                baseStream: new MemoryStream(dataAsBytes),
                baseStreamOffset: 0,
                length: dataAsBytes.Length,
                name: "Data",
                fileName: $"dummy{i}.md"
                )
                {
                    Headers = new HeaderDictionary(),
                    ContentType = contentType
                });
            }

            return files;
        }

        private ControllerContext RequestWithFileCollection(IFormFileCollection files)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("Content-Type", "multipart/form-data");
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(), files);
            var actx = new ActionContext(httpContext, new RouteData(), new ControllerActionDescriptor());
            return new ControllerContext(actx);
        }

        private void VerifyLogger(string message, LogLevel logLevel)
        {
            _markUpProcessorLogger.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == logLevel),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == message),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }
    }
}