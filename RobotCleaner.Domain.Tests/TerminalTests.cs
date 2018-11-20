using System.Collections.Generic;
using FluentAssertions;
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

            var expected = new Instructions(
                x: 10, y: 22,
                commands: new List<Command>
                {
                    new Command(Direction.E, 2),
                    new Command(Direction.N, 1)
                });

            var actual = terminal.ReadInstructions();

            actual.Should().BeEquivalentTo(expected);
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
