using Xunit;
using DelphiSupervisorV6;
using System.Diagnostics;
using System.Linq;
using NSubstitute;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

namespace TestCommands
{
    public class UnitTestCommands
    {
        [Fact]
        public void ShowById_ReturProcessById()
        {
            //Arrange
            var processProvider = Substitute.For<IProcessProvider>();
            var view = Substitute.For<IView>();
            var configWatcher = Substitute.For<IConfigWatcher>();
            var processInfo = new ProcessInfo("GiveMeJob", 123, 6945);
            processProvider.GetByID(123).Returns(processInfo);
            var sut = new Commands(view,processProvider, configWatcher);

            //Act 
            sut.ShowById(123);

            //Assert
            view.Received().ShowOne(processInfo);
        }

        [Fact]
        public void ShowAll_ReturnAllProcess()
        {
            //Arrange
            var processProvider = Substitute.For<IProcessProvider>();
            var view = Substitute.For<IView>();
            var configWatcher = Substitute.For<IConfigWatcher>();
            var processInfo = new List<ProcessInfo>() { new ProcessInfo("Give", 111, 6543), new ProcessInfo("Me", 222, 4367), new ProcessInfo("Job", 333, 8433) };
            processProvider.GetAllProcesses().Returns(processInfo);
            var sut = new Commands(view,processProvider,configWatcher);

            //Act
            sut.ShowAll();

            //Assert
            view.Received().ShowAll(processInfo);
        }

        [Fact]
        public void ShowByName_ReturnProcessByName()
        {
            //Arrange
            var processProvider = Substitute.For<IProcessProvider>();
            var view = Substitute.For<IView>();
            var configWatcher = Substitute.For<IConfigWatcher>();
            var processInfo = new List<ProcessInfo>() { new ProcessInfo("Junior", 111, 6543), new ProcessInfo("Junior", 222, 4367), new ProcessInfo("Junior", 333, 8433) };
            processProvider.GetProcessByName("Junior").Returns(processInfo);
            var sut = new Commands(view, processProvider, configWatcher);

            //Act
            sut.ProcessByName("Junior");

            //Assert
            view.Received().ShowAll(processInfo);
        }

        [Fact]
        public void StartProcess_ReturnStartProcess()
        {
            //Arrange 
            var processProvider = Substitute.For<IProcessProvider>();
            var view = Substitute.For<IView>();
            var configWatcher = Substitute.For<IConfigWatcher>();
            var processInfo = new ProcessInfo("GiveMeJob", 123, 5745);
            processProvider.StartProcces("D:\\Nice\\GiveMeJob.exe").Returns(processInfo);
            var sut = new Commands(view, processProvider, configWatcher);

            //Act
            sut.StartProcess("D:\\Nice\\GiveMeJob.exe");

            //Assert
            view.Received().ShowOne(processInfo);
        }

        [Fact]
        public void KillProcess_ReturnKillProcess()
        {
            //Arrange
            var processProvider = Substitute.For<IProcessProvider>();
            var view = Substitute.For<IView>();
            var configWatcher = Substitute.For<IConfigWatcher>();
            var processInfo = new ProcessInfo("Trainee", 123, 4343);
            processProvider.KillProcces(123).Returns(processInfo);
            var sut = new Commands(view, processProvider, configWatcher);

            //Act
            sut.KillProcess(123);

            //Assert
            view.Received().ShowOne(processInfo);
        }

        [Fact]
        public void AddedConfig_ReturnMessage()
        {
            //Arrange           
            var view = Substitute.For<IView>();
            var configWatcher = Substitute.For<IConfigWatcher>();
            var configuredService = new ConfiguredService() { ServiceName = "Myroslav", ApplicationPath = "123" };
            var sut = new ConfiguredServicesTable(configWatcher, view);

            //Act
            configWatcher.ServiceAdded += Raise.Event<AddService>(configuredService);

            //Assert
            view.Received().ShowNewConfig(configuredService);
        }

        [Fact]
        public void AddConfig_AddObjectToCollection()
        {
            ///Arrange 
            var view = Substitute.For<IView>();
            var configWatcher = Substitute.For<IConfigWatcher>();
            var expected = new ConfiguredService() { ServiceName = "Myroslav", ApplicationPath = "123" };
            var sut = new ConfiguredServicesTable(configWatcher, view);

            //Act
            configWatcher.ServiceAdded += Raise.Event<AddService>(expected);
            var result = sut.GetServices();
            var actual = result[0];

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShowAllConfiguredServices_ReturnAllConfiguredServices()
        {
            //Arrange
            var view = Substitute.For<IView>();
            var configWatcher = Substitute.For<IConfigWatcher>();
            var processProvider = Substitute.For<IProcessProvider>();
            var processInfo = new List<ProcessInfo>() { new ProcessInfo("Give", 111, 6543), new ProcessInfo("Me", 222, 4367), new ProcessInfo("Job", 333, 8433) };
            processProvider.GetAllConfigureServices().Returns(processInfo);
            var sut = new Commands(view, processProvider, configWatcher);

            //Act
            sut.GetAll();

            //Assert
            view.Received().ShowAll(processInfo);
        }
    }
}