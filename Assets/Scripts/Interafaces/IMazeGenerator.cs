namespace Interafaces
{
    public interface IMazeGenerator
    {
        bool[,] GenerateMaze(int width, int height);
    }
}