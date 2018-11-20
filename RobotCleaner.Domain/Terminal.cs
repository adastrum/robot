using System;
using System.Collections.Generic;

namespace RobotCleaner.Domain
{
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
}