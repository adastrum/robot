using System.Collections.Generic;

namespace RobotCleaner.Domain
{
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
}