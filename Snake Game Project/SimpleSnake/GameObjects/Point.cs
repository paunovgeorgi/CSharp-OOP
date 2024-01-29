

namespace SimpleSnake.GameObjects
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }


        public virtual bool IsCollideWith(Point point)
        {
            return 
                   point.X == X &&
                   point.Y == Y;
        }

        public static Point GetNextPoint(Point direction, Point point)
        {
            return new Point(point.X + direction.X, point.Y + direction.Y);
        }
    }
}
