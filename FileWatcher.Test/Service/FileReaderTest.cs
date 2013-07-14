using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileWatcher.Models;
using FileWatcher.Service;
using Moq;
using Xunit;
using FileWatcher.Interface;

namespace FileWatcher.Test
{
    public class FileReaderTest
    {

        [Fact]
        public void ShouldNotReturnTrueIfPathDoesNotExist()
        {
            string path = @"C:\Users\Nino\Desktop\test2";

            IFileReader fileReader = new FileReader();
            var result = fileReader.TryGetFile(path);

            Assert.Equal(false, result);
        }

        [Fact]
        public void GetContentShouldReturnXmlDataType()
        {
            var xml = this.TestVector();

            var fileReader = new Mock<IFileReader>(MockBehavior.Strict);
            fileReader.Setup(s => s.GetContent(It.IsAny<string>())).Returns(xml);

            var result = fileReader.Object.GetContent(string.Empty);
            
            Assert.Equal(result.GetType(), typeof(XmlData));

        }

        [Fact]
        public void ShouldThrowNullReferenceExceptionIfResultNull()
        {
            // need to test the inner of the method
            //var xml = this.TestVector();
            //var fileReader = new Mock<IFileReader>(MockBehavior.Strict);
            //fileReader.Setup(s => s.GetContent(It.IsAny<string>())).Throws<NullReferenceException>();

            //var ex =
            //    Assert.Throws<NullReferenceException>(() => fileReader.Object.GetContent(string.Empty));

            //Assert.Equal(ex.GetType(), typeof(ArgumentNullException));
        }

        [Fact]
        public void ShouldReturnCorrectExceptionForBadInput()
        {
            string input = string.Empty;
            IFileReader reader = new FileReader();

            ArgumentException ex = Assert.Throws<ArgumentNullException>(() => reader.TryGetFile(input));

            Assert.Equal("path", ex.ParamName);
        }

        [Fact]
        public void ShouldReturnArgumentNullException()
        {
            string param = string.Empty;
            string paramName = "input";

            IFileReader reader = new FileReader();

            ArgumentException ex = Assert.Throws<ArgumentNullException>(() => reader.PreCheckInput(param, paramName));

            Assert.Equal(paramName, ex.ParamName);
            Assert.Equal(ex.GetType(), typeof(ArgumentNullException));

        }

        public XmlData TestVector()
        {
            var xml = new XmlData()
            {
                Id = 1,
                To = "Nino",
                From = "Cat",
                Heading = "Food",
                Body = "Don't forget to feed me!",
                AppId = Guid.NewGuid()
            };

            return xml;
        }
    }
}
