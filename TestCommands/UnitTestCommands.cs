using Xunit;
using DelphiSupervisorV6;
using System.Diagnostics;
using System.Linq;

namespace TestCommands
{
    public class UnitTestCommands
    {
        [Fact]
        public void ShowAll_ReturAllProcess()
        {
            //Arrange
            ProcessProvider sut = new ProcessProvider();

            //Act 
            var result = sut.GetAllProcesses().Count;
            var expected = Process.GetProcesses().ToList().Count;

            //Assert
            Assert.Equal(expected, result);
        }
    }
}