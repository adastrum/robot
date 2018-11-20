using System;
using System.Collections.Generic;
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

    public interface IDataWriter
    {
        void Write(string data);
    }

    public interface IDataReader
    {
        string Read();
    }

    public class Terminal
    {
        private readonly IDataReader _dataReader;
        private readonly IDataWriter _dataWriter;

        public Terminal(IDataReader dataReader, IDataWriter dataWriter)
        {
            _dataReader = dataReader;
            _dataWriter = dataWriter;
        }

        public Instructions ReadInstructions()
        {
            var input = _dataReader.Read();
            var lines = input.Split(Environment.NewLine);
            var numberOfCommands = int.Parse(lines[0]);
            var coordinatesParts = lines[1].Split(' ');
            var x = int.Parse(coordinatesParts[0]);
            var y = int.Parse(coordinatesParts[1]);
            var commands = new List<Command>();

            for (var i = 0; i < numberOfCommands; i++)
            {
                var commandParts = lines[2 + i].Split(' ');
                commands.Add(new Command(Enum.Parse<Direction>(commandParts[0]), int.Parse(commandParts[1])));
            }

            var instructions = new Instructions(x, y, commands);

            return instructions;
        }

        public void WriteReport(int cleanedPlacesCount)
        {
            _dataWriter.Write($"=> Cleaned: {cleanedPlacesCount}");
        }
    }

    public class Instructions
    {
        public int X { get; }
        public int Y { get; }
        public IList<Command> Commands { get; }

        public Instructions(int x, int y, IList<Command> commands)
        {
            X = x;
            Y = y;
            Commands = commands;
        }
    }

    public struct Command
    {
        public Direction Direction { get; }
        public int NumberOfSteps { get; }

        public Command(Direction direction, int numberOfSteps)
        {
            Direction = direction;
            NumberOfSteps = numberOfSteps;
        }
    }
}
