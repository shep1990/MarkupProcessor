using FluentAssertions;
using MarkupProcessor.Application;
using MarkupProcessor.Application.Commands;
using MarkupProcessor.Controllers;
using MarkupProcessor.Data;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Moq;
using System.IO;
using System.Text;

namespace MarkupProcessor.Tests
{
    [TestClass]
    public class MarkupProcessorTests
    {
        private Mock<ILogger<MarkupProcessorController>> _markUpProcessor;
        private Mock<IMediator> _mediatr;

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

            var file = GetFileMock("text/markdown");

            var sut = await controller.Post(file) as OkObjectResult;

            sut.StatusCode.Should().Be(200);
            _mediatr.Verify(x => x.Send(It.IsAny<AddMDContentCommand>(), It.IsAny<CancellationToken>()), Times.Once);

        }

        private IFormFile GetFileMock(string contentType)
        {
            FileStream fileStream = new FileStream("C:\\git\\MarkupProcessor2\\MarkupProcessor.Tests\\Test Md files\\TestMd.md", FileMode.Open);
            List<string> allLines = new List<string>();
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
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
    }
}