using Moq;
using Xunit;

namespace RobotCleaner.Domain.Tests
{
    public class TerminalTests
    {
        [Fact]
        public void ReadInstructions_ParsesInput()
        {
            var (terminal, dataReaderMock, _) = CreateTerminal();

            dataReaderMock
                .Setup(x => x.Read())
                .Returns("2\r\n10 22\r\nE 2\r\nN 1");

            var actual = terminal.ReadInstructions();

            Assert.Equal(10, actual.X);
            Assert.Equal(22, actual.Y);
            Assert.Equal(2, actual.Commands.Count);
            Assert.Equal(new Command(Direction.E, 2), actual.Commands[0]);
            Assert.Equal(new Command(Direction.N, 1), actual.Commands[1]);
        }

        [Fact]
        public void WriteReport_PrintsReport()
        {
            var (terminal, _, dataWriterMock) = CreateTerminal();

            const int cleanedPlacesCount = 42;
            var expected = $"=> Cleaned: {cleanedPlacesCount}";

            terminal.WriteReport(cleanedPlacesCount);

            dataWriterMock
                .Verify(x => x.Write(expected), Times.Once);
        }

        private static (Terminal, Mock<IDataReader>, Mock<IDataWriter>) CreateTerminal()
        {
            var dataReaderMock = new Mock<IDataReader>();
            var dataWriterMock = new Mock<IDataWriter>();
            var terminal = new Terminal(dataReaderMock.Object, dataWriterMock.Object);

            return (terminal, dataReaderMock, dataWriterMock);
        }
    }
}
