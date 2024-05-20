using UnityEngine;

namespace FindTheWay.Map.Path
{
    /// <summary>
    /// class for Path Star algoritm
    /// contain obstacles map with perimeter borders 
    /// </summary>
    public class Obstacles
    {
        bool[,] elemets;
        public int rowCount { get; private set; }
        public int columnCount { get; private set; }

        public Obstacles(int rows, int columns)
        {
            SetSize(rows, columns);
        }

        public void SetSize(int rows, int columns)
        {
            rowCount = rows;
            columnCount = columns;
        }

        public void SetElement(int row, int column, bool value)
        {
            elemets[row, column] = value;
        }

        public bool GetElement(int row, int column)
        {
            return elemets[row, column];
        }

        public bool IsCellInMap(Vector2Int cell)
        {
            if (cell.x != Mathf.Clamp(cell.x, 1, rowCount - 2) ||
                cell.y != Mathf.Clamp(cell.y, 1, columnCount - 2))
            {
                return false;
            }
            return true;
        }

        public void CreateElementsWithBorders(int rows, int columns)
        {
            SetSize(rows + 2, columns + 2);
            elemets = new bool[rowCount, columnCount];
            GenerateElementsWIthBorders();
        }

        private void GenerateElementsWIthBorders()
        {
            for (int i = 1; i < rowCount - 1; i++)
            {
                for (int j = 1; j < columnCount - 1; j++)
                {
                    elemets[i, j] = false;
                }
                // create border
                elemets[i, 0] = true;
                elemets[i, columnCount - 1] = true;
            }
            // create border
            for (int j = 0; j < columnCount - 1; j++)
            {
                elemets[0, j] = true;
                elemets[rowCount - 1, j] = true;
            }
        }

    }


}
