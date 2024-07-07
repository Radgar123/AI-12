using System.Collections.Generic;
using Interafaces;
using UnityEngine;

namespace MapGenerator
{
    public class RecursiveBacktrackerMazeGenerator : IMazeGenerator
    {
        private int width, height;
        private bool[,] maze;
        private System.Random rand = new System.Random();

        public bool[,] GenerateMaze(int width, int height)
        {
            this.width = width;
            this.height = height;
            maze = new bool[width, height];
        
            Generate(1, 1);
            return maze;
        }

        private void Generate(int x, int y)
        {
            maze[x, y] = true;

            foreach (Vector2Int direction in GetRandomDirections())
            {
                int newX = x + direction.x * 2;
                int newY = y + direction.y * 2;

                if (IsInBounds(newX, newY) && !maze[newX, newY])
                {
                    maze[x + direction.x, y + direction.y] = true;
                    Generate(newX, newY);
                }
            }
        }

        private bool IsInBounds(int x, int y)
        {
            return x > 0 && y > 0 && x < width - 1 && y < height - 1;
        }

        private IEnumerable<Vector2Int> GetRandomDirections()
        {
            List<Vector2Int> directions = new List<Vector2Int>
            {
                Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
            };
            for (int i = directions.Count - 1; i > 0; i--)
            {
                int randIndex = rand.Next(i + 1);
                Vector2Int temp = directions[i];
                directions[i] = directions[randIndex];
                directions[randIndex] = temp;
            }
            return directions;
        }
    }
}