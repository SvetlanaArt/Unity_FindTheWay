using System.Collections.Generic;
using UnityEngine;

namespace FindTheWay.Map.Path
{
    /// <summary>
    /// class for Path Star algoritm
    /// formed information about obstacles and start, goal points
    /// </summary>
    public class GridMap
    {
        Obstacles obstacles;

        Vector2Int pointA;
        Vector2Int pointB;

        public GridMap()
        {
            obstacles = new Obstacles(0, 0);
            ClearPoints();
        }

        public void SetSize(int x, int y)
        {
            obstacles.CreateElementsWithBorders(x, y);
            ClearPoints();
        }

        private void ClearPoints()
        {
            pointA = Vector2Int.zero;
            pointB = Vector2Int.zero;
        }

        public void SetElement(Vector2Int cell, ElementType type)
        {
            cell = CorrectCellWithBorder(cell);
            switch (type)
            {
                case ElementType.obsticle:
                    obstacles.SetElement(cell.x, cell.y, true);
                    break;
                case ElementType.pointA:
                    pointA = cell;
                    break;
                case ElementType.pointB:
                    pointB = cell;
                    break;
                case ElementType.none:
                    break;
            }
        }

        public void DeleteObstacle(Vector2Int cell)
        {
            cell = CorrectCellWithBorder(cell);
            obstacles.SetElement(cell.x, cell.y, false);
        }

        public bool isCellFree(Vector2Int cell)
        {
            cell = CorrectCellWithBorder(cell);
            if (!obstacles.IsCellInMap(cell))
                return false;
            if (cell == pointA || cell == pointB)
                return false;
            return !obstacles.GetElement(cell.x, cell.y);
        }

        public List<Vector2Int> GetPath()
        {
            PathFinding pathFinding = new PathFinding(obstacles);
            List<Vector2Int> path = pathFinding.FindPath(pointA, pointB);
            if (path == null)
                return null;

            for (int i = 0; i < path.Count; i++)
            {
                path[i] = CorrectCellWithoutBorder(path[i]);
            }

            return path;
        }

        private static Vector2Int CorrectCellWithBorder(Vector2Int cell)
        {
            cell += Vector2Int.one;
            return cell;
        }

        private static Vector2Int CorrectCellWithoutBorder(Vector2Int cell)
        {
            cell -= Vector2Int.one;
            return cell;
        }

    }

}
