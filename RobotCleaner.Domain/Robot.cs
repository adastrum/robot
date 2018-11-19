using System;
using System.Collections.Generic;

namespace RobotCleaner.Domain
{
    public class Robot
    {
        private Point _point;
        private readonly Dictionary<Point, bool> _visited = new Dictionary<Point, bool>();

        public Robot(int x, int y)
        {
            _point = new Point(x, y);
        }

        public int X => _point.X;
        public int Y => _point.Y;

        public void Move(Direction direction, int numberOfSteps)
        {
            for (var i = 0; i < numberOfSteps; i++)
            {
                Step(direction);
            }
        }

        private void Step(Direction direction)
        {
            var x = X;
            var y = Y;

            switch (direction)
            {
                case Direction.E:
                    x++;
                    break;
                case Direction.W:
                    x--;
                    break;
                case Direction.S:
                    y--;
                    break;
                case Direction.N:
                    y++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            _point = new Point(x, y);
            _visited[_point] = true;
        }

        public int GetCleanedPlacesCount()
        {
            return _visited.Count + 1;
        }
    }
}