using FluentAssertions;
using MarkupProcessor.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Moq;
using System.IO;
using System.Text;

namespace MarkupProcessor.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<ILogger<MarkupProcessorController>> _markUpProcessor;

        [TestInitialize]
        public void Setup()
        {
            _markUpProcessor = new Mock<ILogger<MarkupProcessorController>>();
        }

        [TestMethod]
        public async Task WhenAMdFileIsReceived_ThenTheContentsShouldBeDeserialised()
        {
            var controller = new MarkupProcessorController(_markUpProcessor.Object);

            var file = GetFileMock("text/markdown");

            var sut = await controller.Get(file);

            sut.Should().NotBeNull();
            Assert.AreEqual("9604648153", sut.Payload["demandId"].ToString());
            Assert.AreEqual("Cancelled", sut.Payload["Status"].ToString()); 
        }

        private IFormFile GetFileMock(string contentType)
        {
            FileStream fileStream = new FileStream("C:\\git\\MarkupProcessor\\MarkupProcessor.Tests\\Test Md files\\TestMd.md", FileMode.Open);
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