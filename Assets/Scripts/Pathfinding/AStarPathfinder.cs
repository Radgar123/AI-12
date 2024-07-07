using System.Collections.Generic;
using Interafaces;
using UnityEngine;

namespace Pathfinding
{
public class AStarPathfinder : IPathfinder
{
    private class Node
    {
        public Vector2Int Position { get; }
        public Node Parent { get; }
        public float G { get; }
        public float H { get; }
        public float F => G + H;

        public Node(Vector2Int position, Node parent, float g, float h)
        {
            Position = position;
            Parent = parent;
            G = g;
            H = h;
        }
    }

    public List<Vector2Int> FindPath(bool[,] maze, Vector2Int start, Vector2Int end)
    {
        List<Node> openList = new List<Node>();
        HashSet<Vector2Int> closedList = new HashSet<Vector2Int>();

        Node startNode = new Node(start, null, 0, Vector2Int.Distance(start, end));
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            openList.Sort((a, b) => a.F.CompareTo(b.F));
            Node currentNode = openList[0];

            if (currentNode.Position == end)
                return RetracePath(currentNode);

            openList.Remove(currentNode);
            closedList.Add(currentNode.Position);

            foreach (Vector2Int direction in GetDirections())
            {
                Vector2Int neighborPos = currentNode.Position + direction;
                if (!IsInBounds(maze, neighborPos) || !maze[neighborPos.x, neighborPos.y] || closedList.Contains(neighborPos))
                    continue;

                float g = currentNode.G + Vector2Int.Distance(currentNode.Position, neighborPos);
                float h = Vector2Int.Distance(neighborPos, end);
                Node neighborNode = new Node(neighborPos, currentNode, g, h);

                if (openList.Exists(node => node.Position == neighborPos && node.F <= neighborNode.F))
                    continue;

                openList.Add(neighborNode);
            }
        }
        return new List<Vector2Int>();
    }

    private List<Vector2Int> RetracePath(Node node)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node current = node;
        while (current != null)
        {
            path.Add(current.Position);
            current = current.Parent;
        }
        path.Reverse();
        return path;
    }

    private bool IsInBounds(bool[,] maze, Vector2Int pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < maze.GetLength(0) && pos.y < maze.GetLength(1);
    }

    private IEnumerable<Vector2Int> GetDirections()
    {
        return new List<Vector2Int> { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
    }
}
}