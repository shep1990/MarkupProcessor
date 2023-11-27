using FluentAssertions;
using MarkupProcessor.Application.Dto;
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
        private Mock<ILogger<MarkupProcessorController>> _markUpProcessor = null!;
        private Mock<IMediator> _mediatr = null!;

        [TestInitialize]
        public void Setup()
        {
            _markUpProcessor = new Mock<ILogger<MarkupProcessorController>>();
            _mediatr = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task WhenAMdFileIsReceived_ThenTheContentsShouldBeDeserialised()
        {
            var response = new HandlerResponse { Success = true };
            _mediatr.Setup(x => x.Send(It.IsAny<AddMDContentCommand>(), It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(response));
            var controller = new MarkupProcessorController(_markUpProcessor.Object, _mediatr.Object);

            var file = GetFileMock("text/markdown", "Test Md files\\TestMd.md");

            controller.ControllerContext = this.RequestWithFile(file);
            var sut = await controller.Post(It.IsAny<string>()) as OkObjectResult;

            sut!.StatusCode.Should().Be(200);
            _mediatr.Verify(x => x.Send(It.IsAny<AddMDContentCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task WhenAnMDFileIsReceived_AndThereIsNoJSONPresent_ThenTheRequestShouldNotBeProcessed()
        {
            var response = new HandlerResponse { Success = true };
            _mediatr.Setup(x => x.Send(It.IsAny<AddMDContentCommand>(), It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(response));
            var controller = new MarkupProcessorController(_markUpProcessor.Object, _mediatr.Object);

            var file = GetFileMock("text/markdown", "Test Md files\\TestMdNoJson.md");

            controller.ControllerContext = this.RequestWithFile(file);
            var sut = await controller.Post(It.IsAny<string>()) as OkObjectResult;

            sut!.StatusCode.Should().Be(200);
            _mediatr.Verify(x => x.Send(It.IsAny<AddMDContentCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [TestMethod]
        public async Task WhenAnMDFileIsReceived_AndAnExceptionIsThrown_ThenTheRequestShouldNotBeProcessed()
        {
            _mediatr.Setup(x => x.Send(It.IsAny<AddMDContentCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception("New Exception"));
            var controller = new MarkupProcessorController(_markUpProcessor.Object, _mediatr.Object);

            var file = GetFileMock("text/markdown", "Test Md files\\TestMd.md");

            controller.ControllerContext = this.RequestWithFile(file);
            var sut = await controller.Post(It.IsAny<string>()) as BadRequestObjectResult;

            sut!.StatusCode.Should().Be(400);
            sut!.Value!.Should().Be("New Exception");
        }

        private IFormFile GetFileMock(string contentType, string filePath)
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

            var file = new FormFile(
                baseStream: new MemoryStream(dataAsBytes),
                baseStreamOffset: 0,
                length: dataAsBytes.Length,
                name: "Data",
                fileName: "dummy.md"
            )
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };

            return file;
        }

        private ControllerContext RequestWithFile(IFormFile file)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("Content-Type", "multipart/form-data");
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(), new FormFileCollection { file });
            var actx = new ActionContext(httpContext, new RouteData(), new ControllerActionDescriptor());
            return new ControllerContext(actx);
        }
    }
}