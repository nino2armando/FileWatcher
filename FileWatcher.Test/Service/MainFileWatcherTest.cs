using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FileWatcher.Interface;
using FileWatcher.Models;
using FileWatcher.Service;
using Moq;
using Xunit;

namespace FileWatcher.Test.Service
{
    public class MainFileWatcherTest
    {
        [Fact]
        public void SetConfigurationShoulReceiveConfigurationSetting()
        {
            Type mainFileWatcher = (typeof(MainFileWatcher));
            MethodInfo setConfiguration = mainFileWatcher.GetMethod("SetConfiguration");

            ParameterInfo[] parameters = setConfiguration.GetParameters();
            var firstParam = parameters[0];

            Assert.Equal(setConfiguration.ReturnType, typeof(void));
            Assert.Equal(parameters.Length , 1);
            Assert.Equal(firstParam.ParameterType.FullName, typeof(ConfigurationSetting).FullName);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionForNullInput()
        {
            ConfigurationSetting setting = null;
            var fileReader = new Mock<IFileReader>(MockBehavior.Strict);
            var uow = new Mock<IUnitOfWork>(MockBehavior.Strict);
            IMainFileWatcher fileWatcher = new MainFileWatcher(fileReader.Object, uow.Object);

            var ex = Assert.Throws<ArgumentNullException>(() => fileWatcher.SetConfiguration(setting));

            Assert.Equal(ex.GetType(), typeof(ArgumentNullException));
            Assert.Equal(ex.ParamName, "setting");
        }

        [Fact]
        public void DisplayMessageShouldRecieveFileSystemEventArgs()
        {
            Type fileWatcher = (typeof (MainFileWatcher));
            MethodInfo method = fileWatcher.GetMethod("DisplayMessage");
            ParameterInfo[] parameters = method.GetParameters();

            var firstParam = parameters[0];

            Assert.Equal(method.ReturnType , typeof(void));
            Assert.Equal(parameters.Length, 1);
            Assert.Equal(firstParam.ParameterType.FullName, typeof(FileSystemEventArgs).FullName);

        }
    }
}
