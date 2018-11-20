namespace RobotCleaner.Domain
{
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