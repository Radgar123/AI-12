using System.Collections.Generic;
using Interafaces;
using Pathfinding;
using UnityEngine;

namespace MapGenerator
{
   public class MazeVisualizer : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject pathPrefab;
    public GameObject startPrefab;
    public GameObject endPrefab;
    public int width = 21;
    public int height = 21;

    private IMazeGenerator mazeGenerator;
    private IPathfinder pathfinder;

    private void Start()
    {
        mazeGenerator = new RecursiveBacktrackerMazeGenerator();
        pathfinder = new AStarPathfinder();

        bool[,] maze = mazeGenerator.GenerateMaze(width, height);

        Vector2Int start = GetRandomEmptyPosition(maze);
        Vector2Int end = GetRandomEmptyPosition(maze, start);

        List<Vector2Int> path = pathfinder.FindPath(maze, start, end);

        VisualizeMaze(maze);
        VisualizePoint(start, startPrefab);
        VisualizePoint(end, endPrefab);
        VisualizePath(path);
    }

    private Vector2Int GetRandomEmptyPosition(bool[,] maze, Vector2Int? avoid = null)
    {
        List<Vector2Int> emptyPositions = new List<Vector2Int>();
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                if (maze[x, y] && (!avoid.HasValue || new Vector2Int(x, y) != avoid.Value))
                {
                    emptyPositions.Add(new Vector2Int(x, y));
                }
            }
        }
        return emptyPositions[Random.Range(0, emptyPositions.Count)];
    }

    private void VisualizeMaze(bool[,] maze)
    {
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                if (!maze[x, y])
                {
                    Instantiate(wallPrefab, new Vector3(x, 0, y), Quaternion.identity);
                }
            }
        }
    }

    private void VisualizePath(List<Vector2Int> path)
    {
        foreach (Vector2Int point in path)
        {
            Instantiate(pathPrefab, new Vector3(point.x, 0, point.y), Quaternion.identity);
        }
    }

    private void VisualizePoint(Vector2Int point, GameObject prefab)
    {
        Instantiate(prefab, new Vector3(point.x, 0, point.y), Quaternion.identity);
    }
}
}