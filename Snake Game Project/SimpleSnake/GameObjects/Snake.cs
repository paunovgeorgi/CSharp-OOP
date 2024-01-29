using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public class Snake
    {
        private const char SnakeSymbol = '\u25CF';
        private const char EmptySymbol = ' ';

        private readonly Queue<GameObject> snakeElements;
        public GameObject Head => snakeElements.Last();

        public Snake()
        {
            snakeElements = new Queue<GameObject>();
            GenerateSnakeElements();
        }

        private void GenerateSnakeElements()
        {
            for (int y = 1; y <= 6; y++)
            {
                var obj = new GameObject(SnakeSymbol, 2, y);
                snakeElements.Enqueue(obj);
                obj.Draw();
            }
        }

        internal void Move(GameObject newSnakeHead)
        {
            snakeElements.Enqueue(newSnakeHead);
            newSnakeHead.Draw();
            GameObject tail = snakeElements.Dequeue();

            tail = new GameObject(EmptySymbol, tail.X, tail.Y);
            tail.Draw();

            // TODO: Draw head
            // TODO: Dequeue tail 
            // TODO: Draw Empty tail

        }

        public bool IsCollideWith(Point point)
        {
            return snakeElements.Any(s => s.X == point.X && s.Y == point.Y);
        }

        public void Grow(Point direction,Point currentSnakeHead, int addingPoints)
        {
            Point nextPoint = currentSnakeHead;
            for (int i = 0; i < addingPoints; i++)
            {
                GameObject newElement = new GameObject(SnakeSymbol, nextPoint.X, nextPoint.Y);
                snakeElements.Enqueue(newElement);
                nextPoint = Point.GetNextPoint(direction, currentSnakeHead);
            }
        }

        private GameObject GetNextPoint(Point direction, GameObject head)
        {
            throw new NotImplementedException();
        }
    }
}
