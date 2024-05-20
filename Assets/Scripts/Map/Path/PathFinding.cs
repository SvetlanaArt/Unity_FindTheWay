using System.Collections.Generic;
using UnityEngine;

namespace FindTheWay.Map.Path
{
    /// <summary>
    /// Path Star algoritm
    /// </summary>
    public class PathFinding
    {
        const int ONE_STEP_COST = 1;

        Cell[,] cells;
        int rows;
        int columns;

        Cell currentCell;
        List<Cell> OpenCells;

        Vector2Int goalPosition;

        public PathFinding(Obstacles obstacles)
        {
            rows = obstacles.rowCount;
            columns = obstacles.columnCount;
            OpenCells = new List<Cell>();
            cells = new Cell[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    cells[i, j] = new Cell(i, j, obstacles.GetElement(i, j));
                }
        }

        public List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
        {
            goalPosition = goal;
            Cell goalCell = cells[goal.x, goal.y];
            currentCell = cells[start.x, start.y];
            currentCell.Fill(null, 0);
            OpenCells.Add(currentCell);

            while (currentCell != null)
            {
                FindNeighbours(currentCell);
                currentCell.isClosed = true;
                OpenCells.Remove(currentCell);
                // is goal reached
                if (goalCell.previous != null)
                {
                    OpenCells.Clear();
                    return GetPath(goalCell);
                }
                currentCell = FindBestCell();
            }
            OpenCells.Clear();
            return null;
        }

        private List<Vector2Int> GetPath(Cell goalCell)
        {
            Cell current = goalCell;
            List<Vector2Int> newPath = new List<Vector2Int>();
            while (current != null)
            {
                newPath.Add(current.position);
                current = current.previous;
            }
            return newPath;
        }

        private Cell FindBestCell()
        {
            Cell bestCell = null;
            int minValue = int.MaxValue;
            foreach (Cell cell in OpenCells)
            {
                if (cell.totalCost < minValue)
                {
                    bestCell = cell;
                    minValue = cell.totalCost;
                }
            }
            return bestCell;
        }

        private void FindNeighbours(Cell cell)
        {
            Vector2Int position = cell.position;
            CheckCell(cells[position.x + 1, position.y], cell);
            CheckCell(cells[position.x, position.y + 1], cell);
            CheckCell(cells[position.x - 1, position.y], cell);
            CheckCell(cells[position.x, position.y - 1], cell);

        }

        private void CheckCell(Cell cell, Cell previous)
        {
            if (cell.isClosed)
                return;
            int newcost = previous.startCost + 1;
            if (OpenCells.Contains(cell))
            {
                if (newcost < cell.startCost)
                {
                    cell.Fill(previous, newcost);
                }
                return;
            }
            cell.SetGoalCost(cell.position, goalPosition);
            cell.Fill(previous, newcost);
            OpenCells.Add(cell);
        }
    }

}
