using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var fileReader = new Mock<IFileReader>(MockBehavior.Strict);
            fileReader.Setup(a => a.TryGetFile(It.IsAny<string>())).Returns(false);

            var result = fileReader.Object.TryGetFile(path);

            Assert.Equal(false, result);
        }

        [Fact]
        public void ShouldReturnCorrectExceptionForBadInput()
        {
            string input = string.Empty;
            var fileReader = new Mock<IFileReader>(MockBehavior.Strict);
            fileReader.Setup(a => a.TryGetFile(It.IsAny<string>())).Returns(false);
            Exception ex = Assert.Throws<ArgumentNullException>(() => fileReader.Object.TryGetFile(input));

            Assert.Equal("path",ex.Message);
        }

    }
}
