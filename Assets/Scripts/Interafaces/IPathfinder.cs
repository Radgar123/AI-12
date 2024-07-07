using System.Collections.Generic;
using UnityEngine;

namespace Interafaces
{
    public interface IPathfinder
    {
        List<Vector2Int> FindPath(bool[,] maze, Vector2Int start, Vector2Int end);
    }

}