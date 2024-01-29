using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleSnake.Enums;
using SimpleSnake.GameObjects;
using SimpleSnake.GameObjects.Food;
using SimpleSnake.Utilities;

namespace SimpleSnake
{
    public class Engine
    {
        private Food[] foods;
        private Wall fieldBoundries;
        private Snake snake;
        private Direction currentDirection;
        private Point[] pointsOfDirection;
        private const int SleepTime = 100;
        private GameState state;
        private Random random;
        private Food foodReference;
        public Engine(Wall fieldBoundries, Snake snake)
        {
            CreateDirections();
            random = new Random();
            foods = new Food[]
            {
                new FoodAsterisk(),
                new FoodDollar(),
                new FoodHash()
            };

            this.fieldBoundries = fieldBoundries;
            this.snake = snake;
        }


        public void Start()
        {
            PlaceFoodOnField();
            while (state != GameState.Over)
            {
                if (Console.KeyAvailable)
                {
                    currentDirection = GetDirection();
                }

               state = UpdateSnake(pointsOfDirection[(int)currentDirection]);

               if (state == GameState.FoodEaten)
               {
                   PlaceFoodOnField();
                   state = GameState.Running;
               }

                Thread.Sleep(SleepTime);
            }

            PlatformInteraction.GameOver(fieldBoundries);
        }
        private void CreateDirections()
        {
            pointsOfDirection = new Point[]
            {
                 new Point(1, 0),
                 new Point(-1, 0),
                 new Point(0, 1),
                 new Point(0, -1)
            };
        }

        private GameState UpdateSnake(Point direction)
        {
            GameObject currentSnakeHead = snake.Head;
            Point nextHeadPoint = Point.GetNextPoint(direction, currentSnakeHead);

           

            if (snake.IsCollideWith(nextHeadPoint))
            {
                return GameState.Over;
            }

            GameObject snakeNewHead = new GameObject(currentSnakeHead.DrawSymbol, nextHeadPoint.X, nextHeadPoint.Y);

            if (fieldBoundries.IsCollideWith(snakeNewHead))
            {
                return GameState.Over;
            }
            snake.Move(snakeNewHead);
            // TODO: Food

            if (foodReference.IsCollideWith(snakeNewHead))
            {
                snake.Grow(direction,currentSnakeHead,foodReference.Points);
                return GameState.FoodEaten;
            }


            return GameState.Running;
        }

        

        private Direction GetDirection()
        {
           return PlatformInteraction.GetInput(currentDirection);
        }

        private void PlaceFoodOnField()
        {
            int randomFoodIndex = random.Next(0, foods.Length - 1);
            foodReference = foods[randomFoodIndex];

            do
            {
                foodReference.X = random.Next(2, fieldBoundries.X - 2);
                foodReference.Y = random.Next(2, fieldBoundries.Y - 2);
            } while (snake.IsCollideWith(foodReference));

            {
                
            }

            foodReference.Draw();
        }

    }

    internal enum GameState
    {
        Idle, 
        Start,
        FoodEaten,
        Running,
        Over
    }
}
