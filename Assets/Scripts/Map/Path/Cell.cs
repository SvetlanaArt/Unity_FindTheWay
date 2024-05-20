using UnityEngine;

namespace FindTheWay.Map.Path
{
    /// <summary>
    /// class for Path Star algoritm
    /// represents one cell in a map 
    /// </summary>
    public class Cell
    {
        public Vector2Int position { get; private set; }
        public int startCost { get; private set; }
        private int goalCost;
        public int totalCost { get; private set; }
        public bool isClosed;
        public Cell previous { get; private set; }

        public Cell(int row, int column, bool isObstacle)
        {
            position = new Vector2Int(row, column);
            isClosed = isObstacle;
            previous = null;
        }

        public void Fill(Cell previous, int newcost)
        {
            this.previous = previous;
            startCost = newcost;
            totalCost = startCost + goalCost;
        }

        public void SetGoalCost(Vector2Int position, Vector2Int goal)
        {
            int distance = Mathf.Abs(position.x - goal.x) +
                           Mathf.Abs(position.y - goal.y);
            goalCost = distance;
        }
    }


}
